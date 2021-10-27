using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Configuration;
using System.Security.Claims;
using System.DirectoryServices; 

namespace App_MEF_Expedientes.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        private bool AuthenticateUser(string user, string password)
        {
            bool respuesta = false; 
            string ldap = ConfigurationManager.AppSettings["LDAP"].ToString();
            if (string.IsNullOrEmpty(ldap))
            {
                //.CreateLogger("LINK DE LDAP ESTA VACIO");
            }

   
            try
            {
                DirectoryEntry de = new DirectoryEntry("LDAP://" + ldap, user, password, System.DirectoryServices.AuthenticationTypes.Secure);
                DirectorySearcher ds = new DirectorySearcher(de);
                SearchResult result = ds.FindOne();
                respuesta = true;
                return respuesta;
            }
            catch (Exception ex)
            {
                string mensaje = "login==> " + ex.Message;
                mensaje += " si el error de codigo es : 525,52e,530,531,532,533,701,773,775";
                mensaje += " la explicacion del error en el  siguiente link";
                mensaje += " https://support.infrasightlabs.com/troubleshooting/common-error-codes-for-active-directory-authentication/";
                Log.CreateLogger(mensaje);
                respuesta.Success = false;
                respuesta.Message = "Usuario / Contraseña  es incorrecta";
            }
            return respuesta;
        }

    }
}
