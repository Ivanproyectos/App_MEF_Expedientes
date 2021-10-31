using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras;
using MEF.Expedientes.Repository;
using MEF.Expedientes.Contract.Maestras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF.Expedientes.Service.Maestras
{
    public  class Cls_Serv_Oficina : Repository<Cls_Ent_Oficina>, ICls_Serv_Oficina
    {
        public IEnumerable<Cls_Ent_Oficina> Buscar_Oficina_Listar(string desc_oficina, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            IEnumerable<Cls_Ent_Oficina> lista;
            try
            {
                    lista = FindAll(w => w.DESC_CORTA.ToUpper().Contains(desc_oficina.ToUpper()));
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                lista = new List<Cls_Ent_Oficina>();
            }
            return lista;
        }


    }
}
