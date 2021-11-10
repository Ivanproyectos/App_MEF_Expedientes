using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_MEF_Expedientes.Models;
using App_MEF_Expedientes.Controllers;

namespace App_MEF_Expedientes.Filters
{
    public class VerifySession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var Ouser = HttpContext.Current.Session["USUARIO"];
            if (Ouser == null)
            {
             if(filterContext.Controller is HomeController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Login/Index"); 
                }
            
            }
            base.OnActionExecuting(filterContext); 
        }

    }
}