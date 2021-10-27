using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_MEF_Expedientes.Recursos.Clases
{
    public class Css_Codigo
    {
        public static string Generar_Codigo_Temporal()
        {
            string CODIGO_TEMPORAL = Guid.NewGuid().ToString();

            return CODIGO_TEMPORAL;
        }
    }
}