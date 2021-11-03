using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MEF.Expedientes.Entity.Maestras.Vistas
{
    [Table("V_ADJUNTOS")]
    public class Cls_v_Adjuntos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ID_ADJUNTO { get; set; }
        public long ID_MAESTRA { get; set; }
        public long ID_TIPO_ARCHIVO { get; set; }
        public string DESC_ARCHIVO { get; set; }
        public int NUMERO { get; set; }
        public string FECHA { get; set; }
        public string OBSERVACION { get; set; }
        public string NOMBRE_ARCHIVO { get; set; }
 
        public int FLG_ESTADO { get; set; }
        public string USU_CREACION { get; set; }

        public string FEC_CREACION { get; set; }

        public string USU_MODIFICACION { get; set; }
  

        public string FEC_MODIFICACION { get; set; }

  
        
    }
}
