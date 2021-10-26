using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF.Expedientes.Entity
{
    public class Cls_Ent_Auditoria
    {

        public object OBJETO { get; set; }
        public string MENSAJE_SALIDA { get; set; }
        public string ERROR_LOG { get; set; }
        public bool EJECUCION_PROCEDIMIENTO { get; set; }
        public bool AUTORIZADO { get; set; }
        public string URL { get; set; }

        public bool RECHAZAR { get; set; } // Auxiliar
        public bool RECHAZAR2 { get; set; } // Auxiliar

        public void NoAutorizado(string URL_)
        {
            OBJETO = null;
            URL = URL_;
            MENSAJE_SALIDA = "";
            ERROR_LOG = "";
            AUTORIZADO = false;
            RECHAZAR = false;
            EJECUCION_PROCEDIMIENTO = true;
        }

        public void Limpiar()
        {
            OBJETO = null;
            MENSAJE_SALIDA = "";
            ERROR_LOG = "";
            RECHAZAR = false;
            AUTORIZADO = true;
            EJECUCION_PROCEDIMIENTO = true;
        }

        public void Rechazar(String mensaje)
        {
            OBJETO = null;
            MENSAJE_SALIDA = mensaje;
            ERROR_LOG = mensaje;
            RECHAZAR = true;
            AUTORIZADO = true;
            EJECUCION_PROCEDIMIENTO = true;
        }

        public void Error(Exception ex)
        {
            OBJETO = null;
            MENSAJE_SALIDA = ex.Message;
            ERROR_LOG = ex.ToString();
            RECHAZAR = true;
            AUTORIZADO = true;
            EJECUCION_PROCEDIMIENTO = false;
        }

    }
}
