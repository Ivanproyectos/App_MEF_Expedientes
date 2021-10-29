using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras;
using System.Collections.Generic;

namespace MEF.Expedientes.Contract.Maestras
{
    public interface ICls_Serv_Oficina
    {
        IEnumerable<Cls_Ent_Oficina> Buscar_Oficina_Listar(string desc_oficina, ref Cls_Ent_Auditoria auditoria);

    }
}
