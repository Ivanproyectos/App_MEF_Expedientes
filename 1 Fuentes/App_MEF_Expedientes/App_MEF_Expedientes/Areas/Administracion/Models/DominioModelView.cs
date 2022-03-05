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


        /*model Correlativo Expediente*/

        [Display(Name = "Año:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Año] es obligatorio")]
        public int ANIO { get; set; }


        [Display(Name = "Número Expediente:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Número Expediente] es obligatorio")]
        public string NUMERO_EXPEDIENTE { get; set; }


        [Display(Name = "Correlativo:")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "[Correlativo] es obligatorio")]
        public int CORRELATIVO { get; set; }

        public string Accion { get; set; }

    }
}