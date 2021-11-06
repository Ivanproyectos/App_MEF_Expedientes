using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Login;
using System.Collections.Generic;

namespace MEF.Expedientes.Contract.Login
{
    public interface ICls_Serv_Login
    {
        List<Cls_Ent_Usuario> Login_Listar(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria);
    }
}
