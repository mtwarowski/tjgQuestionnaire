using System.Net;
using System.Threading.Tasks;

namespace PairingTest.Web.Http
{
    public interface IApiResponse<T> where T : class
    {
        Task<T> GetPayloadAsync();
        HttpStatusCode Status { get; }
        bool IsSuccessful { get; }
    }
}