using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Administracion;
using MEF.Expedientes.Repository;
using MEF.Expedientes.Contract.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF.Expedientes.Service.Administracion
{
    public  class Cls_Serv_FormatoCorreo : Repository<Cls_Ent_FormatoCorreo>, ICls_Serv_FormatoCorreo
    {
        readonly string SECUENCIA = "SEQ_ID_FORMATOCORREO";

        public IEnumerable<Cls_Ent_FormatoCorreo> FormatoCorreo_Listar(Cls_Ent_FormatoCorreo entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            IEnumerable<Cls_Ent_FormatoCorreo> lista;

            try
            {
                if (!string.IsNullOrEmpty(entidad.ASUNTO))
                    lista = FindAll(w => w.ASUNTO.ToUpper().Contains(entidad.ASUNTO.ToUpper()));

                else
                    lista = GetAll().OrderByDescending(w => w.ID_FORMATO);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                lista = new List<Cls_Ent_FormatoCorreo>();
            }
            return lista;
        }


        public IEnumerable<Cls_Ent_FormatoCorreo> FormatoCorreo_Buscar(ref Cls_Ent_Auditoria auditoria, long id = 0, string descripcion = null)
        {
            auditoria.Limpiar();
            IEnumerable<Cls_Ent_FormatoCorreo> entidad = new List<Cls_Ent_FormatoCorreo>();

            try
            {
                if (id != 0)
                    entidad = FindAll(w => w.ID_FORMATO == id);
                else if (descripcion != null)
                    entidad = FindAll(e => e.ASUNTO.ToUpper() == descripcion.ToUpper());
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
            return entidad;
        }

        public void FormatoCorreo_Registrar(Cls_Ent_FormatoCorreo entFormatoCorreo, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();

            try
            {
                Add(entFormatoCorreo);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }

        }

        public void FormatoCorreo_Actualizar(Cls_Ent_FormatoCorreo entFormatoCorreo, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                Update(entFormatoCorreo, entFormatoCorreo.ID_FORMATO);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void FormatoCorreo_Actualizar_FormatoCorreo(Cls_Ent_FormatoCorreo entFormatoCorreo, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                List<Cls_Ent_FormatoCorreo> Mientidad = (List<Cls_Ent_FormatoCorreo>)FormatoCorreo_Buscar(ref auditoria, entFormatoCorreo.ID_FORMATO);
                List<Cls_Ent_FormatoCorreo> buscar = new List<Cls_Ent_FormatoCorreo>();
                buscar = (List<Cls_Ent_FormatoCorreo>)FormatoCorreo_Buscar(ref auditoria, 0, entFormatoCorreo.ASUNTO);
                bool Valido = true;
                if (buscar.Count() > 0)
                {
                    foreach (var item in buscar)
                    {
                        if (item.ASUNTO.ToUpper().Equals(entFormatoCorreo.ASUNTO))
                        {
                                if (item.ID_FORMATO != Mientidad[0].ID_FORMATO)
                                {
                                    Valido = false;
                                    break;
                                }
                        }
                    }
                }
                if (Valido)
                {
                    Mientidad[0].ASUNTO = entFormatoCorreo.ASUNTO;
                    Mientidad[0].BODY = entFormatoCorreo.BODY;
                    Mientidad[0].IP_MODIFICACION = entFormatoCorreo.IP_MODIFICACION;
                    Mientidad[0].FEC_MODIFICACION = DateTime.Now;
                    Mientidad[0].USU_MODIFICACION = entFormatoCorreo.USU_MODIFICACION;
                    FormatoCorreo_Actualizar(Mientidad[0], ref auditoria);
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

        public void FormatoCorreo_Estado(Cls_Ent_FormatoCorreo entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_FormatoCorreo> Mientidad = (List<Cls_Ent_FormatoCorreo>)FormatoCorreo_Buscar(ref auditoria, entidad.ID_FORMATO);
            try
            {
                if (Mientidad != null)
                {
                    Mientidad[0].FLG_ESTADO = entidad.FLG_ESTADO;
                    Mientidad[0].IP_MODIFICACION = entidad.IP_MODIFICACION;
                    Mientidad[0].FEC_MODIFICACION = DateTime.Now;
                    Mientidad[0].USU_MODIFICACION = entidad.USU_MODIFICACION;
                    FormatoCorreo_Actualizar(Mientidad[0], ref auditoria);
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void FormatoCorreo_Eliminar(Cls_Ent_FormatoCorreo entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            //List<Cls_Ent_FormatoCorreo> Mientidad = (List<Cls_Ent_FormatoCorreo>)FormatoCorreo_Buscar(ref auditoria, entidad.ID_FORMATO);
            try
            {
                    DeleteUno(entidad.ID_FORMATO);
    
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void FormatoCorreo_Agregar_FormatoCorreo(Cls_Ent_FormatoCorreo entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            bool Valido = true;
            List<Cls_Ent_FormatoCorreo> buscar = FormatoCorreo_Buscar(ref auditoria, entidad.ID_FORMATO, entidad.ASUNTO).ToList();

            try
            {
                if (buscar.Count() > 0)
                {
                    foreach (var item in buscar)
                    {
                        if (item.ASUNTO.ToUpper().Equals(entidad.ASUNTO))
                        {            
                                Valido = false;
                                break;
                        }
                    }
                }

                if (Valido)
                {
               
                        entidad.ID_FORMATO = GetSequence(SECUENCIA);
                        entidad.FLG_ESTADO = 1;
                        entidad.FEC_CREACION = DateTime.Now;
                        FormatoCorreo_Registrar(entidad, ref auditoria);
                
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
