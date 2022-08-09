
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
    public class AdjuntosController : Controller
    {
        // GET: Maestras/Adjuntos

        private ICls_Serv_Adjuntos _cls_Serv_Adjuntos;
        private ICls_V_Serv_Adjuntos _cls_V_Serv_Adjuntos;
        public AdjuntosController()
        {
            _cls_Serv_Adjuntos = new Cls_Serv_Adjuntos();
            _cls_V_Serv_Adjuntos = new Cls_V_Serv_Adjuntos();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Adjuntos_Listar(Cls_v_Adjuntos entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            auditoria.Limpiar();
            entidad.ID_SISTEMA = int.Parse(ConfigurationManager.AppSettings["Codigo_Sistema"].ToString()); 
            IEnumerable<Cls_v_Adjuntos> lista = _cls_V_Serv_Adjuntos.Adjuntos_Listar(entidad, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
            else
            {
                auditoria.OBJETO = lista;
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }


        
        public ActionResult Mantenimiento_Ver(int id)
        {
            AdjuntosModelView model = new AdjuntosModelView();
            model.ID_ADJUNTO = id;
            IEnumerable<Cls_Ent_Adjuntos> lista;
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            lista = _cls_Serv_Adjuntos.Adjuntos_Buscar(ref auditoria, id);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (auditoria.RECHAZAR)
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
            else
            {
                foreach (var item in lista)
                {
                    model.MiArchivo = new Cls_Ent_Archivo(); 
                    string ID_UNICO_TEMP = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal();
                    var ruta_temporal = Recursos.Clases.Css_Ruta.Ruta_Temporal() + ID_UNICO_TEMP + item.EXTENSION_ARCHIVO;
                    System.IO.File.Create(ruta_temporal).Close();
                    System.IO.File.WriteAllBytes(ruta_temporal, item.ARCHIVO_BLOB);
                    model.MiArchivo.CODIGO_ARCHIVO = ID_UNICO_TEMP;
                    model.MiArchivo.EXTENSION = item.EXTENSION_ARCHIVO;
                }
            }
            return View(model);
        }



        public ActionResult Descargar_Archivo(int id)
        {
            AdjuntosModelView model = new AdjuntosModelView();
            model.ID_ADJUNTO = id;
            return View(model);
        }


      
        public ActionResult Adjuntos_Insertar(AdjuntosModelView param)
        {

            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            var ruta_temporal = Recursos.Clases.Css_Ruta.Ruta_Temporal() + param.MiArchivo.CODIGO_ARCHIVO + param.MiArchivo.EXTENSION;
            if (System.IO.File.Exists(ruta_temporal))
            {
                byte[] ARCHIVO_BLOB = Foto(ruta_temporal);
                System.IO.File.Delete(ruta_temporal);
                Cls_Ent_Adjuntos entidad = new Cls_Ent_Adjuntos
                {
                    ID_MAESTRA = param.ID_MAESTRA,
                    ID_TIPO_ARCHIVO = param.ID_TIPO_ARCHIVO,
                    OBSERVACION = param.OBSERVACION,
                    FECHA = DateTime.Parse(param.FECHA),
                    NUMERO = param.NUMERO,
                    ARCHIVO_BLOB = ARCHIVO_BLOB,
                    ID_SISTEMA = int.Parse(ConfigurationManager.AppSettings["Codigo_Sistema"].ToString()),
                    EXTENSION_ARCHIVO = param.MiArchivo.EXTENSION,
                    NOMBRE_ARCHIVO = param.MiArchivo.NOMBRE_ARCHIVO,
                    USU_CREACION = param.USU_CREACION
                };

                entidad.IP_CREACION = Recursos.Clases.Css_IP.Obtener_IP();
                _cls_Serv_Adjuntos.Adjuntos_Agregar_Adjuntos(entidad, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    if (auditoria.RECHAZAR)
                        Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                }

            }
            else {
                auditoria.Rechazar("Archivo no existe");
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

        public ActionResult Adjuntos_Eliminar(Cls_Ent_Adjuntos entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            _cls_Serv_Adjuntos.Adjuntos_Eliminar(entidad, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (auditoria.RECHAZAR)
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }







    }
}
