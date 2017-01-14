using System.Web.Mvc;
using PairingTest.Web.Models;
using System.Threading.Tasks;
using System.Linq;
using PairingTest.Web.Services;

namespace PairingTest.Web.Controllers
{
    public class QuestionnaireController : Controller
    {
        IQuestionnaireService questionnaireService;
        public QuestionnaireController(IQuestionnaireService questionnaireService)
        {
            this.questionnaireService = questionnaireService;
        }

        public async Task<ViewResult> Index()
        {
            var questionnaireViewModel = await questionnaireService.GetQuestionnaireAsync();

            if (questionnaireViewModel == null)
                return View("Error");

            return View(MapToVM(questionnaireViewModel));
        }
        

        [HttpPost]
        public ViewResult Questionnaire(QuestionnaireWithAnswerViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Index", viewModel);

            return View("Success");
        }

        public QuestionnaireWithAnswerViewModel MapToVM(QuestionnaireViewModel questionnaire)
        {
            return new QuestionnaireWithAnswerViewModel
            {
                QuestionnaireTitle = questionnaire.QuestionnaireTitle,
                QuestionAnswers = questionnaire.QuestionsText.Select(q => new AnswareViewModel { QuestionText = q }).ToList()
            };
        }
    }
}
