using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace App_MEF_Expedientes.Models
{
    public class LoginModelView
    {
        [Display(Name = "username:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Usuario] es obligatorio")]
        public string LOGIN { get; set; }

        [Display(Name = "Contraseña:")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 3)]
        [Required(ErrorMessage = "[Contraseña] es obligatorio")]
        public string PASSWORD { get; set; }
    }
}