using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Administracion;
using System.Collections.Generic;

namespace MEF.Expedientes.Contract.Administracion
{
    public interface ICls_Serv_Dominio
    {
        IEnumerable<Cls_Ent_Dominio> Dominio_Listar(Cls_Ent_Dominio entidad, ref Cls_Ent_Auditoria auditoria);
        void Dominio_Registrar(Cls_Ent_Dominio entDominio, ref Cls_Ent_Auditoria auditoria);
        IEnumerable<Cls_Ent_Dominio> Dominio_Buscar(ref Cls_Ent_Auditoria auditoria, long id = 0, string descripcion = null);
        void Dominio_Actualizar(Cls_Ent_Dominio entDominio, ref Cls_Ent_Auditoria auditoria);
        void Dominio_Actualizar_Dominio(Cls_Ent_Dominio entDominio, ref Cls_Ent_Auditoria auditoria);
        void Dominio_Estado(Cls_Ent_Dominio entDominio, ref Cls_Ent_Auditoria auditoria);
        void Dominio_Eliminar(Cls_Ent_Dominio entDominio, ref Cls_Ent_Auditoria auditoria);
        void Dominio_Agregar_Dominio(Cls_Ent_Dominio entDominio, ref Cls_Ent_Auditoria auditoria);
    

  }
}
