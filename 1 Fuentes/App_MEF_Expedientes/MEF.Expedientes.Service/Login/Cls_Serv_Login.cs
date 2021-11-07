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
                using (var cmd = _context.GetStoredProcedureCommand("SEGURIDAD.PCK_SEEGURIDAD_LDAP.USP_USUARIO_DET_PERFIL_LISTAR",
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
                                    if (dr.IsDBNull(pos_ID_USUARIO)) lista.ID_USUARIO = 0;
                                    else lista.ID_USUARIO = long.Parse(dr[pos_ID_USUARIO].ToString());
                                    if (dr.IsDBNull(pos_LOGIN_USUARIO)) lista.LOGIN_USUARIO = "";
                                    else lista.LOGIN_USUARIO = Convert.ToString(dr[pos_LOGIN_USUARIO]);
                                    if (dr.IsDBNull(pos_ID_OFICINA)) lista.ID_OFICINA = 0;
                                    else lista.ID_OFICINA = Convert.ToInt32(dr[pos_ID_OFICINA]);

                                    if (dr.IsDBNull(pos_NOMBRE_OFICINA)) lista.NOMBRE_OFICINA = "";
                                    else lista.NOMBRE_OFICINA = Convert.ToString(dr[pos_NOMBRE_OFICINA]);

                                    if (dr.IsDBNull(pos_SIGLA)) lista.SIGLA = "";
                                    else lista.SIGLA = Convert.ToString(dr[pos_SIGLA]);

                                    if (dr.IsDBNull(pos_NOMBRE_PERSONA)) lista.NOMBRE_PERSONA = "";
                                    else lista.NOMBRE_PERSONA = Convert.ToString(dr[pos_NOMBRE_PERSONA]);
                                }

                                int ID_PERFIL = 0;
                                string DESC_PERFIL = "";

                                if (dr.IsDBNull(pos_ID_PERFIL)) ID_PERFIL = 0;
                                else ID_PERFIL = Convert.ToInt32(dr[pos_ID_PERFIL]);

                                if (dr.IsDBNull(pos_DESC_PERFIL)) DESC_PERFIL = "";
                                else DESC_PERFIL = Convert.ToString(dr[pos_DESC_PERFIL]);

                                lista.Perfil.Add(new Cls_Ent_Usuario { ID_PERFIL = ID_PERFIL, DESC_PERFIL = DESC_PERFIL });

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



        public List<Cls_Ent_Modulos> Modulos_Usuarios(Cls_Ent_Modulos param, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            System.Data.Common.DbDataReader dr = null;
            List<Cls_Ent_Modulos> lista = new List<Cls_Ent_Modulos>();
            try
            {
                using (var cmd = _context.GetStoredProcedureCommand("SEGURIDAD.PCK_SEEGURIDAD_LDAP.USP_MENU_USUARIO_PERFIL_LISTAR",
                new OracleParameter("PI_ID_USUARIO", param.ID_USUARIO),
                new OracleParameter("PI_ID_SISTEMA", param.ID_SISTEMA),
                new OracleParameter("PI_ID_PERFIL", param.ID_PERFIL),
                //new OracleParameter("PI_Results", OracleDbType.Int32, ParameterDirection.Output),
                null))
                {
                    try
                    {
                        dr = cmd.ExecuteReader();
                        int pos_ID_SISTEMA_MODULO = dr.GetOrdinal("ID_SISTEMA_MODULO");
                        int pos_ID_SISTEMA_MODULO_PADRE = dr.GetOrdinal("ID_SISTEMA_MODULO_PADRE");
                        int pos_ID_TIPO_MODULO = dr.GetOrdinal("ID_TIPO_MODULO");
                        int pos_ID_A = dr.GetOrdinal("ID_A");
                        int pos_ID_LI = dr.GetOrdinal("ID_LI");
                        int pos_DESC_MODULO = dr.GetOrdinal("DESC_MODULO");
                        int pos_IMAGEN = dr.GetOrdinal("IMAGEN");
                        int pos_ORDEN = dr.GetOrdinal("ORDEN");
                        int pos_URL_MODULO = dr.GetOrdinal("URL_MODULO");

 

                        if (dr.HasRows)
                            while (dr.Read())
                            {
                              Cls_Ent_Modulos entidad = new Cls_Ent_Modulos();
                            if (dr.IsDBNull(pos_ID_SISTEMA_MODULO)) entidad.ID_SISTEMA_MODULO = 0;
                            else entidad.ID_SISTEMA_MODULO = int.Parse(dr[pos_ID_SISTEMA_MODULO].ToString());

                            if (dr.IsDBNull(pos_ID_SISTEMA_MODULO_PADRE)) entidad.ID_SISTEMA_MODULO_PADRE = 0;
                            else entidad.ID_SISTEMA_MODULO_PADRE = int.Parse(dr[pos_ID_SISTEMA_MODULO_PADRE].ToString());

                            if (dr.IsDBNull(pos_ID_TIPO_MODULO)) entidad.ID_TIPO_MODULO = 0;
                            else entidad.ID_TIPO_MODULO = int.Parse(dr[pos_ID_TIPO_MODULO].ToString());

                            if (dr.IsDBNull(pos_ID_A)) entidad.ID_A = "";
                            else entidad.ID_A = Convert.ToString(dr[pos_ID_A]);

                            if (dr.IsDBNull(pos_ID_LI)) entidad.ID_LI = "";
                            else entidad.ID_LI = Convert.ToString(dr[pos_ID_LI]);

                            if (dr.IsDBNull(pos_DESC_MODULO)) entidad.DESC_MODULO = "";
                            else entidad.DESC_MODULO = Convert.ToString(dr[pos_DESC_MODULO]);

                            if (dr.IsDBNull(pos_IMAGEN)) entidad.IMAGEN = "";
                            else entidad.IMAGEN = Convert.ToString(dr[pos_IMAGEN]);

                            if (dr.IsDBNull(pos_ORDEN)) entidad.ORDEN = 0;
                            else entidad.ORDEN = int.Parse(dr[pos_ORDEN].ToString());

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
