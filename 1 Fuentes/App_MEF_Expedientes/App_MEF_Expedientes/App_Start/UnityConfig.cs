using System;
using MEF.Expedientes.Service.Maestras;
using MEF.Expedientes.Contract.Maestras;
using MEF.Expedientes.Contract.Administracion;
using MEF.Expedientes.Service.Administracion;
using Unity;

namespace App_MEF_Expedientes
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {

            container.RegisterType<ICls_Serv_Oficina, Cls_Serv_Oficina>();
            container.RegisterType<ICls_Serv_Dominio, Cls_Serv_Dominio>();
            container.RegisterType<ICls_Serv_Expedientes, Cls_Serv_Expedientes>();
            container.RegisterType<ICls_Serv_FormatoCorreo, Cls_Serv_FormatoCorreo>();
            container.RegisterType<ICls_Serv_Adjuntos, Cls_Serv_Adjuntos>();

            
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
        }
    }
}