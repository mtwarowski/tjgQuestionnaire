using System;
using System.Threading.Tasks;
using PairingTest.Web.Models;
using PairingTest.Web.Helpers;
using PairingTest.Web.Http;

namespace PairingTest.Web.Services
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly IAppConfiguration configuration;
        private readonly IHttpClient httpClient;
        public QuestionnaireService(IAppConfiguration configuration, IHttpClient httpClient)
        {
            this.configuration = configuration;
            this.httpClient = httpClient;
        }

        public async Task<QuestionnaireViewModel> GetQuestionnaireAsync()
        {
            var questionnaireUrl = configuration.QuestionnaireUrl;
            var httpCallResult = await httpClient.GetAsync<QuestionnaireViewModel>(questionnaireUrl);

            if (!httpCallResult.IsSuccessful)
                return null;

            return await httpCallResult.GetPayloadAsync();
        }
    }
}