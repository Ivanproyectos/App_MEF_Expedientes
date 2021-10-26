using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_MEF_Expedientes.Areas.Maestras.Models; 

namespace App_MEF_Expedientes.Areas.Maestras.Controllers
{
    public class ExpedientesController : Controller
    {
        // GET: Maestras/Expedientes
        public ActionResult Index()
        {
            return View();
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
