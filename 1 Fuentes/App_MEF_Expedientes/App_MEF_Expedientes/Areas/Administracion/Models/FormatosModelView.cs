using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_MEF_Expedientes.Areas.Administracion.Models
{
    public class FormatosModelView
    {
        public long ID_FORMATO { get; set; }


        [Display(Name = "Asunto:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Asunto] es obligatorio")]
        public string ASUNTO { get; set; }


        [Display(Name = "Formato:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Formato] es obligatorio")]
        public string BODY { get; set; }



        public string Accion { get; set; }
    }
}