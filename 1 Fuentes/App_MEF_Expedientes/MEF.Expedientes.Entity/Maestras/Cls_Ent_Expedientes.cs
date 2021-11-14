using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MEF.Expedientes.Entity.Maestras
{
    [Table("T_EXPM_EXPEDIENTES")]
    public class Cls_Ent_Expedientes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ID_EXPEDIENTE { get; set; }
        public string ID_PERSONAL { get; set; }
        public string COD_EXPEDIENTE { get; set; }
        public DateTime? FECHA_RECEPCION { get; set; }
        public DateTime? FECHA_PRESCRIPCION { get; set; }
        public DateTime? FECHA_HECHO { get; set; }
        public string HOJA_RUTA { get; set; }
        public long ID_REMITENTE { get; set; }
        public long? ID_ACTO { get; set; }
        public string OBSERVACION_INVESTIGADORA { get; set; }
        public long? ID_FALTA { get; set; }
        public string ARTICULO { get; set; }
        public string INC { get; set; }
        public string OBSERVACION_INSTRUCTORA { get; set; }
        public long? ID_PRECALIFICACION { get; set; }
        public string TIPO_SANCION_RECOMENDADA { get; set; }
        public string ACTO_INICIO { get; set; }
        public DateTime? FECHA_NOTIFICACION { get; set; }
        public string RECOMENDACION_PREINFORME { get; set; }
        public long? ID_SANCION_RECOMENDADA { get; set; }
        public long ID_ORGANO_INSTRUCTOR { get; set; }
        public DateTime? FECHA_NOTIFICACION_INICIO { get; set; }
        public string DOCUMENTO_FINALIZACION { get; set; }

        public string RECOMENDACION_INSTRUCTOR { get; set; }
        public long ID_ORGANO_SANCIONADOR { get; set; }
        public string SANCION { get; set; }
        public string OBSERVACION_SANCIONADORA { get; set; }
        public int? DIAS_VIGENTE { get; set; }
        public string DOCUMENTO_NOTIFICA { get; set; }
        public long? ID_SITUACION { get; set; }
        public long? ID_ESTADO { get; set; }
        public int FLG_ESTADO { get; set; }

        public string NOMBRE_ARCHIVO { get; set; }

        public string EXTENSION_ARCHIVO { get; set; }

        public Byte[] ARCHIVO_BLOB { get; set; }
        public string USU_CREACION { get; set; }
        public DateTime? FEC_CREACION { get; set; }
        public string USU_MODIFICACION { get; set; }
        public string IP_CREACION { get; set; }

        public DateTime? FEC_MODIFICACION { get; set; }

        public string IP_MODIFICACION { get; set; }
        
    }
}
