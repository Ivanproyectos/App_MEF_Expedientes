﻿
using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras;
using MEF.Expedientes.Entity.Maestras.Vistas; 
using MEF.Expedientes.Service.Maestras;
using MEF.Expedientes.Service.Maestras.Vista; 
using MEF.Expedientes.Contract.Maestras;
using MEF.Expedientes.Contract.Maestras.Vista;
using System.Collections.Generic;
using System.Web.Mvc;
using App_MEF_Expedientes.Areas.Maestras.Models;
using MEF.Expedientes.Entity.Administracion;
using MEF.Expedientes.Service.Administracion;
using MEF.Expedientes.Contract.Administracion;
using System.Data;
using System.Linq;
using System;
using System.IO;
using System.Configuration;

namespace App_MEF_Expedientes.Areas.Maestras.Controllers
{
    public class ExpedientesController : Controller
    {
        // GET: Maestras/Expedientes
        private ICls_Serv_Oficina _cls_Serv_Oficina;
        private ICls_Serv_Personal _cls_Serv_Personal;
        private ICls_Serv_Expedientes _cls_Expedientes;
        private ICls_Serv_Dominio _cls_Serv_Dominio;
        private ICls_V_Serv_Expedientes _cls_V_Serv_Expedientes;
      

        public ExpedientesController()
        {
            _cls_Serv_Oficina = new Cls_Serv_Oficina();
            _cls_Serv_Personal = new Cls_Serv_Personal();
            _cls_Expedientes = new Cls_Serv_Expedientes();
            _cls_Serv_Dominio = new Cls_Serv_Dominio();
            _cls_V_Serv_Expedientes = new Cls_V_Serv_Expedientes(); 
        }


        public ActionResult Index()
        {
            ExpedientesModelView modelo = new ExpedientesModelView();
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();

            Cls_Ent_Dominio entidad_Estado = new Cls_Ent_Dominio();
            entidad_Estado.NOM_DOMINIO = "ESTADO";
            entidad_Estado.FLG_ESTADO = 1;
            var Lista_Estado = _cls_Serv_Dominio.Dominio_Listar(entidad_Estado, ref auditoria);
            if (auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (!auditoria.RECHAZAR)
                {
                    modelo.Lista_Estado = Lista_Estado.Select(x => new SelectListItem()
                    {
                        Text = x.DESC_CORTA_DOMINIO.ToString(),
                        Value = x.ID_DOMINIO.ToString()
                    }).ToList();
                    modelo.Lista_Estado.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                }
            }
            else
            {
                modelo.Lista_Estado.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }




            Cls_Ent_Dominio entidad_Situacion = new Cls_Ent_Dominio();
            entidad_Situacion.NOM_DOMINIO = "SITUACION";
            entidad_Situacion.FLG_ESTADO = 1;
            var Lista_Situacion = _cls_Serv_Dominio.Dominio_Listar(entidad_Situacion, ref auditoria);
            if (auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (!auditoria.RECHAZAR)
                {
                    modelo.Lista_Situacion = Lista_Situacion.Select(x => new SelectListItem()
                    {
                        Text = x.DESC_CORTA_DOMINIO.ToString(),
                        Value = x.ID_DOMINIO.ToString()
                    }).ToList();
                    modelo.Lista_Situacion.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                }
            }
            else
            {
                modelo.Lista_Situacion.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }


            return View(modelo);
        }


        public JsonResult Expedientes_Paginado(Recursos.Paginacion.GridTable grid)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                grid.page = (grid.page == 0) ? 1 : grid.page;
                grid.rows = (grid.rows == 0) ? 100 : grid.rows;
                var @where = (Recursos.Paginacion.Css_Paginacion.GetWhere(grid.filters, grid.rules));
                if (!string.IsNullOrEmpty(@where))
                {
                    grid._search = true;
                    if (!string.IsNullOrEmpty(grid.searchString))
                    {
                        @where = @where + " and ";
                    }
                }
                System.Threading.Thread.Sleep(1000);


                //  Recursos.Css_Log.Guardar(@where);
                IList<Cls_v_Expedientes> lista = _cls_V_Serv_Expedientes.Expedientes_Listar_Todo(grid.sidx, grid.sord, grid.rows, grid.page, @where, ref auditoria);

                if (auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    var generic = Recursos.Paginacion.Css_Paginacion.BuscarPaginador(grid.page, grid.rows, (int)auditoria.OBJETO, lista);
                    generic.Value.rows = generic.List.Select(item => new Recursos.Paginacion.Css_Row
                    {
                        id = item.ID_EXPEDIENTE.ToString(),
                        cell = new string[] {
                        null,
                        null,
                        null,
                        null,
                        null,
                        item.ID_EXPEDIENTE.ToString(),
                        item.COD_EXPEDIENTE.ToString() + "-" + item.ANIO_CODIGO.ToString(),
                        item.NOMBRE_COMPLETO.ToString(),
                        item.OFICINA,
                        item.REGIMEN_LABORAL,
                        item.FALTA,
                        item.SANCION,
                        item.FECHA_HECHO,
                        item.SITUACION,
                        item.ESTADO,
                        item.FLG_ESTADO.ToString(),
                        item.USU_CREACION,
                        item.FEC_CREACION,
                        item.USU_MODIFICACION,
                        item.FEC_MODIFICACION,
                        item.EXTENSION_ARCHIVO, 
                        item.DIAS.ToString(),
                   }
                    }).ToArray();

                    var jsonResult = Json(generic.Value, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Recursos.Clases.Css_Log.Guardar(ex.ToString());
                return null;
            }
        }





        public ActionResult Buscar_Oficina_Listar(string DESC_OFICINA)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            auditoria.Limpiar();
            IEnumerable<Cls_Ent_Oficina> lista = _cls_Serv_Oficina.Buscar_Oficina_Listar(DESC_OFICINA, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
            else
            {
                auditoria.OBJETO = lista;
            }
            return Json(auditoria.OBJETO, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Buscar_Personal_Listar(string NOMBRES_APE)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            auditoria.Limpiar();
            IEnumerable<Cls_Ent_Personal> lista = _cls_Serv_Personal.Buscar_Personal_Listar(NOMBRES_APE, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
            else
            {
                auditoria.OBJETO = lista;
            }
            return Json(auditoria.OBJETO, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Mantenimiento(int id, string Accion)
        {
            ExpedientesModelView  modelo = new  ExpedientesModelView();
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria(); 
            modelo.ID_EXPEDIENTE = id;
            modelo.Accion = Accion;
            try
            {
                if (Accion == "M")
                {
                    IEnumerable<Cls_v_Expedientes> lista;
                    Cls_v_Expedientes entidad = new Cls_v_Expedientes
                    {
                        ID_EXPEDIENTE = id
                    };
                    lista = _cls_V_Serv_Expedientes.Expedientes_V_Buscar(ref auditoria, entidad);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        if (auditoria.RECHAZAR)
                            Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    }
                    else
                    {
                        foreach (var item in lista)
                        {
                            modelo.COD_EXPEDIENTE = item.COD_EXPEDIENTE;
                            modelo.FECHA_RECEPCION = item.FECHA_RECEPCION;
                            modelo.FECHA_PRESCRIPCION = item.FECHA_PRESCRIPCION;
                            modelo.HOJA_RUTA = item.HOJA_RUTA;
                            modelo.FECHA_HECHO = item.FECHA_HECHO;
                            modelo.ID_REMITENTE = item.ID_REMITENTE;
                            modelo.SEARCH_REMITENTE = item.REMITENTE;
                            modelo.ID_PERSONAL = item.ID_PERSONAL;
                            modelo.SEARCH_PERSONAL = item.NOMBRE_COMPLETO;
                            modelo.REGIMEN_LABORAL = item.REGIMEN_LABORAL;
                            modelo.OFICINA = item.OFICINA;
                            modelo.ID_ACTO = item.ID_ACTO;
                            modelo.OBSERVACION_INVESTIGADORA = item.OBSERVACION_INVESTIGADORA;
                            modelo.ID_FALTA = item.ID_FALTA;
                            modelo.ARTICULO = item.ARTICULO;
                            modelo.INC = item.INC;
                            modelo.ID_PRECALIFICACION = item.ID_PRECALIFICACION;
                            modelo.TIPO_SANCION_RECOMENDADA = item.TIPO_SANCION_RECOMENDADA;
                            modelo.ACTO_INICIO = item.ACTO_INICIO;
                            modelo.FECHA_NOTIFICACION = item.FECHA_NOTIFICACION;
                            modelo.OBSERVACION_INSTRUCTORA = item.OBSERVACION_INSTRUCTORA;
                            modelo.ID_ORGANO_INSTRUCTOR = item.ID_ORGANO_INSTRUCTOR;
                            modelo.SEARCH_ORGANO_INSTRUCTOR = item.ORGANO_INSTRUCTOR;
                            modelo.RECOMENDACION_PREINFORME = item.RECOMENDACION_PREINFORME;
                            modelo.ID_SANCION_RECOMENDADA = item.ID_SANCION_RECOMENDADA;
                            modelo.FECHA_NOTIFICACION_INICIO = item.FECHA_NOTIFICACION_INICIO;
                            modelo.DOCUMENTO_FINALIZACION = item.DOCUMENTO_FINALIZACION;
                            modelo.RECOMENDACION_INSTRUCTOR = item.RECOMENDACION_INSTRUCTOR;
                            modelo.ID_ORGANO_SANCIONADOR = item.ID_ORGANO_SANCIONADOR;
                            modelo.SEARCH_ORGANO_SANCIONADOR = item.ORGANO_SANCIONADOR;
                            modelo.SANCION = item.SANCION;
                            modelo.ID_SITUACION = item.ID_SITUACION;
                            modelo.ID_ESTADO = item.ID_ESTADO;
                            modelo.OBSERVACION_SANCIONADORA = item.OBSERVACION_SANCIONADORA;
                            modelo.DIAS_VIGENTE = item.DIAS_VIGENTE;
                            modelo.DOCUMENTO_NOTIFICA = item.DOCUMENTO_NOTIFICA;

                            modelo.ORGANO_INSTRUCTOR_S4 = item.ORGANO_INSTRUCTOR_S4;
                            modelo.ID_PRESUNTA_FALTA = item.ID_PRESUNTA_FALTA;
                            modelo.ID_INFORME_PRECALIFICACION = item.ID_INFORME_PRECALIFICACION;
                            modelo.ID_ORGANO_INSTRUCTOR_S4 = item.ID_ORGANO_INSTRUCTOR_S4;
                            modelo.ANIO_CODIGO = item.ANIO_CODIGO;
                            
                        }
                    }
                }
                else
                {
                    Cls_Ent_Dominio entidad_Correlativo = new Cls_Ent_Dominio();
                    entidad_Correlativo.COD_DOMINIO = "NUM_EXP";
                    Cls_Ent_Dominio _Correlativo = _cls_Serv_Dominio.Dominio_Listar_MaxId(entidad_Correlativo, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    }
                    else
                    {
                        if (_Correlativo != null)
                            modelo.COD_EXPEDIENTE = _Correlativo.DESC_CORTA_DOMINIO;
                        else
                            modelo.COD_EXPEDIENTE = "0"; 
                    }

                }



                Cls_Ent_Dominio entidad_acto = new Cls_Ent_Dominio();
                entidad_acto.NOM_DOMINIO = "TIPACT";
                entidad_acto.FLG_ESTADO = 1;
                var listaActo = _cls_Serv_Dominio.Dominio_Listar(entidad_acto, ref auditoria);
                if (auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    if (!auditoria.RECHAZAR)
                    {
                        modelo.Lista_Acto = listaActo.Select(x => new SelectListItem()
                        {
                            Text = x.DESC_CORTA_DOMINIO.ToString(),
                            Value = x.ID_DOMINIO.ToString()
                        }).ToList();
                        modelo.Lista_Acto.Insert(0, new SelectListItem() { Value = "0", Text = "-- Seleccione --" });
                    }
                }
                else
                {
                    modelo.Lista_Acto.Insert(0, new SelectListItem() { Value = "0", Text = "-- Seleccione --" });
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                }

                Cls_Ent_Dominio entidad_Estado = new Cls_Ent_Dominio();
                entidad_Estado.NOM_DOMINIO = "ESTADO";
                entidad_Estado.FLG_ESTADO = 1;
                var Lista_Estado = _cls_Serv_Dominio.Dominio_Listar(entidad_Estado, ref auditoria);
                if (auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    if (!auditoria.RECHAZAR)
                    {
                        modelo.Lista_Estado = Lista_Estado.Select(x => new SelectListItem()
                        {
                            Text = x.DESC_CORTA_DOMINIO.ToString(),
                            Value = x.ID_DOMINIO.ToString()
                        }).ToList();
                        modelo.Lista_Estado.Insert(0, new SelectListItem() { Value = "0", Text = "-- Seleccione --" });
                    }
                }
                else
                {
                    modelo.Lista_Estado.Insert(0, new SelectListItem() { Value = "0", Text = "-- Seleccione --" });
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                }



                Cls_Ent_Dominio entidad_Situacion = new Cls_Ent_Dominio();
                entidad_Situacion.NOM_DOMINIO = "SITUACION";
                entidad_Situacion.FLG_ESTADO = 1;
                var Lista_Situacion = _cls_Serv_Dominio.Dominio_Listar(entidad_Situacion, ref auditoria);
                if (auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    if (!auditoria.RECHAZAR)
                    {
                        modelo.Lista_Situacion = Lista_Situacion.Select(x => new SelectListItem()
                        {
                            Text = x.DESC_CORTA_DOMINIO.ToString(),
                            Value = x.ID_DOMINIO.ToString()
                        }).ToList();
                        modelo.Lista_Situacion.Insert(0, new SelectListItem() { Value = "0", Text = "-- Seleccione --" });
                    }
                }
                else
                {
                    modelo.Lista_Situacion.Insert(0, new SelectListItem() { Value = "0", Text = "-- Seleccione --" });
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                }


                Cls_Ent_Dominio entidad_Sancion = new Cls_Ent_Dominio();
                entidad_Sancion.NOM_DOMINIO = "TIPSAN";
                entidad_Sancion.FLG_ESTADO = 1;
                var Lista_Sancion = _cls_Serv_Dominio.Dominio_Listar(entidad_Sancion, ref auditoria);
                if (auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    if (!auditoria.RECHAZAR)
                    {
                        modelo.Lista_Sacion_Recomendada = Lista_Sancion.Select(x => new SelectListItem()
                        {
                            Text = x.DESC_CORTA_DOMINIO.ToString(),
                            Value = x.ID_DOMINIO.ToString()
                        }).ToList();
                        modelo.Lista_Sacion_Recomendada.Insert(0, new SelectListItem() { Value = "0", Text = "-- Seleccione --" });
                    }
                }
                else
                {
                    modelo.Lista_Sacion_Recomendada.Insert(0, new SelectListItem() { Value = "0", Text = "-- Seleccione --" });
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                }


                Cls_Ent_Dominio entidad_Falta = new Cls_Ent_Dominio();
                entidad_Falta.NOM_DOMINIO = "TIPFAL";
                entidad_Falta.FLG_ESTADO = 1;
                var Lista_Falta = _cls_Serv_Dominio.Dominio_Listar(entidad_Falta, ref auditoria);
                if (auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    if (!auditoria.RECHAZAR)
                    {
                        modelo.Lista_Falta = Lista_Falta.Select(x => new SelectListItem()
                        {
                            Text = x.DESC_CORTA_DOMINIO.ToString(),
                            Value = x.ID_DOMINIO.ToString()
                        }).ToList();
                        modelo.Lista_Falta.Insert(0, new SelectListItem() { Value = "0", Text = "-- Seleccione --" });
                    }
                }
                else
                {
                    modelo.Lista_Falta.Insert(0, new SelectListItem() { Value = "0", Text = "-- Seleccione --" });
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                }

                modelo.Lista_PresuntaFalta = new List<SelectListItem>();
                modelo.Lista_PresuntaFalta.Insert(0, new SelectListItem() { Value = "0", Text = "--Seleccione--" });
                modelo.Lista_PresuntaFalta.Insert(1, new SelectListItem() { Value = "1", Text = "AMONESTACION ESCRITA" });
                modelo.Lista_PresuntaFalta.Insert(2, new SelectListItem() { Value = "2", Text = "SUSPENSIÓN" });
                modelo.Lista_PresuntaFalta.Insert(3, new SelectListItem() { Value = "3", Text = "DESTITUCIÓN" });


                modelo.Lista_InformePrecalificacion = new List<SelectListItem>();
                modelo.Lista_InformePrecalificacion.Insert(0, new SelectListItem() { Value = "0", Text = "--Seleccione--" });
                modelo.Lista_InformePrecalificacion.Insert(1, new SelectListItem() { Value = "1", Text = "INICIO DE PAD" });
                modelo.Lista_InformePrecalificacion.Insert(2, new SelectListItem() { Value = "2", Text = "NO HA LUGAR" });
                

            }
            catch (Exception ex)
            {
               var Mensaje =  auditoria.MENSAJE_SALIDA = ex.Message;
                Recursos.Clases.Css_Log.Guardar(Mensaje);
            }


            return View(modelo);
        }



        public ActionResult Mantenimiento_Archivos(int id, string COD_EXPEDIENTE)
        {
            AdjuntosModelView modelo = new AdjuntosModelView();
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            modelo.ID_MAESTRA = id;
            modelo.COD_EXPEDIENTE = COD_EXPEDIENTE; 
    
            
            Cls_Ent_Dominio entidad_tipoArc = new Cls_Ent_Dominio();
            entidad_tipoArc.NOM_DOMINIO = "TIPO_ADJ";
            entidad_tipoArc.FLG_ESTADO = 1;
            var Lista_Sancion = _cls_Serv_Dominio.Dominio_Listar(entidad_tipoArc, ref auditoria);
            if (auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (!auditoria.RECHAZAR)
                {
                    modelo.Lista_Tipo_Archivo = Lista_Sancion.Select(x => new SelectListItem()
                    {
                        Text = x.DESC_CORTA_DOMINIO.ToString(),
                        Value = x.ID_DOMINIO.ToString()
                    }).ToList();
                    modelo.Lista_Tipo_Archivo.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                }
            }
            else
            {
                modelo.Lista_Tipo_Archivo.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }


            return View(modelo);
        }

        public ActionResult Mantenimiento_Expediente(int id, string COD_EXPEDIENTE)
        {
            ExpedientesModelView modelo = new ExpedientesModelView();
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            modelo.ID_EXPEDIENTE = id;
            modelo.COD_EXPEDIENTE = COD_EXPEDIENTE;

            IEnumerable<Cls_Ent_Expedientes> lista;
            lista = _cls_Expedientes.Expedientes_Buscar(ref auditoria, id);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (auditoria.RECHAZAR)
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
            else
            {
                foreach (var item in lista)
                {
                    if (item.NOMBRE_ARCHIVO != null)
                    {
                        modelo.MiArchivo = new Cls_Ent_Archivo();
                        string ID_UNICO_TEMP = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal();
                        var ruta_temporal = Recursos.Clases.Css_Ruta.Ruta_Temporal() + ID_UNICO_TEMP + item.EXTENSION_ARCHIVO;
                        System.IO.File.Create(ruta_temporal).Close();
                        System.IO.File.WriteAllBytes(ruta_temporal, item.ARCHIVO_BLOB);
                        modelo.MiArchivo.CODIGO_ARCHIVO = ID_UNICO_TEMP;
                        modelo.MiArchivo.EXTENSION = item.EXTENSION_ARCHIVO;
                    }
                }
            }

            return View(modelo);
        }


        public ActionResult Expedientes_InsertarDocumento_Digital(ExpedientesModelView param)
        {

            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            var ruta_temporal = Recursos.Clases.Css_Ruta.Ruta_Temporal() + param.MiArchivo.CODIGO_ARCHIVO + param.MiArchivo.EXTENSION;
            if (System.IO.File.Exists(ruta_temporal))
            {
                byte[] ARCHIVO_BLOB = Foto(ruta_temporal);
                System.IO.File.Delete(ruta_temporal);
                Cls_Ent_Expedientes entidad = new Cls_Ent_Expedientes
                {
                    ID_EXPEDIENTE = param.ID_EXPEDIENTE,
                    EXTENSION_ARCHIVO = param.MiArchivo.EXTENSION,
                    NOMBRE_ARCHIVO = param.MiArchivo.NOMBRE_ARCHIVO,
                    ARCHIVO_BLOB = ARCHIVO_BLOB
                };

                _cls_Expedientes.Expedientes_InsertarDocumento_Digital(entidad, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    if (auditoria.RECHAZAR)
                        Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                }

            }
            else
            {
                auditoria.Rechazar("Archivo no existe");
            }

            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Expedientes_Insertar(Cls_Ent_Expedientes entidad, int FLG_CODIGO)
        {
            int _NextVal = 0; 
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try {
                // generar codigo expediente
                Cls_Ent_Dominio entidad_Correlativo = new Cls_Ent_Dominio();
                entidad_Correlativo.COD_DOMINIO = "NUM_EXP";
                Cls_Ent_Dominio _Correlativo = _cls_Serv_Dominio.Dominio_Listar_MaxId(entidad_Correlativo, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    }
                    else
                    {
                        if (_Correlativo != null)
                        {
                        if (FLG_CODIGO == 1) { entidad.COD_EXPEDIENTE = _Correlativo.DESC_CORTA_DOMINIO; }  // codigo_expediente
                            _NextVal = Convert.ToInt32(_Correlativo.DESC_LARGA_DOMINIO);
                        }
                        else
                        {
                            auditoria.Rechazar("Error en Generar Código Expediente: no generado");
                        }
                }
                if (!auditoria.RECHAZAR)
                {
                    entidad.ANIO_CODIGO = Convert.ToInt32(_Correlativo.COD_DOMINIO); // anio codigo expediente
                    entidad.IP_CREACION = Recursos.Clases.Css_IP.Obtener_IP();
                    _cls_Expedientes.Expedientes_Agregar_Expedientes(entidad, ref auditoria);
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        if (auditoria.RECHAZAR)
                            Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    }
                    else
                    {
                        if (!auditoria.RECHAZAR)
                        {
                            if (FLG_CODIGO == 1)
                            {
                                // actualizar codigo expediente nuevo correlativo
                                Cls_Ent_Dominio entidad_Dominio = new Cls_Ent_Dominio();
                                string CODIGO_EXP = "";
                                _NextVal = (_NextVal + 1);
                                var _COD = "000000" + _NextVal;
                                var IndexCount = Convert.ToString(_NextVal).Length;
                                if (IndexCount < 7)
                                {
                                    CODIGO_EXP = string.Format("{0}{1}", "EXP", _COD.Substring(IndexCount, 6));
                                    //entidad.COD_EXPEDIENTE = CODIGO_EXP;
                                }
                                else
                                {
                                    CODIGO_EXP = string.Format("{0}{1}", "EXP", _NextVal); 
                                }
                                entidad_Dominio.ID_DOMINIO = _Correlativo.ID_DOMINIO;
                                entidad_Dominio.COD_DOMINIO = _Correlativo.COD_DOMINIO;
                                entidad_Dominio.IP_MODIFICACION = Recursos.Clases.Css_IP.Obtener_IP();
                                entidad_Dominio.DESC_CORTA_DOMINIO = CODIGO_EXP;
                                entidad_Dominio.DESC_LARGA_DOMINIO = Convert.ToString(_NextVal);
                                _cls_Serv_Dominio.Dominio_Actualizar_Dominio(entidad_Dominio, ref auditoria);
                                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                                {
                                    if (auditoria.RECHAZAR)
                                        Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                                }
                            }
                        }
                    }
                }
            }
         catch (Exception ex)
        {
            var Mensaje = auditoria.MENSAJE_SALIDA = ex.Message;
            Recursos.Clases.Css_Log.Guardar(Mensaje);
        }

            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Expedientes_Estado(Cls_Ent_Expedientes entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            entidad.IP_MODIFICACION = Recursos.Clases.Css_IP.Obtener_IP();
            _cls_Expedientes.Expedientes_Estado(entidad, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (auditoria.RECHAZAR)
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Expedientes_Eliminar(Cls_Ent_Expedientes entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            entidad.IP_MODIFICACION = Recursos.Clases.Css_IP.Obtener_IP();
            _cls_Expedientes.Expedientes_Eliminar(entidad, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (auditoria.RECHAZAR)
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Expedientes_Actualizar(Cls_Ent_Expedientes entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            entidad.IP_MODIFICACION = Recursos.Clases.Css_IP.Obtener_IP();
            //string USU_MODIFICACION = "ADMIN ACTUALZA";
            _cls_Expedientes.Expedientes_Actualizar_Expedientes(entidad, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (auditoria.RECHAZAR)
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }

            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }


        public byte[] Foto(String SourceLoc)
        {
            FileStream fs = new FileStream(SourceLoc, FileMode.Open, FileAccess.Read);
            byte[] ImageData = new byte[fs.Length];
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return ImageData;

        }



    }
}
