using Moq;
using NUnit.Framework;
using PairingTest.Web.Controllers;
using PairingTest.Web.Models;
using PairingTest.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PairingTest.Unit.Tests.Web
{
    [TestFixture]
    public class QuestionnaireControllerTests
    {
        private Mock<IQuestionnaireService> mockQuestionnaireService;
        private QuestionnaireController questionnaireController;

        [TestFixtureSetUp]
        public void Init()
        {
            mockQuestionnaireService = new Mock<IQuestionnaireService>();
            questionnaireController = new QuestionnaireController(mockQuestionnaireService.Object);
        }

        [Test]
        public async Task Index_CallesServiceToGetQuestionnaire()
        {
            //Arrange
            mockQuestionnaireService.Setup(arg => arg.GetQuestionnaireAsync())
                .Returns(Task.FromResult(new QuestionnaireViewModel()));

            //Act
            await questionnaireController.Index();

            //Assert
            mockQuestionnaireService.Verify(m => m.GetQuestionnaireAsync());
        }

        [Test]
        public async Task Index_WithNoQuestionnaire_RedirectsToErrorPage()
        {
            //Arrange
            mockQuestionnaireService.Setup(arg => arg.GetQuestionnaireAsync())
                .Returns(Task.FromResult<QuestionnaireViewModel>(null));

            //Act
            var result = (await questionnaireController.Index()).ViewName;

            //Assert
            Assert.AreEqual("Error", result);
        }

        [Test]
        public async Task Index_GetQuestionnaireFromServiceWithCorrectTitle()
        {
            //Arrange
            const string expectedTitle = "My expected quesitons";
            mockQuestionnaireService.Setup(arg => arg.GetQuestionnaireAsync())
                .Returns(Task.FromResult(new QuestionnaireViewModel { QuestionnaireTitle = expectedTitle }));

            //Act
            var result = (QuestionnaireWithAnswerViewModel)(await questionnaireController.Index()).ViewData.Model;
            
            //Assert
            Assert.That(result.QuestionnaireTitle, Is.EqualTo(expectedTitle));
        }

        [Test]
        public async Task Index_GetQuestionnaireFromServiceWithCorrectQuestions()
        {
            //Arrange
            var expectedQuestions = new List<string> { "Question One", "Question Two", "Question Three" };
            mockQuestionnaireService.Setup(arg => arg.GetQuestionnaireAsync())
                .Returns(Task.FromResult(new QuestionnaireViewModel { QuestionsText = expectedQuestions }));
            
            //Act
            var result = (QuestionnaireWithAnswerViewModel)(await questionnaireController.Index()).ViewData.Model;

            //Assert
            Assert.AreEqual(3, result.QuestionAnswers.Count);
            Assert.AreEqual(expectedQuestions[0], result.QuestionAnswers[0].QuestionText);
            Assert.AreEqual(expectedQuestions[1], result.QuestionAnswers[1].QuestionText);
            Assert.AreEqual(expectedQuestions[2], result.QuestionAnswers[2].QuestionText);
        }

        [Test]
        public void Questionnaire_WithValidModel_ShowsSuccessView()
        {
            //Arrange
            var givenViewModel = new QuestionnaireWithAnswerViewModel { QuestionnaireTitle = "Given Title" };
            
            //Act
            var result = questionnaireController.Questionnaire(givenViewModel);
            var resultViewModel = (QuestionnaireWithAnswerViewModel)result.ViewData.Model;
            
            //Assert
            Assert.AreEqual("Success", result.ViewName);
        }

        [Test]
        public void Questionnaire_WithInvalidModel_ShowsIndexViewWithInvalidModel()
        {
            //Arrange
            var givenViewModel = new QuestionnaireWithAnswerViewModel { QuestionnaireTitle = "Given Title" };
            var target = new QuestionnaireController(mockQuestionnaireService.Object);
            target.ModelState.AddModelError("fakeErrorKey", "Fake Error message");

            //Act
            var result = target.Questionnaire(givenViewModel);
            var resultViewModel = (QuestionnaireWithAnswerViewModel)result.ViewData.Model;

            //Assert
            Assert.AreEqual("Index", result.ViewName);
            Assert.AreSame(givenViewModel, resultViewModel);
            Assert.AreEqual(givenViewModel.QuestionnaireTitle, resultViewModel.QuestionnaireTitle);
        }
    }
}