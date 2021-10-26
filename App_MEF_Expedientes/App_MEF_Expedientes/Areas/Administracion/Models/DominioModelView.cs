using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_MEF_Expedientes.Areas.Administracion.Models
{
    public class DominioModelView
    {
        public long ID_DOMINIO { get; set; }


        [Display(Name = "Código:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Código] es obligatorio")]
        public string COD_DOMINIO { get; set; }


        [Display(Name = "Descripción Corta:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Descripción Corta] es obligatorio")]
        public string DESC_CORTA_DOMINIO { get; set; }


        [Display(Name = "Descripción Larga:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Descripción Larga] es obligatorio")]
        public string DESC_LARGA_DOMINIO { get; set; }


        public string Accion { get; set; }

    }
}