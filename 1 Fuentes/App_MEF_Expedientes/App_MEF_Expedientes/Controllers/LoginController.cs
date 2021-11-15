using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Configuration;
using System.Security.Claims;
using System.DirectoryServices;
using MEF.Expedientes.Entity; 


namespace App_MEF_Expedientes.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Validar_usuario(string USUARIO, string CLAVE)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                auditoria.Limpiar();
                bool autorizado = AuthenticateUser(USUARIO, CLAVE, ref auditoria);
                if (autorizado)
                {
                    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                    {
                        Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    }
                    else
                    {
                        if (!auditoria.RECHAZAR)
                        {
                            string ENCRIPTADO = Recursos.Clases.Css_Encriptar.Encrypt(USUARIO);
                            auditoria.OBJETO = ENCRIPTADO;
                            Session["USUARIO"] = ENCRIPTADO;
                        }
                    }
                }
                else {
                   auditoria.Rechazar("Usuario / Contraseña  es incorrecta.");
                }

            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }



        private bool AuthenticateUser(string user, string password, ref Cls_Ent_Auditoria auditoria)
        {
            bool respuesta = false;
            Recursos.Clases.Css_ActiveDirectory  active = new Recursos.Clases.Css_ActiveDirectory();
            try
            {
                respuesta =  active.ValidateCredentials(user, password); 
            }
            catch (Exception ex)
            {   
                //auditoria.Rechazar("Usuario / Contraseña  es incorrecta.");
                Recursos.Clases.Css_Log.Guardar(ex.Message); 
            }
            return respuesta;
        }


        public ActionResult CerrarSession()
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            auditoria.Limpiar();
            Session["USUARIO"] = null;
            return RedirectToAction("Index", "Home");
        }


    




    }
}
