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
        public Cls_Ent_Usuario Usuario(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            System.Data.Common.DbDataReader dr = null;
            Cls_Ent_Usuario lista = new Cls_Ent_Usuario();
            try
            {
                using (var cmd = _context.GetStoredProcedureCommand("PCK_SEEGURIDAD_LDAP.USP_USUARIO_DET_PERFIL_LISTAR",
                new OracleParameter("PI_ID_SISTEMA", entidad.ID_SISTEMA),
                new OracleParameter("PI_USUARIO", entidad.LOGIN_USUARIO),
                //new OracleParameter("PI_Results", OracleDbType.Int32, ParameterDirection.Output),
                null))
                {
                    try
                    {
                        dr = cmd.ExecuteReader();
                        int pos_ID_USUARIO = dr.GetOrdinal("ID_USUARIO");
                        int pos_LOGIN_USUARIO = dr.GetOrdinal("LOGIN_USUARIO");
                        int pos_ID_OFICINA = dr.GetOrdinal("ID_OFICINA");
                        int pos_NOMBRE_OFICINA = dr.GetOrdinal("NOMBRE_OFICINA");
                        int pos_SIGLA = dr.GetOrdinal("SIGLA");
                        int pos_NOMBRE_PERSONA = dr.GetOrdinal("NOMBRE_PERSONA");

                        int pos_ID_PERFIL = dr.GetOrdinal("ID_PERFIL");
                        int pos_DESC_PERFIL = dr.GetOrdinal("DESC_PERFIL");


                        int inicio = 0;

                        if (dr.HasRows)
                            while (dr.Read())
                            {
                                //Cls_Ent_Usuario entidad = new Cls_Ent_Usuario();
                                if (inicio == 0)
                                {
                                    if (dr.IsDBNull(pos_ID_USUARIO)) entidad.ID_USUARIO = 0;
                                    else entidad.ID_USUARIO = long.Parse(dr[pos_ID_USUARIO].ToString());
                                    if (dr.IsDBNull(pos_LOGIN_USUARIO)) entidad.LOGIN_USUARIO = "";
                                    else entidad.LOGIN_USUARIO = Convert.ToString(dr[pos_LOGIN_USUARIO]);
                                    if (dr.IsDBNull(pos_ID_OFICINA)) entidad.ID_OFICINA = 0;
                                    else entidad.ID_OFICINA = Convert.ToInt32(dr[pos_ID_OFICINA]);

                                    if (dr.IsDBNull(pos_NOMBRE_OFICINA)) entidad.NOMBRE_OFICINA = "";
                                    else entidad.NOMBRE_OFICINA = Convert.ToString(dr[pos_NOMBRE_OFICINA]);

                                    if (dr.IsDBNull(pos_SIGLA)) entidad.SIGLA = "";
                                    else entidad.SIGLA = Convert.ToString(dr[pos_SIGLA]);

                                    if (dr.IsDBNull(pos_NOMBRE_PERSONA)) entidad.NOMBRE_PERSONA = "";
                                    else entidad.NOMBRE_PERSONA = Convert.ToString(dr[pos_NOMBRE_PERSONA]);
                                }

                                int ID_PERFIL = 0;
                                string DESC_PERFIL = "";

                                if (dr.IsDBNull(pos_ID_PERFIL)) ID_PERFIL = 0;
                                else ID_PERFIL = Convert.ToInt32(dr[pos_ID_PERFIL]);

                                if (dr.IsDBNull(pos_DESC_PERFIL)) DESC_PERFIL = "";
                                else DESC_PERFIL = Convert.ToString(dr[pos_DESC_PERFIL]);

                                entidad.Perfil.Add(new Cls_Ent_Usuario { ID_PERFIL = ID_PERFIL, DESC_PERFIL = DESC_PERFIL });

                                inicio++;

                                //lista.Add(entidad);
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
