using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MEF.Expedientes.Entity.Maestras
{
    [Table("T_EXPM_DOMINIO")]
  public   class Cls_Ent_Dominio
    {
        [Key]
        public long ID_DOMINIO     { get; set; }
        public string COD_DOMINIO { get; set; }
        public string NOM_DOMINIO { get; set; }
        public string DESC_CORTA_DOMINIO { get; set; }
        public string DESC_LARGA_DOMINIO { get; set; }

        public int FLG_ESTADO { get; set; }
        public string USU_CREACION { get; set; }
        public DateTime? FEC_CREACION { get; set; }

        public string USU_MODIFICACION { get; set; }
        public DateTime? FEC_MODIFICACION { get; set; }
 

    }
}
