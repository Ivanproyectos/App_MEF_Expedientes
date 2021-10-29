﻿using System;
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
                //bool autorizado = AuthenticateUser(USUARIO, CLAVE, ref auditoria);
                //if (autorizado)
                //{
                //    //int ID_SISTEMA = int.Parse(ConfigurationManager.AppSettings["Codigo_Sistema"].ToString());
                //    if (!auditoria.EJECUCION_PROCEDIMIENTO)
                //    {
                //        Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                //    }
                //    else
                //    {
                //        if (!auditoria.RECHAZAR)
                //        {
                string ENCRIPTADO = Recursos.Clases.Css_Encriptar.Encrypt(USUARIO);
                auditoria.OBJETO = ENCRIPTADO;
                //        }
                //    }
                //}

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
            string ldap = ConfigurationManager.AppSettings["LDAP"].ToString();

            try
            {
                if (string.IsNullOrEmpty(ldap))
                {
                    auditoria.Rechazar("Configuración active directory vacio.");
                }
                else { 
                DirectoryEntry de = new DirectoryEntry("LDAP://" + ldap, user, password, System.DirectoryServices.AuthenticationTypes.Secure);
                DirectorySearcher ds = new DirectorySearcher(de);
                    ds.Filter = "(SAMAccountName=" + user + ")";
                    ds.PropertiesToLoad.Add("cn");
                    SearchResult result = ds.FindOne();
                 respuesta = true;
                //return respuesta;
               }
            }
            catch (Exception ex)
            {
                string mensaje = "login==> " + ex.Message;
                mensaje += " si el error de codigo es : 525,52e,530,531,532,533,701,773,775";
                mensaje += " la explicacion del error en el  siguiente link";
                mensaje += " https://support.infrasightlabs.com/troubleshooting/common-error-codes-for-active-directory-authentication/";
                respuesta = false;         
                auditoria.Rechazar("Usuario / Contraseña  es incorrecta.");
                Recursos.Clases.Css_Log.Guardar(ex.Message); 
            }
            return respuesta;
        }




        //public bool CheckUserInGroup(string group)

        //{

        //    string serverName = ConfigurationManager.AppSettings["ADServer"];

        //    string userName = ConfigurationManager.AppSettings["ADUserName"];

        //    string password = ConfigurationManager.AppSettings["ADPassword"];

        //    bool result = false;

        //    SecureString securePwd = null;

        //    if (password != null)

        //    {

        //        securePwd = new SecureString();

        //        foreach (char chr in password.ToCharArray())

        //        {

        //            securePwd.AppendChar(chr);

        //        }

        //    }

        //    try

        //    {

        //        ActiveDirectory adConnectGroup = new ActiveDirectory(serverName, userName, securePwd);

        //        SearchResultEntry groupResult = adConnectGroup.GetEntryByCommonName(group);

        //        Group grp = new Group(adConnectGroup, groupResult);

        //        SecurityPrincipal userPrincipal = grp.Members.Find(sp => sp.SAMAccountName.ToLower() == User.Identity.Name.ToLower());

        //        if (userPrincipal != null)

        //        {

        //            result = true;

        //        }

        //    }

        //    catch

        //    {

        //        result = false;

        //    }

        //    return result;

        //}

    }
}