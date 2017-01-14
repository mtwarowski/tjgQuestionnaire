using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PairingTest.Web.Models
{
    public class QuestionnaireViewModel
    {
        public string QuestionnaireTitle { get; set; }
        public List<string> QuestionsText { get; set; }

        public QuestionnaireViewModel()
        {
            QuestionsText = new List<string>();
        }
    }
}