using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Maestras.Vistas;
using MEF.Expedientes.Repository;
using MEF.Expedientes.Contract.Maestras.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace MEF.Expedientes.Service.Maestras.Vista
{
   public  class Cls_V_Serv_Adjuntos : Repository<Cls_v_Adjuntos>, ICls_V_Serv_Adjuntos
    {

     
        public IEnumerable<Cls_v_Adjuntos> Adjuntos_Listar(long id,ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            IEnumerable<Cls_v_Adjuntos> entidad = new List<Cls_v_Adjuntos>();

            try
            {
                if (id != 0)
                    entidad = FindAll(w => w.ID_MAESTRA == id);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
            return entidad;
        }



    }
}
