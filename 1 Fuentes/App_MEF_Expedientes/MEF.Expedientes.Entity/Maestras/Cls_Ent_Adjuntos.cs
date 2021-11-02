using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MEF.Expedientes.Entity.Maestras
{
    [Table("T_EXPD_ADJUNTOS")]
  public   class Cls_Ent_Adjuntos
    {
        [Key]
        public long ID_ADJUNTO { get; set; }
        public long ID_MAESTRA { get; set; }
        public long ID_TIPO_ARCHIVO { get; set; }
        public int NUMERO { get; set; }
        public DateTime? FECHA { get; set; }

        public string OBSERVACION { get; set; }

        public Byte[] ARCHIVO { get; set; }

        public int ID_SISTEMA { get; set; }

        public int FLG_ESTADO { get; set; }
        public string USU_CREACION { get; set; }


        public DateTime? FEC_CREACION { get; set; }


        public string USU_MODIFICACION { get; set; }
        public string IP_CREACION { get; set; }

        public DateTime? FEC_MODIFICACION { get; set; }

        public string IP_MODIFICACION { get; set; }

    }
}
