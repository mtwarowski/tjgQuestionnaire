using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PairingTest.Web.Models
{

    public class QuestionnaireWithAnswerViewModel
    {
        [Required]
        public string QuestionnaireTitle { get; set; }
        public List<AnswareViewModel> QuestionAnswers { get; set; }

        public QuestionnaireWithAnswerViewModel()
        {
            QuestionAnswers = new List<AnswareViewModel>();
        }
    }
}