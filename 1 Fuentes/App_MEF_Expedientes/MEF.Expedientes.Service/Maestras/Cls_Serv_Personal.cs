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
    public  class Cls_Serv_Personal : Repository<Cls_Ent_Personal>, ICls_Serv_Personal
    {

        public IEnumerable<Cls_Ent_Personal> Buscar_Personal_Listar(string nombres_ape, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            IEnumerable<Cls_Ent_Personal> lista;

            try
            {
                    lista = FindAll(w => w.NOMBRE_COMPLETO.ToUpper().Contains(nombres_ape.ToUpper()));
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                lista = new List<Cls_Ent_Personal>();
            }
            return lista;
        }
    }
}
