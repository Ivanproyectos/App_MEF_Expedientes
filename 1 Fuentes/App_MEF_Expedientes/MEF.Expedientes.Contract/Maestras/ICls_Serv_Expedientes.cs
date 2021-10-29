using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras;
using System.Collections.Generic;

namespace MEF.Expedientes.Contract.Maestras
{
    public interface ICls_Serv_Expedientes
    {
        IEnumerable<Cls_Ent_Personal> Buscar_Personal_Listar(string nombres_ape, ref Cls_Ent_Auditoria auditoria);

        IEnumerable<Cls_Ent_Oficina> Buscar_Oficina_Listar(string desc_oficina, ref Cls_Ent_Auditoria auditoria);

    }
}
