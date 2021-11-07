using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF.Expedientes.Entity.Login
{
   public class Cls_Ent_Usuario
    {

        public Cls_Ent_Usuario()
        {
            Perfil = new List<Cls_Ent_Usuario>();
        }
        public long ID_PERFIL { get; set; }
        public string DESC_PERFIL { get; set; }
        public long ID_USUARIO { get; set; }
        public string LOGIN_USUARIO { get; set; }

        public long ID_OFICINA { get; set; }

        public string NOMBRE_OFICINA { get; set; }

        public string SIGLA { get; set; }

        public string NOMBRE_PERSONA { get; set; }
        public int ID_SISTEMA { get; set; }

        public List<Cls_Ent_Usuario> Perfil { get; set; }



    }
}
