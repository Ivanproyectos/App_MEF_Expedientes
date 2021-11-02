
using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras;
using MEF.Expedientes.Service.Maestras;
using MEF.Expedientes.Contract.Maestras;
using System.Collections.Generic;
using System.Web.Mvc;
using App_MEF_Expedientes.Areas.Maestras.Models;
using MEF.Expedientes.Entity.Administracion;
using MEF.Expedientes.Service.Administracion;
using MEF.Expedientes.Contract.Administracion;
using System.Data;
using System.Linq;

namespace App_MEF_Expedientes.Areas.Maestras.Controllers
{
    public class ExpedientesController : Controller
    {
        // GET: Maestras/Expedientes
        private ICls_Serv_Oficina _cls_Serv_Oficina;
        private ICls_Serv_Personal _cls_Serv_Personal;
        private ICls_Serv_Expedientes _cls_Expedientes;
        private ICls_Serv_Dominio _cls_Serv_Dominio;


        public ExpedientesController()
        {
            _cls_Serv_Oficina = new Cls_Serv_Oficina();
            _cls_Serv_Personal = new Cls_Serv_Personal();
            _cls_Expedientes = new Cls_Serv_Expedientes();
            _cls_Serv_Dominio = new Cls_Serv_Dominio();
        }


        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult Expedientes_Codigo_Listar()
        //{
        //    Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
        //    auditoria.Limpiar();
        //    Cls_Ent_Expedientes lista = _cls_Expedientes.Expedientes_Codigo_Listar( ref auditoria);
        //    if (!auditoria.EJECUCION_PROCEDIMIENTO)
        //    {
        //        Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
        //    }
        //    else
        //    {
        //        auditoria.OBJETO = lista;
        //    }
        //    return Json(auditoria.OBJETO, JsonRequestBehavior.AllowGet);
        //}


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
                    modelo.Lista_Acto.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                }
            }
            else
            {
                modelo.Lista_Acto.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }


            Cls_Ent_Dominio entidad_precalificacion = new Cls_Ent_Dominio();
            entidad_precalificacion.NOM_DOMINIO = "TIPACT";
            entidad_precalificacion.FLG_ESTADO = 1;
            var Lista_precalificacion = _cls_Serv_Dominio.Dominio_Listar(entidad_precalificacion, ref auditoria);
            if (auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (!auditoria.RECHAZAR)
                {
                    modelo.Lista_Precalifacion = listaActo.Select(x => new SelectListItem()
                    {
                        Text = x.DESC_CORTA_DOMINIO.ToString(),
                        Value = x.ID_DOMINIO.ToString()
                    }).ToList();
                    modelo.Lista_Precalifacion.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                }
            }
            else
            {
                modelo.Lista_Precalifacion.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
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
                    modelo.Lista_Estado.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                }
            }
            else
            {
                modelo.Lista_Estado.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }



            Cls_Ent_Dominio entidad_Situacion= new Cls_Ent_Dominio();
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




            Cls_Ent_Dominio entidad_Sancion = new Cls_Ent_Dominio();
            entidad_Sancion.NOM_DOMINIO = "TIPSAN";
            entidad_Sancion.FLG_ESTADO = 1;
            var Lista_Sancion= _cls_Serv_Dominio.Dominio_Listar(entidad_Sancion, ref auditoria);
            if (auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (!auditoria.RECHAZAR)
                {
                    modelo.Lista_Sacion_Recomendada = Lista_Sancion.Select(x => new SelectListItem()
                    {
                        Text = x.DESC_CORTA_DOMINIO.ToString(),
                        Value = x.ID_DOMINIO.ToString()
                    }).ToList();
                    modelo.Lista_Sacion_Recomendada.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                }
            }
            else
            {
                modelo.Lista_Sacion_Recomendada.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });
                Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }

      

            return View(modelo);
        }



        public ActionResult Expedientes_Insertar(Cls_Ent_Expedientes entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            entidad.IP_CREACION = Recursos.Clases.Css_IP.Obtener_IP();
            _cls_Expedientes.Expedientes_Agregar_Expedientes(entidad, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (auditoria.RECHAZAR)
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
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






    }
}
