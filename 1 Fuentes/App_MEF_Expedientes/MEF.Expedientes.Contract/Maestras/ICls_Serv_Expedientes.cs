using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras;
using System.Collections.Generic;

namespace MEF.Expedientes.Contract.Maestras
{
    public interface ICls_Serv_Expedientes
    {
       Cls_Ent_Expedientes Expedientes_Codigo_Listar(ref Cls_Ent_Auditoria auditoria);

   

    }
}
