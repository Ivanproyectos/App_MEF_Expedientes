using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.Expedientes.Entity;

namespace App_MEF_Expedientes.Areas.Maestras.Controllers
{
    public class ArchivoController : Controller
    {
        // GET: Maestras/Archivo
        public ActionResult Guardar_Temporal_Archivo(HttpPostedFileBase MifileArchivo)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            if (MifileArchivo != null)
            {
                try
                {
                    var content = new byte[MifileArchivo.ContentLength];
                    MifileArchivo.InputStream.Read(content, 0, MifileArchivo.ContentLength);

                    string CODIGO_UNICO = Recursos.Clases.Css_Codigo.Generar_Codigo_Temporal();

                    decimal tamanio = content.Length; // OBTENEMOS EL ARCHIVO EN BYTES
                    string Nombreencriptado = CODIGO_UNICO + System.IO.Path.GetExtension(MifileArchivo.FileName).ToString();
                    //MisRutas = Recursos.Css_ServidorArchivos.RutaServidorTemporal();
                    //var ruta_link = System.Configuration.ConfigurationManager.AppSettings["Nombre_Sistema"].ToString() + "/" + MisRutas.RUTA + Nombreencriptado;

                    var ruta_link = Recursos.Clases.Css_Ruta.Ruta_Temporal() + Nombreencriptado;
                    var ruta = Recursos.Clases.Css_Ruta.Ruta_Temporal() + Nombreencriptado;
                    MifileArchivo.SaveAs(ruta);

                    Cls_Ent_Archivo entidad_archivo = new Cls_Ent_Archivo();
                    entidad_archivo.CODIGO_ARCHIVO = CODIGO_UNICO;
                    entidad_archivo.NOMBRE_ARCHIVO = System.IO.Path.GetFileName(MifileArchivo.FileName).ToString();
                    //entidad_archivo.RUTA_ARCHIVO = ruta;
                    entidad_archivo.RUTA_LINK = ruta_link;
                    entidad_archivo.EXTENSION = System.IO.Path.GetExtension(MifileArchivo.FileName).ToString();

                    auditoria.OBJETO = entidad_archivo;
                    auditoria.EJECUCION_PROCEDIMIENTO = true;
                }
                catch (Exception ex)
                {
                    auditoria.EJECUCION_PROCEDIMIENTO = false;
                    auditoria.MENSAJE_SALIDA = "No se encontró el archivo";
                    Recursos.Clases.Css_Log.Guardar(ex.ToString());
                }
            }
            else
            {
                auditoria.EJECUCION_PROCEDIMIENTO = false;
                auditoria.MENSAJE_SALIDA = "No se encontró el archivo";
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Archivo_Eliminar_Temporal(string CODIGO, string EXTENSION)
        {
            Cls_Ent_Auditoria auditoria = new Cls_Ent_Auditoria();
            try
            {
                var TEMPORAL = Recursos.Clases.Css_Ruta.Ruta_Temporal() + @"" + CODIGO + EXTENSION;
                if (System.IO.File.Exists(TEMPORAL))
                    System.IO.File.Delete(TEMPORAL);
                auditoria.Limpiar();
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
                string CodigoLog = Recursos.Clases.Css_Log.Guardar(ex.ToString());
                auditoria.MENSAJE_SALIDA = Recursos.Clases.Css_Log.Mensaje(CodigoLog);
            }
            return Json(auditoria, JsonRequestBehavior.AllowGet);
        }



    }
}
