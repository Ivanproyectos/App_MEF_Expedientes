using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.Expedientes.Entity; 

namespace App_MEF_Expedientes.Areas.Maestras.Models
{
    public class AdjuntosModelView
    {

        public long ID_ADJUNTO { get; set; }

        public long ID_MAESTRA { get; set; }

        [Display(Name = "Tipo Archivo:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Tipo Archivo] es obligatorio")]
        public long ID_TIPO_ARCHIVO { get; set; }
        public List<SelectListItem> Lista_Tipo_Archivo { get; set; }



        [Display(Name = "Número:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Número] es obligatorio")]
        public int NUMERO { get; set; }


        [Display(Name = "Fecha:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Fecha] es obligatorio")]
        public string FECHA { get; set; }


        [Display(Name = "Obervación:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Obervación] es obligatorio")]
        public string OBSERVACION { get; set; }

        public string COD_EXPEDIENTE { get; set; }
        public string Accion { get; set; }

        public Cls_Ent_Archivo MiArchivo { get; set; }
        public string USU_CREACION { get; set; }



    }
}