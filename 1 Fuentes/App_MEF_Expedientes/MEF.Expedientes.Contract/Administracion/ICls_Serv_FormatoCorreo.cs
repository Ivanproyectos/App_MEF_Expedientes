using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Administracion;
using System.Collections.Generic;

namespace MEF.Expedientes.Contract.Administracion
{
    public interface ICls_Serv_FormatoCorreo
    {
        IEnumerable<Cls_Ent_FormatoCorreo> FormatoCorreo_Listar(Cls_Ent_FormatoCorreo entidad, ref Cls_Ent_Auditoria auditoria);
        void FormatoCorreo_Registrar(Cls_Ent_FormatoCorreo entFormatoCorreo, ref Cls_Ent_Auditoria auditoria);
        IEnumerable<Cls_Ent_FormatoCorreo> FormatoCorreo_Buscar(ref Cls_Ent_Auditoria auditoria, long id = 0, string descripcion = null);
        void FormatoCorreo_Actualizar(Cls_Ent_FormatoCorreo entFormatoCorreo, ref Cls_Ent_Auditoria auditoria);
        void FormatoCorreo_Actualizar_FormatoCorreo(Cls_Ent_FormatoCorreo entFormatoCorreo, ref Cls_Ent_Auditoria auditoria);
        void FormatoCorreo_Estado(Cls_Ent_FormatoCorreo entFormatoCorreo, ref Cls_Ent_Auditoria auditoria);
        void FormatoCorreo_Eliminar(Cls_Ent_FormatoCorreo entFormatoCorreo, ref Cls_Ent_Auditoria auditoria);
        void FormatoCorreo_Agregar_FormatoCorreo(Cls_Ent_FormatoCorreo entFormatoCorreo, ref Cls_Ent_Auditoria auditoria);
    

  }
}
