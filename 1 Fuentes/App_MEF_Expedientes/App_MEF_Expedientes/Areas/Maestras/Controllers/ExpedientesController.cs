
using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras;
using MEF.Expedientes.Service.Maestras;
using MEF.Expedientes.Contract.Maestras;
using System.Collections.Generic;
using System.Web.Mvc;
using App_MEF_Expedientes.Areas.Maestras.Models; 

namespace App_MEF_Expedientes.Areas.Maestras.Controllers
{
    public class ExpedientesController : Controller
    {
        // GET: Maestras/Expedientes
        private ICls_Serv_Oficina _cls_Serv_Oficina;
        private ICls_Serv_Personal _cls_Serv_Personal;
        private ICls_Serv_Expedientes _cls_Expedientes; 
        
        public ExpedientesController()
        {
            _cls_Serv_Oficina = new Cls_Serv_Oficina();
            _cls_Serv_Personal = new Cls_Serv_Personal();
            _cls_Expedientes = new Cls_Serv_Expedientes();
        }


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Expedientes_Codigo_Listar()
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            auditoria.Limpiar();
            Cls_Ent_Expedientes lista = _cls_Expedientes.Expedientes_Codigo_Listar( ref auditoria);
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
            modelo.ID_EXPEDIENTE = id;
            modelo.Accion = Accion;


            modelo.Lista_Acto = new List<SelectListItem>();
            modelo.Lista_Acto.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });

            modelo.Lista_Precalifacion = new List<SelectListItem>();
            modelo.Lista_Precalifacion.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });


            modelo.Lista_Situacion = new List<SelectListItem>();
            modelo.Lista_Situacion.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });


            modelo.Lista_Estado = new List<SelectListItem>();
            modelo.Lista_Estado.Insert(0, new SelectListItem() { Value = "", Text = "-- Seleccione --" });


            return View(modelo);
        }


    }
}
