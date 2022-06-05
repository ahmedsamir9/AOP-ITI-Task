using AOPAPI.Aspects.Logging.By_Decoraator;
using AOPAPI.Aspects.Logging.By_interceptor;
using AOPAPI.Aspects.Utitiles;
using AOPAPI.BLL;
using AOPAPI.DAL;
using AOPAPI.DAL.Repositories;
using System;
using Unity;
using Unity.Injection;
using Unity.Interception;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.Lifetime;

namespace AOPAPI
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
              var container = new UnityContainer().AddExtension(new Diagnostic());
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
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<Context>(new PerResolveLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new PerResolveLifetimeManager());
            container.RegisterType<ICourseRepository, CourseRepository>(new PerResolveLifetimeManager());
            container.RegisterType<IUserService, UserService>(new PerResolveLifetimeManager());
            container.RegisterType<ICourseService, CourseService>(new PerResolveLifetimeManager());
            container.RegisterType<ILogger , Logger>(new PerResolveLifetimeManager());
            #region Decorator injection 
            //container.RegisterType<ICourseService, CourseService>(
            //                             "base",
            //      new PerResolveLifetimeManager()
            // );

            //container.RegisterType<ICourseService, CourseServiceLogger>(
            // new PerResolveLifetimeManager(),
            //new InjectionConstructor(new ResolvedParameter<ICourseService>("base"), new ResolvedParameter<ILogger>())
            //);

            #endregion
            //container.AddNewExtension<Interception>();

            //container.RegisterType<ICourseService, CourseService>(
            //   new PerResolveLifetimeManager(),
            //   new Interceptor<InterfaceInterceptor>(),
            //   new InterceptionBehavior<LoggingInterceptor>()
            //   );
          //  container.RegisterType<ILogger, Logger>(new PerResolveLifetimeManager());
        }
    }
}