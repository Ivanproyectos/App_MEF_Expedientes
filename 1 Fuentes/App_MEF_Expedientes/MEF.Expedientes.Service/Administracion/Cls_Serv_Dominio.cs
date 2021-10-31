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
    public  class Cls_Serv_Dominio : Repository<Cls_Ent_Dominio>, ICls_Serv_Dominio
    {
        readonly string SECUENCIA = "SEQ_ID_DOMINIO";

        public IEnumerable<Cls_Ent_Dominio> Dominio_Listar(Cls_Ent_Dominio entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            IEnumerable<Cls_Ent_Dominio> lista;

            try
            {
                if (!string.IsNullOrEmpty(entidad.DESC_CORTA_DOMINIO))
                    lista = FindAll(w => w.DESC_CORTA_DOMINIO.ToUpper().Contains(entidad.DESC_CORTA_DOMINIO.ToUpper())).Where(w => w.NOM_DOMINIO == entidad.NOM_DOMINIO && w.ID_DOMINIO_PADRE != 0);
                //lista = GetAll().OrderByDescending(w => w.ID_DOMINIO);
                else if(!string.IsNullOrEmpty(entidad.DESC_LARGA_DOMINIO) )
                    lista = FindAll(w => w.DESC_LARGA_DOMINIO.ToUpper().Contains(entidad.DESC_LARGA_DOMINIO.ToUpper())).Where(w => w.NOM_DOMINIO == entidad.NOM_DOMINIO && w.ID_DOMINIO_PADRE != 0);
                else if(entidad.FLG_ESTADO != 2)
                    lista = FindAll(w => w.FLG_ESTADO == entidad.FLG_ESTADO).Where(w => w.NOM_DOMINIO == entidad.NOM_DOMINIO && w.ID_DOMINIO_PADRE != 0);
                else
                 lista = GetAll().OrderByDescending(w => w.ID_DOMINIO).Where(w => w.NOM_DOMINIO == entidad.NOM_DOMINIO && w.ID_DOMINIO_PADRE != 0);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                lista = new List<Cls_Ent_Dominio>();
            }
            return lista;
        }


        public IEnumerable<Cls_Ent_Dominio> Dominio_Buscar(ref Cls_Ent_Auditoria auditoria, long id = 0, string descripcion = null)
        {
            auditoria.Limpiar();
            IEnumerable<Cls_Ent_Dominio> entidad = new List<Cls_Ent_Dominio>();

            try
            {
                if (id != 0)
                    entidad = FindAll(w => w.ID_DOMINIO == id);
                else if (descripcion != null)
                    entidad = FindAll(e => e.DESC_CORTA_DOMINIO.ToUpper() == descripcion.ToUpper());
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
            return entidad;
        }

        public void Dominio_Registrar(Cls_Ent_Dominio entDominio, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();

            try
            {
                Add(entDominio);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }

        }

        public void Dominio_Actualizar(Cls_Ent_Dominio entDominio, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                Update(entDominio, entDominio.ID_DOMINIO);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Dominio_Actualizar_Dominio(Cls_Ent_Dominio entDominio, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                List<Cls_Ent_Dominio> Mientidad = (List<Cls_Ent_Dominio>)Dominio_Buscar(ref auditoria, entDominio.ID_DOMINIO);
                List<Cls_Ent_Dominio> buscar = new List<Cls_Ent_Dominio>();
                buscar = (List<Cls_Ent_Dominio>)Dominio_Buscar(ref auditoria, 0, entDominio.DESC_CORTA_DOMINIO);
                bool Valido = true;
                if (buscar.Count() > 0)
                {
                    foreach (var item in buscar)
                    {
                        if (item.DESC_CORTA_DOMINIO.ToUpper().Equals(entDominio.DESC_CORTA_DOMINIO))
                        {
                                if (item.ID_DOMINIO != Mientidad[0].ID_DOMINIO)
                                {
                                    Valido = false;
                                    break;
                                }
                        }
                    }
                }
                if (Valido)
                {
                    Mientidad[0].DESC_CORTA_DOMINIO = entDominio.DESC_CORTA_DOMINIO;
                    Mientidad[0].DESC_LARGA_DOMINIO = entDominio.DESC_LARGA_DOMINIO;
                    Mientidad[0].COD_DOMINIO = entDominio.COD_DOMINIO;
                    Mientidad[0].IP_MODIFICACION = entDominio.IP_MODIFICACION;
                    Mientidad[0].FEC_MODIFICACION = DateTime.Now;
                    Mientidad[0].USU_MODIFICACION = entDominio.USU_MODIFICACION;
                    Dominio_Actualizar(Mientidad[0], ref auditoria);
                }
                else
                {
                    auditoria.Rechazar("Descripción ya existe");
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Dominio_Estado(Cls_Ent_Dominio entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            List<Cls_Ent_Dominio> Mientidad = (List<Cls_Ent_Dominio>)Dominio_Buscar(ref auditoria, entidad.ID_DOMINIO);
            try
            {
                if (Mientidad != null)
                {
                    Mientidad[0].FLG_ESTADO = entidad.FLG_ESTADO;
                    Mientidad[0].IP_MODIFICACION = entidad.IP_MODIFICACION;
                    Mientidad[0].FEC_MODIFICACION = DateTime.Now;
                    Mientidad[0].USU_MODIFICACION = entidad.USU_MODIFICACION;
                    Dominio_Actualizar(Mientidad[0], ref auditoria);
                }
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Dominio_Eliminar(Cls_Ent_Dominio entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            //List<Cls_Ent_Dominio> Mientidad = (List<Cls_Ent_Dominio>)Dominio_Buscar(ref auditoria, entidad.ID_DOMINIO);
            try
            {
                    DeleteUno(entidad.ID_DOMINIO);
    
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }

        public void Dominio_Agregar_Dominio(Cls_Ent_Dominio entidad, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            bool Valido = true;
            List<Cls_Ent_Dominio> buscar = Dominio_Buscar(ref auditoria, entidad.ID_DOMINIO, entidad.DESC_CORTA_DOMINIO).ToList();

            try
            {
                if (buscar.Count() > 0)
                {
                    foreach (var item in buscar)
                    {
                        if (item.DESC_CORTA_DOMINIO.ToUpper().Equals(entidad.DESC_CORTA_DOMINIO))
                        {            
                                Valido = false;
                                break;
                        }
                    }
                }

                if (Valido)
                {
               
                        entidad.ID_DOMINIO = GetSequence(SECUENCIA);
                        entidad.FLG_ESTADO = 1;
                        entidad.FEC_CREACION = DateTime.Now;
                        Dominio_Registrar(entidad, ref auditoria);
                
                }
                else
                    auditoria.Rechazar("Descripción ya existe");
            }
            catch (Exception ex)
            {

                auditoria.Error(ex);
            }
        }

    }
}
