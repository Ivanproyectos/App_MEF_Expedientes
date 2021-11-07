using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF.Expedientes.Entity.Login
{
  public class Cls_Ent_Modulos
    {
        public int ID_SISTEMA_MODULO { get; set; }
        public int ID_PERFIL { get; set; }
        public int ID_SISTEMA { get; set; }
        public long ID_USUARIO { get; set; }
        public int ID_TIPO_MODULO { get; set; }
        public string DESC_TIPO_MODULO { get; set; }
        public int ID_SISTEMA_MODULO_PADRE { get; set; }
        public string ID_A { get; set; }
        public string ID_LI { get; set; }
        public string IMAGEN { get; set; }
        public string URL_MODULO { get; set; }
        public string DESC_MODULO { get; set; }
        public int ORDEN { get; set; }
        public int NIVEL { get; set; }

        public List<Cls_Ent_Modulos> Modulos_Hijos { get; set; }

    }
}
