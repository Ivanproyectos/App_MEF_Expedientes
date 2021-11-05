
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEF.Expedientes.Entity.Maestras;
using MEF.Expedientes.Entity.Maestras.Vistas;
using MEF.Expedientes.Service.Maestras;
using MEF.Expedientes.Service.Maestras.Vista;
using MEF.Expedientes.Contract.Maestras;
using MEF.Expedientes.Contract.Maestras.Vista;
using MEF.Expedientes.Entity;

namespace App_MEF_Expedientes.Recursos.Form
{
    public partial class DescargarAdjuntos : System.Web.UI.Page
    {
        private ICls_Serv_Adjuntos _cls_Serv_Adjuntos;
        protected void Page_Load(object sender, EventArgs e)
        {
            _cls_Serv_Adjuntos = new Cls_Serv_Adjuntos();
            if (!IsPostBack)
            {
                int ID_ARCHIVO = int.Parse(Request.QueryString["ID_ARCHIVO"]);
                Descargar(ID_ARCHIVO);
            }
        }
            private void Descargar(int ID_ARCHIVO)
            {
                Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
                try
                {
                IEnumerable<Cls_Ent_Adjuntos> lista;
                lista = _cls_Serv_Adjuntos.Adjuntos_Buscar(ref auditoria, ID_ARCHIVO);
        
                if (auditoria.EJECUCION_PROCEDIMIENTO)
                  {
                    if (!auditoria.RECHAZAR)
                    {
                        var Archivo_ = lista.First(); 

                        byte[] bytes = Archivo_.ARCHIVO_BLOB;
                        Response.Clear();
                        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", Archivo_.NOMBRE_ARCHIVO + Archivo_.EXTENSION_ARCHIVO));
                        Response.ContentType = "application/octet-stream";
                        Response.BinaryWrite(bytes);
                        Response.End();
                        Response.Close();

                     }
                }
                else
                {
                    if (auditoria.RECHAZAR)
                        Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                }

                }
                catch (Exception ex)
                {
                    //auditoria.Error(ex);
                }
            }

        
    }
}