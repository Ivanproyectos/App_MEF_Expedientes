using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_MEF_Expedientes.Areas.Administracion.Models;
using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Administracion;
using MEF.Expedientes.Service.Administracion;
using MEF.Expedientes.Contract.Administracion;

namespace App_MEF_Expedientes.Areas.Administracion.Controllers
{
    public class FormatosCorreoController : Controller
    {
        private ICls_Serv_FormatoCorreo _cls_Serv_FormatoCorreo;
        // GET: Administracion/FormatoCorreo
        public FormatosCorreoController()
        {
            _cls_Serv_FormatoCorreo = new Cls_Serv_FormatoCorreo();
        }
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Mantenimiento(int id, string Accion)
        {
            FormatosModelView modelo = new FormatosModelView
            {
                Accion = Accion,
                ID_FORMATO = id
            };


            if (Accion == "M")
            {
                IEnumerable<Cls_Ent_FormatoCorreo> lista;
                Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
                lista = _cls_Serv_FormatoCorreo.FormatoCorreo_Buscar(ref auditoria, id);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    if (auditoria.RECHAZAR)
                        Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                }
                else
                {
                    foreach (var item in lista)
                    {
                        modelo.ASUNTO = item.ASUNTO;
                        modelo.BODY = item.BODY;
                    }
                }
            }

            return View(modelo);
        }



        public ActionResult FormatoCorreo_Listar(Cls_Ent_FormatoCorreo entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            auditoria.Limpiar();
            IEnumerable<Cls_Ent_FormatoCorreo> lista = _cls_Serv_FormatoCorreo.FormatoCorreo_Listar(entidad, ref auditoria);
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

        public ActionResult FormatoCorreo_Insertar(Cls_Ent_FormatoCorreo entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            entidad.IP_CREACION = Recursos.Clases.Css_IP.Obtener_IP();
            _cls_Serv_FormatoCorreo.FormatoCorreo_Agregar_FormatoCorreo(entidad, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (auditoria.RECHAZAR)
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FormatoCorreo_Estado(Cls_Ent_FormatoCorreo entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            entidad.IP_MODIFICACION = Recursos.Clases.Css_IP.Obtener_IP();
            _cls_Serv_FormatoCorreo.FormatoCorreo_Estado(entidad, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (auditoria.RECHAZAR)
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FormatoCorreo_Eliminar(Cls_Ent_FormatoCorreo entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            entidad.IP_MODIFICACION = Recursos.Clases.Css_IP.Obtener_IP();
            _cls_Serv_FormatoCorreo.FormatoCorreo_Eliminar(entidad, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (auditoria.RECHAZAR)
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FormatoCorreo_Actualizar(Cls_Ent_FormatoCorreo entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            entidad.IP_MODIFICACION = Recursos.Clases.Css_IP.Obtener_IP();
            //string USU_MODIFICACION = "ADMIN ACTUALZA";
            _cls_Serv_FormatoCorreo.FormatoCorreo_Actualizar_FormatoCorreo(entidad, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (auditoria.RECHAZAR)
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }

            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }





    }
}
