using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras.Vistas;
using System.Collections.Generic;
namespace MEF.Expedientes.Contract.Maestras.Vista
{
    public interface  ICls_V_Serv_Expedientes
    {
        List<Cls_v_Expedientes> Expedientes_Listar_Todo(string ORDEN_COLUMNA, string ORDEN, int FILAS, int PAGINA, string @WHERE, ref Cls_Ent_Auditoria auditoria);
        IEnumerable<Cls_v_Expedientes> Expedientes_V_Buscar(ref Cls_Ent_Auditoria auditoria, Cls_v_Expedientes entidad);
         IEnumerable<Cls_v_Expedientes> Expedientes_V_GetAll(ref Cls_Ent_Auditoria auditoria, Cls_v_Expedientes Param);
        IEnumerable<Cls_v_Expedientes> Expedientes_V_Sanciones(ref Cls_Ent_Auditoria auditoria, Cls_v_Expedientes Param);

        
    }
}
