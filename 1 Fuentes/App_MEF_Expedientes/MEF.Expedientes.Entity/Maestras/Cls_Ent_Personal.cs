using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MEF.Expedientes.Entity.Maestras
{
    [Table("V_PERSONAL")]
  public   class Cls_Ent_Personal
    {
        [Key]
        public string ID_PERSONAL { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string REGIMEN_LABORAL { get; set; }
        public string OFICINA { get; set; }



    }
}
