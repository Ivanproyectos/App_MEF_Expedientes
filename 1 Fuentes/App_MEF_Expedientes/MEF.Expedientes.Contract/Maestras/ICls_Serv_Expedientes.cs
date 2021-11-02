using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras;
using System.Collections.Generic;

namespace MEF.Expedientes.Contract.Maestras
{
    public interface ICls_Serv_Expedientes
    {
        //IEnumerable<Cls_Ent_Expedientes> Expedientes_Listar(Cls_Ent_Expedientes entidad, ref Cls_Ent_Auditoria auditoria);

        void Expedientes_Registrar(Cls_Ent_Expedientes entExpedientes, ref Cls_Ent_Auditoria auditoria);
        IEnumerable<Cls_Ent_Expedientes> Expedientes_Buscar(ref Cls_Ent_Auditoria auditoria, long id = 0, string descripcion = null);
        void Expedientes_Actualizar(Cls_Ent_Expedientes entExpedientes, ref Cls_Ent_Auditoria auditoria);
        void Expedientes_Actualizar_Expedientes(Cls_Ent_Expedientes entExpedientes, ref Cls_Ent_Auditoria auditoria);
        void Expedientes_Estado(Cls_Ent_Expedientes entExpedientes, ref Cls_Ent_Auditoria auditoria);
        void Expedientes_Eliminar(Cls_Ent_Expedientes entExpedientes, ref Cls_Ent_Auditoria auditoria);
        void Expedientes_Agregar_Expedientes(Cls_Ent_Expedientes entExpedientes, ref Cls_Ent_Auditoria auditoria);



    }
}
