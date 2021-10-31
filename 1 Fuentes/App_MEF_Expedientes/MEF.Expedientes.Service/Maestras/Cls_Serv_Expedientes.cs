using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras;
using MEF.Expedientes.Repository;
using MEF.Expedientes.Contract.Maestras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF.Expedientes.Service.Maestras
{
    public class Cls_Serv_Expedientes : Repository<Cls_Ent_Expedientes>, ICls_Serv_Expedientes
    {
        readonly string SECUENCIA = "SEQ_ID_EXPEDIENTES";
        public Cls_Ent_Expedientes Expedientes_Codigo_Listar( ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
           Cls_Ent_Expedientes lista;
            try
            {
                lista = new Cls_Ent_Expedientes(); 


                string sql = "SELECT COUNT(*) FROM T_EXPM_EXPEDIENTES";
                long query = GetQuery(sql);
                //lista = FindAll(w => w.DESC_CORTA.ToUpper().Contains(desc_oficina.ToUpper()));
                //var count = FindAllCount(w => w.ID_EXPEDIENTE != null);
                lista.ID_EXPEDIENTE = GetSequence(SECUENCIA);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                lista = new Cls_Ent_Expedientes();
            }
            return lista;
        }

    }
}
