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


            return View(modelo);
        }


    }
}
