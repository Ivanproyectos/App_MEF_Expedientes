using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras.Vistas;
using MEF.Expedientes.Repository;
using MEF.Expedientes.Contract.Maestras.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace MEF.Expedientes.Service.Maestras.Vista
{
   public  class Cls_V_Serv_Expedientes : Repository<Cls_v_Expedientes>, ICls_V_Serv_Expedientes
    {

        public List<Cls_v_Expedientes> Expedientes_Listar_Todo(string ORDEN_COLUMNA, string ORDEN, int FILAS, int PAGINA, string @WHERE, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            System.Data.Common.DbDataReader dr = null;
            List<Cls_v_Expedientes> lista = new List<Cls_v_Expedientes>();
            try
            {
                using (var cmd = _context.GetStoredProcedureCommand("PCK_EXPEDIENTES.USP_EXPEDIENTE_PAGINACION",
                new OracleParameter("PI_PAGINA", PAGINA),
                new OracleParameter("PI_NROREGISTROS", FILAS),
                new OracleParameter("PI_ORDEN_COLUMNA", ORDEN_COLUMNA),
                new OracleParameter("PI_ORDEN", ORDEN),
                new OracleParameter("PI_WHERE", @WHERE),
                new OracleParameter("PO_CUENTA", OracleDbType.Int32, ParameterDirection.Output),
                null))
                {
                    try
                    {
                        dr = cmd.ExecuteReader();
                        int CUENTA = int.Parse(cmd.Parameters["PO_CUENTA"].Value.ToString());
                        auditoria.OBJETO = CUENTA;
                        int pos_ID_EXPEDIENTE = dr.GetOrdinal("ID_EXPEDIENTE");
                        int pos_ID_PERSONALO = dr.GetOrdinal("ID_PERSONAL");
                        int pos_NOMBRE_COMPLETO = dr.GetOrdinal("NOMBRE_COMPLETO");
                        int pos_REGIMEN_LABORAL = dr.GetOrdinal("REGIMEN_LABORAL");
                        int pos_OFICINA = dr.GetOrdinal("OFICINA");         
                        int pos_COD_EXPEDIENTE = dr.GetOrdinal("COD_EXPEDIENTE");
                        int pos_FECHA_RECEPCION = dr.GetOrdinal("FECHA_RECEPCION");
                        int pos_FECHA_PRESCRIPCION = dr.GetOrdinal("FECHA_PRESCRIPCION");
                        int pos_FECHA_HECHO = dr.GetOrdinal("FECHA_HECHO");
                        int pos_HOJA_RUTA = dr.GetOrdinal("HOJA_RUTA");
                        int pos_REMITENTE = dr.GetOrdinal("REMITENTE");
                        int pos_ACTO = dr.GetOrdinal("ACTO");
                        int pos_OBSERVACION_INVESTIGADORA = dr.GetOrdinal("OBSERVACION_INVESTIGADORA");
                        int pos_ARTICULO = dr.GetOrdinal("ARTICULO");
                        int pos_INC = dr.GetOrdinal("INC");
                        int pos_OBSERVACION_INSTRUCTORA = dr.GetOrdinal("OBSERVACION_INSTRUCTORA");
                        int pos_TIPO_SANCION_RECOMENDADA = dr.GetOrdinal("TIPO_SANCION_RECOMENDADA");
                        int pos_ACTO_INICIO = dr.GetOrdinal("ACTO_INICIO");
                        int pos_FECHA_NOTIFICACION = dr.GetOrdinal("FECHA_NOTIFICACION");
                        int pos_RECOMENDACION_PREINFORME = dr.GetOrdinal("RECOMENDACION_PREINFORME");
                        int pos_SANCION = dr.GetOrdinal("SANCION");
                        int pos_SANCION_RECOMENDADA = dr.GetOrdinal("SANCION_RECOMENDADA");        
                        int pos_ORGANO_INSTRUCTOR = dr.GetOrdinal("ORGANO_INSTRUCTOR");
                        int pos_FECHA_NOTIFICACION_INICIO = dr.GetOrdinal("FECHA_NOTIFICACION_INICIO");
                        int pos_DOCUMENTO_FINALIZACION = dr.GetOrdinal("DOCUMENTO_FINALIZACION");
                        int pos_RECOMENDACION_INSTRUCTOR = dr.GetOrdinal("RECOMENDACION_INSTRUCTOR");
                        int pos_ORGANO_SANCIONADOR = dr.GetOrdinal("ORGANO_SANCIONADOR");
                        int pos_OBSERVACION_SANCIONADORA = dr.GetOrdinal("OBSERVACION_SANCIONADORA");
                        int pos_SITUACION = dr.GetOrdinal("SITUACION");
                        int pos_ESTADO = dr.GetOrdinal("ESTADO");
                        int pos_FEC_CREACION = dr.GetOrdinal("FEC_CREACION");
                        int pos_FLG_ESTADO = dr.GetOrdinal("FLG_ESTADO");
                        int pos_USU_CREACION = dr.GetOrdinal("USU_CREACION");
                        int pos_USU_MODIFICACION = dr.GetOrdinal("USU_MODIFICACION");
                        int pos_FEC_MODIFICACION = dr.GetOrdinal("FEC_MODIFICACION");
                        int pos_FALTA = dr.GetOrdinal("FALTA");


                        if (dr.HasRows)
                            while (dr.Read())
                            {
                                Cls_v_Expedientes entidad = new Cls_v_Expedientes();

                                if (dr.IsDBNull(pos_ID_EXPEDIENTE)) entidad.ID_EXPEDIENTE = 0;
                                else entidad.ID_EXPEDIENTE = long.Parse(dr[pos_ID_EXPEDIENTE].ToString());

                                if (dr.IsDBNull(pos_NOMBRE_COMPLETO)) entidad.NOMBRE_COMPLETO = "";
                                else entidad.NOMBRE_COMPLETO = dr.GetString(pos_NOMBRE_COMPLETO);

                                if (dr.IsDBNull(pos_REGIMEN_LABORAL)) entidad.REGIMEN_LABORAL = "";
                                else entidad.REGIMEN_LABORAL = dr.GetString(pos_REGIMEN_LABORAL);


                                if (dr.IsDBNull(pos_OFICINA)) entidad.OFICINA = "";
                                else entidad.OFICINA = dr.GetString(pos_OFICINA);

                                if (dr.IsDBNull(pos_COD_EXPEDIENTE)) entidad.COD_EXPEDIENTE = "";
                                else entidad.COD_EXPEDIENTE = dr.GetString(pos_COD_EXPEDIENTE);

                                if (dr.IsDBNull(pos_FECHA_RECEPCION)) entidad.FECHA_RECEPCION = "";
                                else entidad.FECHA_RECEPCION = dr.GetString(pos_FECHA_RECEPCION);

                                if (dr.IsDBNull(pos_FECHA_PRESCRIPCION)) entidad.FECHA_PRESCRIPCION = "";
                                else entidad.FECHA_PRESCRIPCION = dr.GetString(pos_FECHA_PRESCRIPCION);

                                if (dr.IsDBNull(pos_FECHA_HECHO)) entidad.FECHA_HECHO = "";
                                else entidad.FECHA_HECHO = dr.GetString(pos_FECHA_HECHO);

                                if (dr.IsDBNull(pos_FALTA)) entidad.FALTA = "";
                                else entidad.FALTA = dr.GetString(pos_FALTA);

                                if (dr.IsDBNull(pos_SANCION)) entidad.SANCION = "";
                                else entidad.SANCION = dr.GetString(pos_SANCION);

                                if (dr.IsDBNull(pos_SANCION_RECOMENDADA)) entidad.SANCION_RECOMENDADA = "";
                                else entidad.SANCION_RECOMENDADA = dr.GetString(pos_SANCION_RECOMENDADA);

                                if (dr.IsDBNull(pos_SITUACION)) entidad.SITUACION = "";
                                else entidad.SITUACION = dr.GetString(pos_SITUACION);

                                if (dr.IsDBNull(pos_ESTADO)) entidad.ESTADO = "";
                                else entidad.ESTADO = dr.GetString(pos_ESTADO);

                                if (dr.IsDBNull(pos_FLG_ESTADO)) entidad.FLG_ESTADO = 0;
                                else entidad.FLG_ESTADO = int.Parse(dr[pos_FLG_ESTADO].ToString());

                                if (dr.IsDBNull(pos_FEC_CREACION)) entidad.FEC_CREACION = "";
                                else entidad.FEC_CREACION = dr.GetString(pos_FEC_CREACION);

                                if (dr.IsDBNull(pos_USU_CREACION)) entidad.USU_CREACION = "";
                                else entidad.USU_CREACION = dr.GetString(pos_USU_CREACION);

                                if (dr.IsDBNull(pos_USU_MODIFICACION)) entidad.USU_MODIFICACION = "";
                                else entidad.USU_MODIFICACION = dr.GetString(pos_USU_MODIFICACION);


                                if (dr.IsDBNull(pos_FEC_MODIFICACION)) entidad.FEC_MODIFICACION = "";
                                else entidad.FEC_MODIFICACION = dr.GetString(pos_FEC_MODIFICACION);

                                lista.Add(entidad);
                            }
                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        auditoria.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
            return lista;
        }



        public IEnumerable<Cls_v_Expedientes> Expedientes_V_Buscar(ref Cls_Ent_Auditoria auditoria, Cls_v_Expedientes Param)
        {
            auditoria.Limpiar();
            IEnumerable<Cls_v_Expedientes> entidad = new List<Cls_v_Expedientes>();

            try
            {
                if (Param.ID_EXPEDIENTE != 0)
                    entidad = FindAll(w => w.ID_EXPEDIENTE == Param.ID_EXPEDIENTE);
                else if (Param.COD_EXPEDIENTE != null)
                    entidad = FindAll(e => e.COD_EXPEDIENTE.ToUpper() == Param.COD_EXPEDIENTE.ToUpper());
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
            return entidad;
        }


        public IEnumerable<Cls_v_Expedientes> Expedientes_V_GetAll(ref Cls_Ent_Auditoria auditoria, Cls_v_Expedientes Param)
        {
            auditoria.Limpiar();
            IEnumerable<Cls_v_Expedientes> entidad = new List<Cls_v_Expedientes>();

            try
            {
                if (!string.IsNullOrEmpty(Param.DNI) )
                    entidad = FindAll(w => w.ID_EXPEDIENTE == Param.ID_EXPEDIENTE);
                else if (Param.ID_ESTADO != 0)
                    entidad = FindAll(e => e.ID_ESTADO == Param.ID_ESTADO);
                else if (Param.ID_SITUACION != 0)
                    entidad = FindAll(e => e.ID_SITUACION == Param.ID_SITUACION);
                else
                    entidad = GetAll().OrderByDescending(w => w.ID_EXPEDIENTE);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
            return entidad;
        }



    }
}
