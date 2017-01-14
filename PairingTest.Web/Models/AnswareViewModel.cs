using System.ComponentModel.DataAnnotations;

namespace PairingTest.Web.Models
{
    public class AnswareViewModel
    {
        [Required]
        public string QuestionText { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide an answer.")]
        public string AnswereText { get; set; }
    }
}