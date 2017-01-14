using System.Configuration;

namespace PairingTest.Web.Helpers
{
    public class AppSettingsConfiguration : IAppConfiguration
    {
        public string QuestionnaireUrl => ConfigurationManager.AppSettings["QuestionnaireServiceUri"];
    }
}