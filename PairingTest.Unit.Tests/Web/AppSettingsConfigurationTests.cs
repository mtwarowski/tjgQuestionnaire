using NUnit.Framework;
using PairingTest.Web.Helpers;
using System.Configuration;
using System;

namespace PairingTest.Unit.Tests.Web
{
    [TestFixture]
    public class AppSettingsConfigurationTests
    {
        [Test]
        public void QuestionnaireUrl_GetsDataFromAppSettings()
        {
            //Arrange
            const string key = "QuestionnaireServiceUri";
            const string value = "Value Stored In Configs";
            AddToAppSettings(key, value);

            //Act
            var result = new AppSettingsConfiguration().QuestionnaireUrl;

            //Assert
            Assert.AreEqual(value, result);
        }

        private void AddToAppSettings(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Clear();
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Full);
        }
    }
}
