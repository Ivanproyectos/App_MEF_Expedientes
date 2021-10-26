﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_MEF_Expedientes.Areas.Administracion.Models;

namespace App_MEF_Expedientes.Areas.Administracion.Controllers
{
    public class TipoArchivoController : Controller
    {
        // GET: Administracion/TipoArchivo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mantenimiento(int id, string Accion)
        {
            DominioModelView modelo = new DominioModelView();
            modelo.ID_DOMINIO = id;
            modelo.Accion = Accion;


            return View(modelo);
        }


    }
}
