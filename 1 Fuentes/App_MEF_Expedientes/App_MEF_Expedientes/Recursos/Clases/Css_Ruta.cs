using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace App_MEF_Expedientes.Recursos.Clases
{
    public class Css_Ruta
    {

        public static string Ruta_Temporal()
        {
            string ruta = "";
            ruta = ConfigurationManager.AppSettings["Servidor_Temporales"].ToString();
            if (ruta == "")
            {
                ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"Recursos\Temporales\");
            }
            return ruta;
        }


        public static string Ruta_Historicos()
        {
            string ruta = "";
            ruta = ConfigurationManager.AppSettings["Servidor_Historicos"].ToString();
            if (ruta == "")
            {
                ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"Recursos\Historicos\");
            }
            return ruta;
        }


    }
}