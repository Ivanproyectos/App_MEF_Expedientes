using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras;
using MEF.Expedientes.Repository;
using MEF.Expedientes.Contract.Maestras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Capa_Recursos;
using System.IO;

namespace MEF.Expedientes.Service.Maestras
{
    public  class Cls_Serv_Adjuntos : Repository<Cls_Ent_Adjuntos>, ICls_Serv_Adjuntos
    {
        readonly string SECUENCIA = "SEQ_ID_ADJUNTO";

        public IEnumerable<Cls_Ent_Adjuntos> Adjuntos_Listar(Cls_Ent_Adjuntos entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            IEnumerable<Cls_Ent_Adjuntos> lista;

            try
            {
              lista = FindAll(w => w.ID_MAESTRA == entidad.ID_MAESTRA); 
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                lista = new List<Cls_Ent_Adjuntos>();
            }
            return lista;
        }


        public IEnumerable<Cls_Ent_Adjuntos> Adjuntos_Buscar(ref Cls_Ent_Auditoria auditoria, long id = 0, string descripcion = null)
        {
            auditoria.Limpiar();
            IEnumerable<Cls_Ent_Adjuntos> entidad = new List<Cls_Ent_Adjuntos>();

            try
            {
                if (id != 0)
                    entidad = FindAll(w => w.ID_ADJUNTO == id);
                else if (descripcion != null)
                    entidad = FindAll(e => e.OBSERVACION.ToUpper() == descripcion.ToUpper());
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
            return entidad;
        }

        public void Adjuntos_Registrar(Cls_Ent_Adjuntos entAdjuntos, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();

            try
            {
                Add(entAdjuntos);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }

        }

        public void Adjuntos_Actualizar(Cls_Ent_Adjuntos entAdjuntos, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                Update(entAdjuntos, entAdjuntos.ID_ADJUNTO);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Adjuntos_Actualizar_Adjuntos(Cls_Ent_Adjuntos entAdjuntos, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                List<Cls_Ent_Adjuntos> Mientidad = (List<Cls_Ent_Adjuntos>)Adjuntos_Buscar(ref auditoria, entAdjuntos.ID_ADJUNTO);
                List<Cls_Ent_Adjuntos> buscar = new List<Cls_Ent_Adjuntos>();
                buscar = (List<Cls_Ent_Adjuntos>)Adjuntos_Buscar(ref auditoria, 0, entAdjuntos.OBSERVACION);
                bool Valido = true;
                if (buscar.Count() > 0)
                {
                    foreach (var item in buscar)
                    {
                        if (item.OBSERVACION.ToUpper().Equals(entAdjuntos.OBSERVACION))
                        {
                                if (item.ID_ADJUNTO != Mientidad[0].ID_ADJUNTO)
                                {
                                    Valido = false;
                                    break;
                                }
                        }
                    }
                }
                if (Valido)
                {
            

                    Mientidad[0].OBSERVACION = entAdjuntos.OBSERVACION;
                    //Mientidad[0].BODY = entAdjuntos.BODY;
                    Mientidad[0].IP_MODIFICACION = entAdjuntos.IP_MODIFICACION;
                    Mientidad[0].FEC_MODIFICACION = DateTime.Now;
                    Mientidad[0].USU_MODIFICACION = entAdjuntos.USU_MODIFICACION;
                    Adjuntos_Actualizar(Mientidad[0], ref auditoria);
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



  
        public void Adjuntos_Estado(Cls_Ent_Adjuntos entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Adjuntos> Mientidad = (List<Cls_Ent_Adjuntos>)Adjuntos_Buscar(ref auditoria, entidad.ID_ADJUNTO);
            try
            {
                if (Mientidad != null)
                {
                    Mientidad[0].FLG_ESTADO = entidad.FLG_ESTADO;
                    Mientidad[0].IP_MODIFICACION = entidad.IP_MODIFICACION;
                    Mientidad[0].FEC_MODIFICACION = DateTime.Now;
                    Mientidad[0].USU_MODIFICACION = entidad.USU_MODIFICACION;
                    Adjuntos_Actualizar(Mientidad[0], ref auditoria);
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Adjuntos_Eliminar(Cls_Ent_Adjuntos entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            //List<Cls_Ent_Adjuntos> Mientidad = (List<Cls_Ent_Adjuntos>)Adjuntos_Buscar(ref auditoria, entidad.ID_ADJUNTO);
            try
            {
                    DeleteUno(entidad.ID_ADJUNTO);
    
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Adjuntos_Agregar_Adjuntos(Cls_Ent_Adjuntos entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            bool Valido = true;
            List<Cls_Ent_Adjuntos> buscar = Adjuntos_Buscar(ref auditoria, entidad.ID_ADJUNTO, entidad.OBSERVACION).ToList();

            try
            {
                if (buscar.Count() > 0)
                {
                    foreach (var item in buscar)
                    {
                        if (item.OBSERVACION.ToUpper().Equals(entidad.OBSERVACION))
                        {            
                                Valido = false;
                                break;
                        }
                    }
                }

                if (Valido)
                {
                 
            
                    entidad.ID_ADJUNTO = GetSequence(SECUENCIA);
                    //entidad.ARCHIVO = ARCHIVO_BLOB;
                    entidad.FLG_ESTADO = 1;
                    entidad.FEC_CREACION = DateTime.Now;
                    Adjuntos_Registrar(entidad, ref auditoria);
                }
                
                else
                    auditoria.Rechazar("Observación ya existe");
            }
            catch (Exception ex)
            {

                auditoria.Error(ex);
            }
        }






    }
}
