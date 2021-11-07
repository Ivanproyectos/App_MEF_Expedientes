using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Login;
using MEF.Expedientes.Service.Login;
using MEF.Expedientes.Contract.Login;
using System.Configuration;
//using MEF.Expedientes.Entity;

namespace App_MEF_Expedientes.Controllers
{
    public class HomeController : Controller
    {
        private ICls_Serv_Login _cls_Serv_Login;
        public HomeController()
        {
            _cls_Serv_Login = new Cls_Serv_Login();
        }

        public ActionResult Index(string usuario)
        {

            
            try
            {
                bool Valido = false;
                string DESENCRIPTADO = Recursos.Clases.Css_Encriptar.Decrypt(usuario);
                int ID_SISTEMA = int.Parse(ConfigurationManager.AppSettings["Codigo_Sistema"].ToString());

                Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
                Cls_Ent_Usuario MiUsuario = new Cls_Ent_Usuario(); 
                MiUsuario = _cls_Serv_Login.Usuario( new Cls_Ent_Usuario { LOGIN_USUARIO = DESENCRIPTADO, ID_SISTEMA = ID_SISTEMA }, ref auditoria);
                if (MiUsuario != null)
                {
                    @ViewBag.Usuario_Nombre = MiUsuario.NOMBRE_PERSONA;
                    @ViewBag.Usuario_Codigo = MiUsuario.LOGIN_USUARIO;
                    @ViewBag.Desc_Oficina = MiUsuario.NOMBRE_OFICINA;
                    @ViewBag.Id_Usuario = MiUsuario.ID_USUARIO;
                    ViewData["Seg_Perfiles"] = MiUsuario.Perfil;
                    Valido = true;
                }
                if (!Valido)
                {

                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                string CodigoLog = Recursos.Clases.Css_Log.Guardar(ex.ToString());
                return View();
            }
        }

    
    }
}
