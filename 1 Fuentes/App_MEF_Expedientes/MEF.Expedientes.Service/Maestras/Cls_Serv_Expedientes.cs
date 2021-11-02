using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras;
using MEF.Expedientes.Repository;
using MEF.Expedientes.Contract.Maestras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace MEF.Expedientes.Service.Maestras
{
    public class Cls_Serv_Expedientes : Repository<Cls_Ent_Expedientes>, ICls_Serv_Expedientes
    {

        readonly string SECUENCIA = "SEQ_ID_EXPEDIENTES";


        public List<Cls_Ent_Expedientes> Expedientes_Listar_Todo(string ORDEN_COLUMNA, string ORDEN, int FILAS, int PAGINA, string @WHERE, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            System.Data.Common.DbDataReader dr = null;
            List<Cls_Ent_Expedientes> lista = new List<Cls_Ent_Expedientes>();
            try
            {
                using (var cmd = _context.GetStoredProcedureCommand("PCK_MICROFORMA.USP_DOCUMENTO_PAGINACION",
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
                        int pos_ID_DOCUMENTO = dr.GetOrdinal("ID_DOCUMENTO");
                        int pos_ID_ESTADO_DOCUMENTO = dr.GetOrdinal("ID_ESTADO_DOCUMENTO");
                        int pos_DESCRIPCION_ESTADO = dr.GetOrdinal("DESCRIPCION_ESTADO");
                        int pos_ID_DOCUMENTO_ASIGNADO = dr.GetOrdinal("ID_DOCUMENTO_ASIGNADO");

                        int pos_ID_CONTROL_CARGA = dr.GetOrdinal("ID_CONTROL_CARGA");
                        int pos_COD_DOCUMENTO = dr.GetOrdinal("COD_DOCUMENTO");
                        int pos_ID_FONDO = dr.GetOrdinal("ID_FONDO");
                        int pos_DESC_FONDO = dr.GetOrdinal("DESC_FONDO");
                        int pos_ID_SECCION = dr.GetOrdinal("ID_SECCION");
                        int pos_DESC_LARGA_SECCION = dr.GetOrdinal("DESC_LARGA_SECCION");
                        int pos_ID_SUB_SECCION = dr.GetOrdinal("ID_SUB_SECCION");
                        int pos_DESC_LARGA_SUBSECCION = dr.GetOrdinal("DESC_LARGA_SUBSECCION");
                        int pos_ID_SERIE = dr.GetOrdinal("ID_SERIE");
                        int pos_DESC_SERIE = dr.GetOrdinal("DESC_SERIE");
                        int pos_ID_TIPO_DOCUMENTO = dr.GetOrdinal("ID_TIPO_DOCUMENTO");
                        int pos_DESC_TIPO_DOCUMENTO = dr.GetOrdinal("DESC_TIPO_DOCUMENTO");
                        int pos_NUM_REGISTRO = dr.GetOrdinal("NUM_REGISTRO");
                        int pos_NUM_EXPEDIENTE = dr.GetOrdinal("NUM_EXPEDIENTE");
                        int pos_VOLUMEN = dr.GetOrdinal("VOLUMEN");
                        int pos_DESCR_A = dr.GetOrdinal("DESCR_A");
                        int pos_DESCR_B = dr.GetOrdinal("DESCR_B");
                        int pos_DESCR_C = dr.GetOrdinal("DESCR_C");
                        int pos_FECHA_INI = dr.GetOrdinal("FECHA_INI");
                        int pos_FECHA_FIN = dr.GetOrdinal("FECHA_FIN");
                        int pos_FOLIOS = dr.GetOrdinal("FOLIOS");
                        int pos_TOT_IMAGENES = dr.GetOrdinal("TOT_IMAGENES");
                        int pos_CAJA = dr.GetOrdinal("CAJA");
                        int pos_OBSERVACION = dr.GetOrdinal("OBSERVACION");

                        int pos_ID_USUARIO = dr.GetOrdinal("ID_USUARIO");
                        int pos_ID_LOTE = dr.GetOrdinal("ID_LOTE");
                        int pos_NOMBRE_USUARIO = dr.GetOrdinal("NOMBRE_USUARIO");

                        int pos_FLG_ELIMINADO = dr.GetOrdinal("FLG_ELIMINADO");
                        int pos_FLG_VALIDO = dr.GetOrdinal("FLG_VALIDO");
                        int pos_FLG_ESTADO = dr.GetOrdinal("FLG_ESTADO");
                        int pos_USU_CREACION = dr.GetOrdinal("USU_CREACION");
                        int pos_FEC_CREACION = dr.GetOrdinal("FEC_CREACION");
                        int pos_STR_FEC_CREACION = dr.GetOrdinal("STR_FEC_CREACION");
                        int pos_IP_CREACION = dr.GetOrdinal("IP_CREACION");
                        int pos_USU_MODIFICACION = dr.GetOrdinal("USU_MODIFICACION");
                        int pos_FEC_MODIFICACION = dr.GetOrdinal("FEC_MODIFICACION");
                        int pos_STR_FEC_MODIFICACION = dr.GetOrdinal("STR_FEC_MODIFICACION");
                        int pos_IP_MODIFICACION = dr.GetOrdinal("IP_MODIFICACION");
                        if (dr.HasRows)
                            while (dr.Read())
                            {
                                Cls_Ent_Expedientes entidad = new Cls_Ent_Expedientes();

                                //if (dr.IsDBNull(pos_ID_DOCUMENTO)) entidad.ID_DOCUMENTO = 0;
                                //else entidad.ID_DOCUMENTO = long.Parse(dr[pos_ID_DOCUMENTO].ToString());

                                //if (dr.IsDBNull(pos_ID_ESTADO_DOCUMENTO)) entidad.ID_ESTADO_DOCUMENTO = 0;
                                //else entidad.ID_ESTADO_DOCUMENTO = long.Parse(dr[pos_ID_ESTADO_DOCUMENTO].ToString());

                                //if (dr.IsDBNull(pos_DESCRIPCION_ESTADO)) entidad.DESCRIPCION_ESTADO = "";
                                //else entidad.DESCRIPCION_ESTADO = dr.GetString(pos_DESCRIPCION_ESTADO);

                                //if (dr.IsDBNull(pos_ID_DOCUMENTO_ASIGNADO)) entidad.ID_DOCUMENTO_ASIGNADO = 0;
                                //else entidad.ID_DOCUMENTO_ASIGNADO = long.Parse(dr[pos_ID_DOCUMENTO_ASIGNADO].ToString());

                                //if (dr.IsDBNull(pos_ID_CONTROL_CARGA)) entidad.ID_CONTROL_CARGA = 0;
                                //else entidad.ID_CONTROL_CARGA = long.Parse(dr[pos_ID_CONTROL_CARGA].ToString());

                                //if (dr.IsDBNull(pos_COD_DOCUMENTO)) entidad.COD_DOCUMENTO = "";
                                //else entidad.COD_DOCUMENTO = dr.GetString(pos_COD_DOCUMENTO);

                                //if (dr.IsDBNull(pos_DESC_FONDO)) entidad.DESC_FONDO = "";
                                //else entidad.DESC_FONDO = dr.GetString(pos_DESC_FONDO);

                                //if (dr.IsDBNull(pos_DESC_LARGA_SECCION)) entidad.DESC_LARGA_SECCION = "";
                                //else entidad.DESC_LARGA_SECCION = dr.GetString(pos_DESC_LARGA_SECCION);

                                //if (dr.IsDBNull(pos_DESC_LARGA_SUBSECCION)) entidad.DESC_LARGA_SUBSECCION = "";
                                //else entidad.DESC_LARGA_SUBSECCION = dr.GetString(pos_DESC_LARGA_SUBSECCION);

                                //if (dr.IsDBNull(pos_DESC_SERIE)) entidad.DESC_SERIE = "";
                                //else entidad.DESC_SERIE = dr.GetString(pos_DESC_SERIE);

                                //if (dr.IsDBNull(pos_DESC_TIPO_DOCUMENTO)) entidad.DESC_TIPO_DOCUMENTO = "";
                                //else entidad.DESC_TIPO_DOCUMENTO = dr.GetString(pos_DESC_TIPO_DOCUMENTO);

                                //if (dr.IsDBNull(pos_NUM_REGISTRO)) entidad.NUM_REGISTRO = "";
                                //else entidad.NUM_REGISTRO = dr.GetString(pos_NUM_REGISTRO);

                                //if (dr.IsDBNull(pos_NUM_EXPEDIENTE)) entidad.NUM_EXPEDIENTE = "";
                                //else entidad.NUM_EXPEDIENTE = dr.GetString(pos_NUM_EXPEDIENTE);

                                //if (dr.IsDBNull(pos_VOLUMEN)) entidad.VOLUMEN = "";
                                //else entidad.VOLUMEN = dr.GetString(pos_VOLUMEN);

                                //if (dr.IsDBNull(pos_DESCR_A)) entidad.DESCR_A = "";
                                //else entidad.DESCR_A = dr.GetString(pos_DESCR_A);

                                //if (dr.IsDBNull(pos_DESCR_B)) entidad.DESCR_B = "";
                                //else entidad.DESCR_B = dr.GetString(pos_DESCR_B);

                                //if (dr.IsDBNull(pos_DESCR_C)) entidad.DESCR_C = "";
                                //else entidad.DESCR_C = dr.GetString(pos_DESCR_C);

                                //if (dr.IsDBNull(pos_FECHA_INI)) entidad.FECHA_INI = "";
                                //else entidad.FECHA_INI = dr.GetString(pos_FECHA_INI);

                                //if (dr.IsDBNull(pos_FECHA_FIN)) entidad.FECHA_FIN = "";
                                //else entidad.FECHA_FIN = dr.GetString(pos_FECHA_FIN);

                                //if (dr.IsDBNull(pos_FOLIOS)) entidad.FOLIOS = "";
                                //else entidad.FOLIOS = dr.GetString(pos_FOLIOS);

                                //if (dr.IsDBNull(pos_TOT_IMAGENES)) entidad.TOT_IMAGENES = "";
                                //else entidad.TOT_IMAGENES = dr.GetString(pos_TOT_IMAGENES);

                                //if (dr.IsDBNull(pos_CAJA)) entidad.CAJA = "";
                                //else entidad.CAJA = dr.GetString(pos_CAJA);

                                //if (dr.IsDBNull(pos_OBSERVACION)) entidad.OBSERVACION = "";
                                //else entidad.OBSERVACION = dr.GetString(pos_OBSERVACION);

                                //if (dr.IsDBNull(pos_ID_USUARIO)) entidad.ID_USUARIO = 0;
                                //else entidad.ID_USUARIO = long.Parse(dr[pos_ID_USUARIO].ToString());

                                //if (dr.IsDBNull(pos_ID_LOTE)) entidad.ID_LOTE = 0;
                                //else entidad.ID_LOTE = long.Parse(dr[pos_ID_LOTE].ToString());

                                //if (dr.IsDBNull(pos_NOMBRE_USUARIO)) entidad.NOMBRE_USUARIO = "";
                                //else entidad.NOMBRE_USUARIO = dr.GetString(pos_NOMBRE_USUARIO);
                                ////int pos_ID_USUARIO = dr.GetOrdinal("ID_USUARIO");
                                ////int pos_ID_LOTE = dr.GetOrdinal("ID_LOTE");
                                ////int pos_NOMBRE_USUARIO = dr.GetOrdinal("NOMBRE_USUARIO");

                                //if (dr.IsDBNull(pos_FLG_ELIMINADO)) entidad.FLG_ELIMINADO = "";
                                //else entidad.FLG_ELIMINADO = dr.GetString(pos_FLG_ELIMINADO);

                                //if (dr.IsDBNull(pos_FLG_VALIDO)) entidad.FLG_VALIDO = "";
                                //else entidad.FLG_VALIDO = dr.GetString(pos_FLG_VALIDO);

                                //if (dr.IsDBNull(pos_FLG_ESTADO)) entidad.FLG_ESTADO = "";
                                //else entidad.FLG_ESTADO = dr.GetString(pos_FLG_ESTADO);

                                //if (dr.IsDBNull(pos_USU_CREACION)) entidad.USU_CREACION = "";
                                //else entidad.USU_CREACION = dr.GetString(pos_USU_CREACION);

                                //if (dr.IsDBNull(pos_STR_FEC_CREACION)) entidad.STR_FEC_CREACION = "";
                                //else entidad.STR_FEC_CREACION = dr.GetString(pos_STR_FEC_CREACION);

                                //if (dr.IsDBNull(pos_IP_CREACION)) entidad.IP_CREACION = "";
                                //else entidad.IP_CREACION = dr.GetString(pos_IP_CREACION);

                                //if (dr.IsDBNull(pos_USU_MODIFICACION)) entidad.USU_MODIFICACION = "";
                                //else entidad.USU_MODIFICACION = dr.GetString(pos_USU_MODIFICACION);

                                //if (dr.IsDBNull(pos_STR_FEC_MODIFICACION)) entidad.STR_FEC_MODIFICACION = "";
                                //else entidad.STR_FEC_MODIFICACION = dr.GetString(pos_STR_FEC_MODIFICACION);

                                if (dr.IsDBNull(pos_IP_MODIFICACION)) entidad.IP_MODIFICACION = "";
                                else entidad.IP_MODIFICACION = dr.GetString(pos_IP_MODIFICACION);
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

        public IEnumerable<Cls_Ent_Expedientes> Expedientes_Buscar(ref Cls_Ent_Auditoria auditoria, long id = 0, string descripcion = null)
        {
            auditoria.Limpiar();
            IEnumerable<Cls_Ent_Expedientes> entidad = new List<Cls_Ent_Expedientes>();

            try
            {
                if (id != 0)
                    entidad = FindAll(w => w.ID_EXPEDIENTE == id);
                else if (descripcion != null)
                    entidad = FindAll(e => e.COD_EXPEDIENTE.ToUpper() == descripcion.ToUpper());
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
            return entidad;
        }

        public void Expedientes_Registrar(Cls_Ent_Expedientes entExpedientes, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();

            try
            {
                Add(entExpedientes);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }

        }

        public void Expedientes_Actualizar(Cls_Ent_Expedientes entExpedientes, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                Update(entExpedientes, entExpedientes.ID_EXPEDIENTE);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Expedientes_Actualizar_Expedientes(Cls_Ent_Expedientes entExpedientes, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                List<Cls_Ent_Expedientes> Mientidad = (List<Cls_Ent_Expedientes>)Expedientes_Buscar(ref auditoria, entExpedientes.ID_EXPEDIENTE);
                List<Cls_Ent_Expedientes> buscar = new List<Cls_Ent_Expedientes>();
                buscar = (List<Cls_Ent_Expedientes>)Expedientes_Buscar(ref auditoria, 0, entExpedientes.COD_EXPEDIENTE);
                bool Valido = true;
                if (buscar.Count() > 0)
                {
                    foreach (var item in buscar)
                    {
                        if (item.COD_EXPEDIENTE.ToUpper().Equals(entExpedientes.COD_EXPEDIENTE))
                        {
                            if (item.ID_EXPEDIENTE != Mientidad[0].ID_EXPEDIENTE)
                            {
                                Valido = false;
                                break;
                            }
                        }
                    }
                }
                if (Valido)
                {
                    //Mientidad[0].ASUNTO = entExpedientes.ASUNTO;
                    //Mientidad[0].BODY = entExpedientes.BODY;
                    Mientidad[0].IP_MODIFICACION = entExpedientes.IP_MODIFICACION;
                    Mientidad[0].FEC_MODIFICACION = DateTime.Now;
                    Mientidad[0].USU_MODIFICACION = entExpedientes.USU_MODIFICACION;
                    Expedientes_Actualizar(Mientidad[0], ref auditoria);
                }
                else
                {
                    auditoria.Rechazar("Asunto ya existe");
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Expedientes_Estado(Cls_Ent_Expedientes entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Expedientes> Mientidad = (List<Cls_Ent_Expedientes>)Expedientes_Buscar(ref auditoria, entidad.ID_EXPEDIENTE);
            try
            {
                if (Mientidad != null)
                {
                    Mientidad[0].FLG_ESTADO = entidad.FLG_ESTADO;
                    Mientidad[0].IP_MODIFICACION = entidad.IP_MODIFICACION;
                    Mientidad[0].FEC_MODIFICACION = DateTime.Now;
                    Mientidad[0].USU_MODIFICACION = entidad.USU_MODIFICACION;
                    Expedientes_Actualizar(Mientidad[0], ref auditoria);
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Expedientes_Eliminar(Cls_Ent_Expedientes entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            //List<Cls_Ent_Expedientes> Mientidad = (List<Cls_Ent_Expedientes>)Expedientes_Buscar(ref auditoria, entidad.ID_EXPEDIENTE);
            try
            {
                DeleteUno(entidad.ID_EXPEDIENTE);

            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Expedientes_Agregar_Expedientes(Cls_Ent_Expedientes entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            bool Valido = true;
            List<Cls_Ent_Expedientes> buscar = Expedientes_Buscar(ref auditoria, entidad.ID_EXPEDIENTE, entidad.COD_EXPEDIENTE).ToList();

            try
            {
                if (buscar.Count() > 0)
                {
                    foreach (var item in buscar)
                    {
                        if (item.COD_EXPEDIENTE.ToUpper().Equals(entidad.COD_EXPEDIENTE))
                        {
                            Valido = false;
                            break;
                        }
                    }
                }

                if (Valido)
                {
                    entidad.ID_EXPEDIENTE = GetSequence(SECUENCIA);
                    var COD = "000000" + entidad.ID_EXPEDIENTE;
                    var IndexCount = Convert.ToString(entidad.ID_EXPEDIENTE).Length;
                    if (IndexCount < 7) {
                        var CODIGO_EXP = string.Format("{0}{1}", "EXP", COD.Substring(IndexCount, 6));
                        entidad.COD_EXPEDIENTE = CODIGO_EXP; 
                        entidad.FLG_ESTADO = 1;
                        entidad.FEC_CREACION = DateTime.Now;
                        Expedientes_Registrar(entidad, ref auditoria);
                    }
                    else {
                        auditoria.Rechazar("Código maximo alcanzado, no se registro.");
                    }

                }
                else
                    auditoria.Rechazar("Asunto ya existe");
            }
            catch (Exception ex)
            {

                auditoria.Error(ex);
            }
        }










    }
}
