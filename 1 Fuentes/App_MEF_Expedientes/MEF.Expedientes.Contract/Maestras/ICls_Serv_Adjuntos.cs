using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras;
using System.Collections.Generic;

namespace MEF.Expedientes.Contract.Maestras
{
    public interface ICls_Serv_Adjuntos
    {
        IEnumerable<Cls_Ent_Adjuntos> Adjuntos_Listar(Cls_Ent_Adjuntos entidad, ref Cls_Ent_Auditoria auditoria);
        void Adjuntos_Registrar(Cls_Ent_Adjuntos entAdjuntos, ref Cls_Ent_Auditoria auditoria);
        IEnumerable<Cls_Ent_Adjuntos> Adjuntos_Buscar(ref Cls_Ent_Auditoria auditoria, long id = 0, string descripcion = null);
        void Adjuntos_Actualizar(Cls_Ent_Adjuntos entAdjuntos, ref Cls_Ent_Auditoria auditoria);
        void Adjuntos_Actualizar_Adjuntos(Cls_Ent_Adjuntos entAdjuntos, ref Cls_Ent_Auditoria auditoria);
        void Adjuntos_Estado(Cls_Ent_Adjuntos entAdjuntos, ref Cls_Ent_Auditoria auditoria);
        void Adjuntos_Eliminar(Cls_Ent_Adjuntos entAdjuntos, ref Cls_Ent_Auditoria auditoria);
        void Adjuntos_Agregar_Adjuntos(Cls_Ent_Adjuntos entAdjuntos, ref Cls_Ent_Auditoria auditoria);
    }
}
