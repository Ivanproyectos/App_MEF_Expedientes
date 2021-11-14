using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MEF.Expedientes.Entity.Maestras.Vistas
{
    [Table("V_EXPEDIENTES")]
    public class Cls_v_Expedientes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ID_EXPEDIENTE { get; set; }
        public string ID_PERSONAL { get; set; }
        public string COD_EXPEDIENTE { get; set; }
        public string FECHA_RECEPCION { get; set; }
        public string REGIMEN_LABORAL { get; set; }
        public string OFICINA { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string FECHA_PRESCRIPCION { get; set; }
        public string FECHA_HECHO { get; set; }
        public string HOJA_RUTA { get; set; }
        public long ID_REMITENTE { get; set; }
        public string REMITENTE { get; set; }
        public long ID_ACTO { get; set; }

        public string ACTO { get; set; }
        public string OBSERVACION_INVESTIGADORA { get; set; }
        public long ID_FALTA { get; set; }
        public string FALTA { get; set; }
        public string ARTICULO { get; set; }
        public string INC { get; set; }
        public string OBSERVACION_INSTRUCTORA { get; set; }
        public long ID_PRECALIFICACION { get; set; }
        public string TIPO_SANCION_RECOMENDADA { get; set; }
        public string ACTO_INICIO { get; set; }
        public string FECHA_NOTIFICACION { get; set; }
        public string RECOMENDACION_PREINFORME { get; set; }
        public long ID_SANCION_RECOMENDADA { get; set; }

        public string SANCION { get; set; }
        public string SANCION_RECOMENDADA { get; set; }
        
        public long ID_ORGANO_INSTRUCTOR { get; set; }

        public string ORGANO_INSTRUCTOR { get; set; }
        
        public string FECHA_NOTIFICACION_INICIO { get; set; }

        public string DOCUMENTO_FINALIZACION { get; set; }

        public string RECOMENDACION_INSTRUCTOR { get; set; }
        public long ID_ORGANO_SANCIONADOR { get; set; }

        public string ORGANO_SANCIONADOR { get; set; }

        public string OBSERVACION_SANCIONADORA { get; set; }

        public long ID_SITUACION { get; set; }
        public int? DIAS_VIGENTE { get; set; }
        public string DOCUMENTO_NOTIFICA { get; set; }
        public string EXTENSION_ARCHIVO { get; set; }

        public string SITUACION { get; set; }
        public long ID_ESTADO { get; set; }
        public string ESTADO { get; set; }
        public int FLG_ESTADO { get; set; }
        public string DNI { get; set; }
        public string USU_CREACION { get; set; }
        public string FEC_CREACION { get; set; }
        public string USU_MODIFICACION { get; set; }
        public string IP_CREACION { get; set; }
        public string FEC_MODIFICACION { get; set; }
        public string IP_MODIFICACION { get; set; }
        public string DESC_PUESTO { get; set; }
        

    }
}
