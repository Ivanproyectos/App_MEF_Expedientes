using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_MEF_Expedientes.Areas.Maestras.Models;
using MEF.Expedientes.Entity.Administracion;
using MEF.Expedientes.Service.Administracion;
using MEF.Expedientes.Contract.Administracion;
using MEF.Expedientes.Entity;

namespace App_MEF_Expedientes.Areas.Reportes.Controllers
{
    public class ExpedientesController : Controller
    {
        private ICls_Serv_Dominio _cls_Serv_Dominio;
        // GET: Reportes/Reportes
        public ExpedientesController()
        {

            _cls_Serv_Dominio = new Cls_Serv_Dominio();
        
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


            Cls_Ent_Dominio entidad_CorreEx = new Cls_Ent_Dominio();
            entidad_CorreEx.NOM_DOMINIO = "NUM_EXP";
            entidad_CorreEx.FLG_ESTADO = 1;
            var Lista_CorrelativoEx = _cls_Serv_Dominio.Dominio_Listar(entidad_CorreEx, ref auditoria);
            if (auditoria.EJECUCION_PROCEDIMIENTO)
            {
                if (!auditoria.RECHAZAR)
                {
                    modelo.Lista_CorrelativoEx = Lista_CorrelativoEx.Select(x => new SelectListItem()
                    {
                        Text = x.COD_DOMINIO.ToString(),
                        Value = x.COD_DOMINIO.ToString()
                    }).ToList();
                    //modelo.Lista_CorrelativoEx.Insert(0, new SelectListItem() { Value = "0", Text = "-- Seleccione --" });
                }
            }
            else
            {
                modelo.Lista_CorrelativoEx.Insert(0, new SelectListItem() { Value = "0", Text = "-- Seleccione --" });
                Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
            




            return View(modelo);
        }

     
    }
}
