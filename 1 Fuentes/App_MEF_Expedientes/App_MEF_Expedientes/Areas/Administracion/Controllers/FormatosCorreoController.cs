using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_MEF_Expedientes.Areas.Administracion.Models;

namespace App_MEF_Expedientes.Areas.Administracion.Controllers
{
    public class FormatosCorreoController : Controller
    {
        // GET: Administracion/FormatosCorreo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mantenimiento(int id, string Accion)
        {
            FormatosModelView modelo = new FormatosModelView();
            modelo.ID_FORMATO = id;
            modelo.Accion = Accion;


            return View(modelo);
        }



    }
}
