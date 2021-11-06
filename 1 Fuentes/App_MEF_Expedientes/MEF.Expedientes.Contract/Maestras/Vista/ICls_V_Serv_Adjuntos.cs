using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras.Vistas;
using System.Collections.Generic;
namespace MEF.Expedientes.Contract.Maestras.Vista
{
    public interface  ICls_V_Serv_Adjuntos
    {
        IEnumerable<Cls_v_Adjuntos> Adjuntos_Listar(Cls_v_Adjuntos param, ref Cls_Ent_Auditoria auditoria);

    }
}


