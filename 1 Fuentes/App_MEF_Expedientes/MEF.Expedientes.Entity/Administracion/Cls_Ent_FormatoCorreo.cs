using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MEF.Expedientes.Entity.Administracion
{
    [Table("T_EXPM_FORMATO_CORREO")]
    public class Cls_Ent_FormatoCorreo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ID_FORMATO { get; set; }
  
        public string ASUNTO { get; set; }
        public string BODY { get; set; }
        public int FLG_ESTADO { get; set; }
        public string USU_CREACION { get; set; }
        public DateTime? FEC_CREACION { get; set; }
        public string USU_MODIFICACION { get; set; }
        public string IP_CREACION { get; set; }
        public DateTime? FEC_MODIFICACION { get; set; }
        public string IP_MODIFICACION { get; set; }

    }
}
