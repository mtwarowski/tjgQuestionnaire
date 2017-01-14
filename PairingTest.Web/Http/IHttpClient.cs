using System.Threading.Tasks;

namespace PairingTest.Web.Http
{
    public interface IHttpClient
    {
        Task<IApiResponse<TResponse>> GetAsync<TResponse>(string url) where TResponse : class;
    }
}