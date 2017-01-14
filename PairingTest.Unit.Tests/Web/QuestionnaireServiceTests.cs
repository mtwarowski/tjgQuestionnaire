using Moq;
using NUnit.Framework;
using PairingTest.Web.Helpers;
using PairingTest.Web.Http;
using PairingTest.Web.Models;
using PairingTest.Web.Services;
using System.Threading.Tasks;

namespace PairingTest.Unit.Tests.Web
{
    [TestFixture]
    public class QuestionnaireServiceTests
    {
        private Mock<IAppConfiguration> mockConfiuration;
        private Mock<IHttpClient> mockHttpClient;
        private Mock<IApiResponse<QuestionnaireViewModel>> mockApiResponse;

        private QuestionnaireService target;

        [TestFixtureSetUp]
        public void Init()
        {
            mockConfiuration = new Mock<IAppConfiguration>();
            mockHttpClient = new Mock<IHttpClient>();
            mockApiResponse = new Mock<IApiResponse<QuestionnaireViewModel>>();
            mockHttpClient.Setup(x => x.GetAsync<QuestionnaireViewModel>(It.IsAny<string>())).ReturnsAsync(mockApiResponse.Object);

            target = new QuestionnaireService(mockConfiuration.Object, mockHttpClient.Object);
        }


        [Test]
        public async Task GetQuestionnaireAsync_ItsConfig()
        {
            //Act
            var result = await target.GetQuestionnaireAsync();

            //Assert
            mockConfiuration.Verify(m => m.QuestionnaireUrl);
        }

        [Test]
        public async Task GetQuestionnaireAsync_CallsHttpClientGetWithConfig()
        {
            //Arrange
            const string givenUrl = "QuestionnaireViewModelApiUrl";
            mockConfiuration.Setup(x => x.QuestionnaireUrl).Returns(givenUrl);

            //Act
            var result = await target.GetQuestionnaireAsync();

            //Assert
            mockHttpClient.Verify(m => m.GetAsync<QuestionnaireViewModel>(givenUrl), Times.Once());
        }

        [Test]
        public async Task GetQuestionnaireAsync_WhenCallUnsuccessful_ReturnsNull()
        {
            //Arrange
            mockHttpClient.Setup(x => x.GetAsync<QuestionnaireViewModel>(It.IsAny<string>())).ReturnsAsync(mockApiResponse.Object);
            mockApiResponse.Setup(x => x.IsSuccessful).Returns(false);

            //Act
            var result = await target.GetQuestionnaireAsync();

            //Assert
            Assert.Null(result);
        }

        [Test]
        public async Task GetQuestionnaireAsync_WhenCallSuccessful_ReturnsGivenObject()
        {
            //Arrange
            var givenQuestionnaireViewModel = new QuestionnaireViewModel { QuestionnaireTitle = "Expected Title"};
            mockApiResponse.Setup(x => x.IsSuccessful).Returns(true);
            mockApiResponse.Setup(x => x.GetPayloadAsync()).ReturnsAsync(givenQuestionnaireViewModel);
            
            //Act
            var result = await target.GetQuestionnaireAsync();

            //Assert
            Assert.AreSame(givenQuestionnaireViewModel, result);
            Assert.AreEqual(givenQuestionnaireViewModel.QuestionnaireTitle, result.QuestionnaireTitle);
        }
    }
}
