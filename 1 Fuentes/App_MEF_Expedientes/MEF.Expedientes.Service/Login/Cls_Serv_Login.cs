using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;

using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Login;
using MEF.Expedientes.Repository;
using MEF.Expedientes.Contract.Login;

namespace MEF.Expedientes.Service.Login
{
    public class Cls_Serv_Login : Repository<Cls_Ent_Usuario>, ICls_Serv_Login
    {
        public List<Cls_Ent_Usuario> Login_Listar(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            System.Data.Common.DbDataReader dr = null;
            List<Cls_Ent_Usuario> lista = new List<Cls_Ent_Usuario>();
            try
            {
                using (var cmd = _context.GetStoredProcedureCommand("PCK_SEEGURIDAD_LDAP.USP_USUARIO_DET_PERFIL_LISTAR",
                new OracleParameter("PI_ID_SISTEMA", entidad.ID_SISTEMA),
                new OracleParameter("PI_USUARIO", entidad.ID_USUARIO),
                new OracleParameter("PI_Results", OracleDbType.Int32, ParameterDirection.Output),
                null))
                {
                    try
                    {
                        dr = cmd.ExecuteReader();
                        //int CUENTA = int.Parse(cmd.Parameters["PO_CUENTA"].Value.ToString());
                        //auditoria.OBJETO = CUENTA;
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
                       


                        if (dr.HasRows)
                            while (dr.Read())
                            {
                                Cls_Ent_Usuario entidad = new Cls_Ent_Usuario();

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




    }
}
