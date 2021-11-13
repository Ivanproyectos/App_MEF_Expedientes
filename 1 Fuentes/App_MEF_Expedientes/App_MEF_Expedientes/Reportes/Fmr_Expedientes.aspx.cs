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
                string DNI = Request.QueryString["DNI"].ToString();
                long ID_SITUACION = long.Parse(Request.QueryString["ID_SITUACION"].ToString());
                long ID_ESTADO = long.Parse(Request.QueryString["ID_ESTADO"].ToString());
                int TIPO_REPORTE = int.Parse(Request.QueryString["TIPO_REPORTE"].ToString());
                long ID_SANCION = long.Parse(Request.QueryString["ID_SANCION"].ToString());

                GenerarReporte(NRO_EXPEDIENTE, DNI ,ID_SITUACION, ID_ESTADO, TIPO_REPORTE, ID_SANCION);

            }
        }

        private void GenerarReporte(string NRO_EXPEDIENTE, string DNI , long ID_SITUACION, long ID_ESTADO, long TIPO_REPORTE, long ID_SANCION)
        {

            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();

            try
            {
                if (TIPO_REPORTE == 1) {
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
                } else if (TIPO_REPORTE == 2) {

                    _cls_V_Serv_Expedientes = new Cls_V_Serv_Expedientes();
                    IEnumerable<Cls_v_Expedientes> lista = null;
                    Cls_v_Expedientes entidad = new Cls_v_Expedientes();
                    entidad.DNI= DNI;
                    entidad.ID_SITUACION = ID_SITUACION;
                    entidad.ID_ESTADO = ID_ESTADO;

                    ReportViewer1.LocalReport.DataSources.Clear();
                    lista = _cls_V_Serv_Expedientes.Expedientes_V_GetAll(ref auditoria, entidad);
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
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Expedientes_Listado.rdlc");
                            ReportParameter[] parameters = new ReportParameter[0];
                            //parameters[0] = new ReportParameter("ID_PERSONAL", entidad.id_cargo_digi.ToString());
                            ReportViewer1.LocalReport.SetParameters(parameters);
                            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Ds_LIstadoExpediente", lista));

                        }
                        this.ReportViewer1.LocalReport.Refresh();
                    }
                }
                else if (TIPO_REPORTE == 3)
                {

                    _cls_V_Serv_Expedientes = new Cls_V_Serv_Expedientes();
                    IEnumerable<Cls_v_Expedientes> lista = null;
                    Cls_v_Expedientes entidad = new Cls_v_Expedientes();
                    entidad.DNI = DNI;
                    entidad.ID_SITUACION = ID_SITUACION;
                    entidad.ID_ESTADO = ID_ESTADO;

                    ReportViewer1.LocalReport.DataSources.Clear();
                    lista = _cls_V_Serv_Expedientes.Expedientes_V_Sanciones(ref auditoria, entidad);
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
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Sanciones.rdlc");
                            ReportParameter[] parameters = new ReportParameter[0];
                            //parameters[0] = new ReportParameter("ID_PERSONAL", entidad.id_cargo_digi.ToString());
                            ReportViewer1.LocalReport.SetParameters(parameters);
                            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("Ds_Sanciones", lista));

                        }
                        this.ReportViewer1.LocalReport.Refresh();
                    }
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