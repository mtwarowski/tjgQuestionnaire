using PairingTest.Web.Models;
using System.Threading.Tasks;

namespace PairingTest.Web.Services
{
    public interface IQuestionnaireService
    {
        Task<QuestionnaireViewModel> GetQuestionnaireAsync();
    }
}