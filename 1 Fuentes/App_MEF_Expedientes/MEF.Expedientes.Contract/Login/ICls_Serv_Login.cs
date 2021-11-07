using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Login;
using System.Collections.Generic;

namespace MEF.Expedientes.Contract.Login
{
    public interface ICls_Serv_Login
    {
       Cls_Ent_Usuario Usuario(Cls_Ent_Usuario entidad, ref Cls_Ent_Auditoria auditoria);

        List<Cls_Ent_Modulos> Modulos_Usuarios(Cls_Ent_Modulos param, ref Cls_Ent_Auditoria auditoria);
    }
}
