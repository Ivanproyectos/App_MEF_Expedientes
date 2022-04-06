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
    public class Correlativo_ExpedienteController : Controller
    {
        // GET: Administracion/Correlativo_Expediente
        private ICls_Serv_Dominio _cls_Serv_Dominio;

        public Correlativo_ExpedienteController()
        {
            _cls_Serv_Dominio = new Cls_Serv_Dominio();
        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Mantenimiento(int id, string Accion)
        {
            DominioModelView modelo = new DominioModelView
            {
                Accion = Accion,
                ID_DOMINIO = id
            };


            if (Accion == "M")
            {
                IEnumerable<Cls_Ent_Dominio> lista;
                Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
                lista = _cls_Serv_Dominio.Dominio_Buscar(ref auditoria, id);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    if (auditoria.RECHAZAR)
                        Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                }
                else
                {
                    foreach (var item in lista)
                    {
                        modelo.ANIO = Convert.ToInt32(item.COD_DOMINIO);
                        modelo.NUMERO_EXPEDIENTE = item.DESC_CORTA_DOMINIO;
                        modelo.CORRELATIVO = Convert.ToInt32(item.DESC_LARGA_DOMINIO);
                    }
                }
            }

            return View(modelo);
        }
        public ActionResult Dominio_Listar_MaxId()
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            auditoria.Limpiar();
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
                    auditoria.OBJETO = _Correlativo.DESC_CORTA_DOMINIO;
                }else
                {
                    auditoria.Rechazar("Codigo Expediente no registrado, comuniquese con el administrador del sistema."); 
                }
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }
       public ActionResult Correlativo_Expediente_Listar(Cls_Ent_Dominio entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            auditoria.Limpiar();
            IEnumerable<Cls_Ent_Dominio> lista = _cls_Serv_Dominio.Dominio_Listar(entidad, ref auditoria);
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

        public ActionResult Correlativo_Expediente_Insertar(Cls_Ent_Dominio entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            entidad.IP_CREACION = Recursos.Clases.Css_IP.Obtener_IP();
            _cls_Serv_Dominio.Dominio_Agregar_Dominio(entidad, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (auditoria.RECHAZAR)
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Correlativo_Expediente_Correlativo_Expediente(Cls_Ent_Dominio entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            entidad.IP_MODIFICACION = Recursos.Clases.Css_IP.Obtener_IP();
            _cls_Serv_Dominio.Dominio_Estado(entidad, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (auditoria.RECHAZAR)
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Correlativo_Expediente_Eliminar(Cls_Ent_Dominio entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            entidad.IP_MODIFICACION = Recursos.Clases.Css_IP.Obtener_IP();
            _cls_Serv_Dominio.Dominio_Eliminar(entidad, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (auditoria.RECHAZAR)
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Correlativo_Expediente_Actualizar(Cls_Ent_Dominio entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            entidad.IP_MODIFICACION = Recursos.Clases.Css_IP.Obtener_IP();
            //string USU_MODIFICACION = "ADMIN ACTUALZA";
            _cls_Serv_Dominio.Dominio_Actualizar_Dominio(entidad, ref auditoria);
            if (!auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (auditoria.RECHAZAR)
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }

            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }


    }
}
