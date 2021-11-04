using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras;
using MEF.Expedientes.Entity.Maestras.Vistas;
using MEF.Expedientes.Service.Maestras;
using MEF.Expedientes.Service.Maestras.Vista;
using MEF.Expedientes.Contract.Maestras;
using MEF.Expedientes.Contract.Maestras.Vista;
using Microsoft.Reporting.WebForms;

namespace App_MEF_Expedientes.Reportes
{
    public partial class Expedientes : System.Web.UI.Page
    {
        private ICls_V_Serv_Expedientes _cls_V_Serv_Expedientes;


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string NRO_EXPEDIENTE = Request.QueryString["NRO_EXPEDIENTE"].ToString();
                GenerarReporte(NRO_EXPEDIENTE);

            }
        }

        private void GenerarReporte(string NRO_EXPEDIENTE)
        {

            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();

            try
            {
                _cls_V_Serv_Expedientes = new Cls_V_Serv_Expedientes();

                IEnumerable<Cls_v_Expedientes> lista = null;


                Cls_v_Expedientes entidad = new Cls_v_Expedientes();
                entidad.COD_EXPEDIENTE = NRO_EXPEDIENTE;
       
                ReportViewer1.LocalReport.DataSources.Clear();
                lista = _cls_V_Serv_Expedientes.Expedientes_V_Buscar(ref auditoria, entidad);
                if (!auditoria.EJECUCION_PROCEDIMIENTO)
                {
                    Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
                }
                else
                {

               
                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    if (lista.Count() > 0)
                    {
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("Expedientes.rdlc");
                        ReportParameter[] parameters = new ReportParameter[0];
                        //parameters[0] = new ReportParameter("ID_PERSONAL", entidad.id_cargo_digi.ToString());
                        ReportViewer1.LocalReport.SetParameters(parameters);

                        ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Ds_Expedientes", lista));
                  
                    }
                    this.ReportViewer1.LocalReport.Refresh();

                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
        }





    }
}