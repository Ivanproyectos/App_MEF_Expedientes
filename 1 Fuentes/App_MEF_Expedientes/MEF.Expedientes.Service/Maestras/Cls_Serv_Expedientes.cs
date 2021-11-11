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
                    Mientidad[0].ID_PERSONAL = entExpedientes.ID_PERSONAL;
                    Mientidad[0].FECHA_RECEPCION = entExpedientes.FECHA_RECEPCION;
                    Mientidad[0].FECHA_PRESCRIPCION = entExpedientes.FECHA_PRESCRIPCION;
                    Mientidad[0].FECHA_HECHO = entExpedientes.FECHA_HECHO;
                    Mientidad[0].HOJA_RUTA = entExpedientes.HOJA_RUTA;
                    Mientidad[0].ID_REMITENTE = entExpedientes.ID_REMITENTE;
                    //Mientidad[0].ID_ACTO = entExpedientes.ID_ACTO;
                    if (entExpedientes.ID_ACTO == 0)
                        Mientidad[0].ID_ACTO = null; 
                    else
                        entExpedientes.ID_ACTO = entExpedientes.ID_ACTO;

                    Mientidad[0].OBSERVACION_INVESTIGADORA = entExpedientes.OBSERVACION_INVESTIGADORA;
                    //Mientidad[0].ID_FALTA = entExpedientes.ID_FALTA;
                    if (entExpedientes.ID_FALTA == 0)
                        Mientidad[0].ID_FALTA = null;
                    else
                       Mientidad[0].ID_FALTA = entExpedientes.ID_FALTA;

                    Mientidad[0].ARTICULO = entExpedientes.ARTICULO;
                    Mientidad[0].INC = entExpedientes.INC;
                    Mientidad[0].OBSERVACION_INSTRUCTORA = entExpedientes.OBSERVACION_INSTRUCTORA;
                    //Mientidad[0].ID_PRECALIFICACION = entExpedientes.ID_PRECALIFICACION;
                    if (entExpedientes.ID_PRECALIFICACION == 0)
                        Mientidad[0].ID_PRECALIFICACION = null;
                    else
                        Mientidad[0].ID_PRECALIFICACION = entExpedientes.ID_PRECALIFICACION;

                    Mientidad[0].TIPO_SANCION_RECOMENDADA = entExpedientes.TIPO_SANCION_RECOMENDADA;
                    Mientidad[0].ACTO_INICIO = entExpedientes.ACTO_INICIO;
                    Mientidad[0].FECHA_NOTIFICACION = entExpedientes.FECHA_NOTIFICACION;
                    Mientidad[0].RECOMENDACION_PREINFORME = entExpedientes.RECOMENDACION_PREINFORME;
                    //Mientidad[0].ID_SANCION_RECOMENDADA = entExpedientes.ID_SANCION_RECOMENDADA;
                    if (entExpedientes.ID_SANCION_RECOMENDADA == 0)
                        Mientidad[0].ID_SANCION_RECOMENDADA = null;
                    else
                        Mientidad[0].ID_SANCION_RECOMENDADA = entExpedientes.ID_SANCION_RECOMENDADA;

                    Mientidad[0].ID_ORGANO_INSTRUCTOR = entExpedientes.ID_ORGANO_INSTRUCTOR;
                    Mientidad[0].FECHA_NOTIFICACION_INICIO = entExpedientes.FECHA_NOTIFICACION_INICIO;
                    Mientidad[0].DOCUMENTO_FINALIZACION = entExpedientes.DOCUMENTO_FINALIZACION;
                    Mientidad[0].RECOMENDACION_INSTRUCTOR = entExpedientes.RECOMENDACION_INSTRUCTOR;
                    Mientidad[0].ID_ORGANO_SANCIONADOR = entExpedientes.ID_ORGANO_SANCIONADOR;
                    Mientidad[0].SANCION = entExpedientes.SANCION;

                    Mientidad[0].OBSERVACION_SANCIONADORA = entExpedientes.OBSERVACION_SANCIONADORA;
                    //Mientidad[0].ID_SITUACION = entExpedientes.ID_SITUACION;

                    if (entExpedientes.ID_SITUACION == 0)
                        Mientidad[0].ID_SITUACION = null;
                    else
                        Mientidad[0].ID_SITUACION = entExpedientes.ID_SITUACION;

                    //Mientidad[0].ID_ESTADO = entExpedientes.ID_ESTADO;
                    if (entExpedientes.ID_ESTADO == 0)
                        Mientidad[0].ID_ESTADO = null;
                    else
                        Mientidad[0].ID_ESTADO = entExpedientes.ID_ESTADO;

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
                        if (entidad.ID_ACTO == 0)
                            entidad.ID_ACTO = null;

                        if (entidad.ID_FALTA == 0)
                            entidad.ID_FALTA = null;

                        if (entidad.ID_PRECALIFICACION == 0)
                            entidad.ID_PRECALIFICACION = null;

                        if (entidad.ID_SITUACION == 0)
                            entidad.ID_SITUACION = null;


                        if (entidad.ID_ESTADO == 0)
                            entidad.ID_ESTADO = null;

                        if (entidad.ID_SANCION_RECOMENDADA == 0)
                            entidad.ID_SANCION_RECOMENDADA = null;

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
