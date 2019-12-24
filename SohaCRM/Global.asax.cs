using Core.Data;
using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Framework.Mvc;
using FluentValidation.Mvc;
using Core.Domain.Users;
using Services;

namespace SohaCRM
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        public static void RegisterRoutes(RouteCollection routes)
        {

        }


        protected void Application_Start()
        {
            
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //most of API providers require TLS 1.2 nowadays
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //disable "X-AspNetMvc-Version" header name
            MvcHandler.DisableMvcResponseHeader = true;

            //initialize engine context
            EngineContext.Initialize(false);

            bool databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();

            //Add some functionality on top of the default ModelMetadataProvider
            ModelMetadataProviders.Current = new NopMetadataProvider();

            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            //fluent validation
            //DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            //ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new NopValidatorFactory()));


        }
    }
}
