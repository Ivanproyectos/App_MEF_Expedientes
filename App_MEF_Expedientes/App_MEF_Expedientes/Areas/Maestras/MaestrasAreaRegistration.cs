using System.Web.Mvc;

namespace App_MEF_Expedientes.Areas.Maestras
{
    public class MaestrasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Maestras";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Maestras_default",
                "Maestras/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
