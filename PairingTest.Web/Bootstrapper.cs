using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using PairingTest.Web.Services;
using PairingTest.Web.Helpers;
using PairingTest.Web.Http;
using PairingTest.Web.Models;

namespace PairingTest.Web
{
    public class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();            
            RegisterTypes(container);
            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IQuestionnaireService, QuestionnaireService>();

            container.RegisterType<IAppConfiguration, AppSettingsConfiguration>();
            container.RegisterType<IHttpClient, JsonHttpClient>();
            container.RegisterType<IApiResponse<QuestionnaireViewModel>, JsonHttpResponse<QuestionnaireViewModel>>();
        }
    }
}