using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_MEF_Expedientes.Areas.Maestras.Models
{
    public class ExpedientesModelView
    {
        public long ID_EXPEDIENTE { get; set; }


        [Display(Name = "Personal:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Personal] es obligatorio")]
        public string SEARCH_PERSONAL { get; set; }

        public string ID_PERSONAL { get; set; }
       

        [Display(Name = "N° de Expedientes:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[N° de Expedientes] es obligatorio")]
        public string COD_EXPEDIENTE { get; set; }


        [Display(Name = "Fecha Recepción:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Fecha Recepción] es obligatorio")]
        public string FECHA_RECEPCION { get; set; }


        [Display(Name = "Fecha Prescripción:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Fecha Prescripción] es obligatorio")]
        public string FECHA_PRESCRIPCION { get; set; }



        [Display(Name = "Fecha Hecho:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Fecha Hecho] es obligatorio")]
        public string FECHA_HECHO { get; set; }


        [Display(Name = "Hoja Ruta:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Hoja Ruta] es obligatorio")]
        public string HOJA_RUTA { get; set; }



        [Display(Name = "Remitente:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Remitente] es obligatorio")]
        public string SEARCH_REMITENTE { get; set; }
        public List<SelectListItem> Lista_Remitente { get; set; }

        public long ID_REMITENTE { get; set; }

        [Display(Name = "Regimen Laboral:")]
        public string REGIMEN_LABORAL { get; set; }
        [Display(Name = "Cargo:")]
        public string   CARGO { get; set; }
        [Display(Name = "Oficina:")]
        public string OFICINA { get; set; }

        [Display(Name = "Acto:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Acto] es obligatorio")]
        public long ID_ACTO { get; set; }
        public List<SelectListItem> Lista_Acto { get; set; }


        [Display(Name = "Observación:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Observación] es obligatorio")]
        public string OBSERVACION_INVESTIGADORA { get; set; }



        [Display(Name = "Falta:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Falta] es obligatorio")]
        public long ID_FALTA { get; set; }

        public List<SelectListItem> Lista_Falta { get; set; }
        

        [Display(Name = "Articulo:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Articulo] es obligatorio")]
        public string ARTICULO { get; set; }



        [Display(Name = "Inc:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Inc] es obligatorio")]
        public string INC { get; set; }


        [Display(Name = "Observación:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Observación] es obligatorio")]
        public string OBSERVACION_INSTRUCTORA { get; set; }


        [Display(Name = "Precalificación:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Precalificación] es obligatorio")]
        public long ID_PRECALIFICACION { get; set; }
        public List<SelectListItem> Lista_Precalifacion { get; set; }




        [Display(Name = "Tipo sanción Recomendada:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Tipo sanción Recomendada] es obligatorio")]
        public string TIPO_SANCION_RECOMENDADA { get; set; }



        [Display(Name = "Acto Inicio:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Acto Inicio] es obligatorio")]
        public string ACTO_INICIO { get; set; }


        [Display(Name = "Fecha Notificación:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Fecha Notificación] es obligatorio")]
        public string FECHA_NOTIFICACION { get; set; }


        [Display(Name = "Recomendación Preinforme:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Recomendación Preinforme] es obligatorio")]
        public string RECOMENDACION_PREINFORME { get; set; }


        [Display(Name = "Sanción Recomenda:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Sanción Recomenda] es obligatorio")]
        public long ID_SANCION_RECOMENDADA { get; set; }
        public List<SelectListItem> Lista_Sacion_Recomendada { get; set; }


        [Display(Name = "Organo Instuctor:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Organo Instuctor] es obligatorio")]
        public string SEARCH_ORGANO_INSTRUCTOR { get; set; }
        public long ID_ORGANO_INSTRUCTOR { get; set; }
        public List<SelectListItem> Lista_Organo_Instructor { get; set; }



        [Display(Name = "Fecha Noticación:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Fecha Noticación] es obligatorio")]
        public string FECHA_NOTIFICACION_INICIO { get; set; }




        [Display(Name = "Documento Finalizción:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Documento Finalizción] es obligatorio")]
        public string DOCUMENTO_FINALIZACION { get; set; }




        [Display(Name = "Recomendación Instructor:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Recomendación Instructor] es obligatorio")]
        public string RECOMENDACION_INSTRUCTOR { get; set; }




        [Display(Name = "Organo Sancionador:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Organo Sancionador] es obligatorio")]
        public string SEARCH_ORGANO_SANCIONADOR { get; set; }
        public long ID_ORGANO_SANCIONADOR { get; set; }
        public List<SelectListItem> Lista_Organo_Sancionador{ get; set; }



        [Display(Name = "Sanción")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Sanción] es obligatorio")]
        public string SANCION { get; set; }



        [Display(Name = "Observación:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Observación] es obligatorio")]
        public string OBSERVACION_SANCIONADORA { get; set; }




        [Display(Name = "Situación:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Situación] es obligatorio")]
        public long ID_SITUACION { get; set; }
        public List<SelectListItem> Lista_Situacion { get; set; }



        [Display(Name = "Estado:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Estado] es obligatorio")]
        public long ID_ESTADO { get; set; }
        public List<SelectListItem> Lista_Estado { get; set; }


        public int ID_SITUACION_SEARCH { get; set; }
        public string Accion { get; set; }

        public int ID_ESTADO_SEARCH { get; set; }
        

    }
}