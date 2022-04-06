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

namespace App_MEF_Expedientes.Controllers
{
    public class ModulosController : Controller
    {
        // GET: Modulos
        private ICls_Serv_Login _cls_Serv_Login;
        public ModulosController()
        {
            _cls_Serv_Login = new Cls_Serv_Login();
        }
        public ActionResult Modulos_Listar(Cls_Ent_Modulos entidad)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            entidad.ID_SISTEMA =  int.Parse(ConfigurationManager.AppSettings["Codigo_Sistema"].ToString());
            try
            {
                List<Cls_Ent_Modulos> MisModulos = new List<Cls_Ent_Modulos>(); 
                 MisModulos = Menu_Listar(entidad, ref auditoria);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    string CodigoLog = Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                    auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                }
                else
                {
                    string html = "";
                    Generar_Vista(MisModulos, ref html, 1);
                    auditoria.OBJETO = html;
                }

            }
            catch (Exception ex)
            {
                // Recursos.Clases.Css_Log.Guardar(ex.ToString());
                string CodigoLog = Recursos.Clases.Css_Log.Guardar(ex.ToString());
                auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
                return null;
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }

        public List<Cls_Ent_Modulos> Usuario_Modulos_Listar(Cls_Ent_Modulos entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                return _cls_Serv_Login.Modulos_Usuarios(entidad, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                return new List<Cls_Ent_Modulos>();
            }
        }

        public List<Cls_Ent_Modulos> Menu_Listar(Cls_Ent_Modulos entidad, ref Cls_Ent_Auditoria auditoria)
        {
            try
            {
                List<Cls_Ent_Modulos> _Menu = Usuario_Modulos_Listar(entidad, ref auditoria);
                return Menu_Ordenar(_Menu);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<Cls_Ent_Modulos> Menu_Ordenar(List<Cls_Ent_Modulos> MenuHijos)
        {
            List<Cls_Ent_Modulos> Menu_Retorno = new List<Cls_Ent_Modulos>();
            List<Cls_Ent_Modulos> MenuHijos_Base = MenuHijos.Where(item => item.ID_SISTEMA_MODULO_PADRE == 0).ToList();

            foreach (Cls_Ent_Modulos item in MenuHijos_Base)
            {
                Menu_Retorno.Add(item);
                Menu_Ordenar_Hijos(MenuHijos, item);
            }
            MenuHijos_Base.Clear();
            MenuHijos.Clear();
            return Menu_Retorno;
        }

        private void Menu_Ordenar_Hijos(List<Cls_Ent_Modulos> MenuHijos, Cls_Ent_Modulos MenuPadre)
        {
            MenuPadre.Modulos_Hijos = new List<Cls_Ent_Modulos>();

            foreach (Cls_Ent_Modulos MenuHijos_ in MenuHijos)
            {
                if (MenuHijos_.ID_SISTEMA_MODULO_PADRE == MenuPadre.ID_SISTEMA_MODULO)
                {
                    MenuPadre.Modulos_Hijos.Add(MenuHijos_);
                }
            }
        }

        public void Generar_Vista(List<Cls_Ent_Modulos> Menu_Lista, ref string menu, int nivel)
        {
            string cssUL = string.Empty;
            string cssLI = string.Empty;
            string vlInvocar = "InvocaReporte()";
            if (nivel == 1)
            {
                cssUL = "main-navigation-menu";
            }
            else
            {
                cssUL = "sub-menu";
            }
            //style=\"margin-top:15px\"
            if (nivel == 1)
            {
                menu = menu + "<ul id=\"ul_menuPrincipal\" class='" + cssUL + "' >";
                menu += "<li  class=\"active open\">"
                        + "	<a><i class=\"clip-list\"></i>"
                        + "	<span class=\"title\">Menú</span>"
                        + "	</a>"
                        + " </li>";
            }
            else
            {
                menu = menu + "<ul class='" + cssUL + "' >" + Convert.ToChar(13);
            }



            int cuenta = 0;
            foreach (Cls_Ent_Modulos _Menu_Lista in Menu_Lista)
            {
                cuenta++;
                //bool existe = MenuBuscarUrl(item);
                bool existe = true;
                string ls_style_li = "";
                if ((existe == true))
                {
                    if (nivel == 1 && cuenta == 1)
                        ls_style_li = "margin-top: 10px; border: 0px;  ";
                    //else
                    //    ls_style_li = "height:38px";

                    menu += "<li  id='" + _Menu_Lista.ID_LI + "' style='" + ls_style_li + "' > ";

                    if (_Menu_Lista.ID_TIPO_MODULO == 5)
                    {
                        menu += "<a style=\"height: 38px; padding-top: 9px;cursor:pointer\">";
                        menu += "<i class='" + _Menu_Lista.IMAGEN + "'></i>";
                        menu += "<span class='title'> " + _Menu_Lista.DESC_MODULO + "</span><i class='icon-arrow'></i><span  class='selected'></span>";
                        menu += "</a>";
                    }
                    else
                    {
                        menu += "<a id=" + _Menu_Lista.ID_A + " style=\"height: 35px; padding-top: 9px;cursor:pointer\" >";
                        menu += "<i class='" + _Menu_Lista.IMAGEN + "'></i>";
                        menu += "<span class='title'>" + _Menu_Lista.DESC_MODULO + "</span></a>";
                    }

                    if (_Menu_Lista.ID_TIPO_MODULO == 5)
                    {
                        if (_Menu_Lista.Modulos_Hijos != null)
                            if (_Menu_Lista.Modulos_Hijos.Count > 0)
                            {
                                Generar_Vista(_Menu_Lista.Modulos_Hijos, ref menu, 2);
                            }
                    }
                    menu += "</li>" + Convert.ToChar(13);
                }
            }
            menu += "</ul>" + Convert.ToChar(13);
        }



    }
}
