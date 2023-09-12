using Dinotrack.Shared.Responses;

namespace Dinotrack.Backend.Services
{
    public interface IApiService
    {
        Task<Response<T>> GetAsync<T>(string servicePrefix, string controller);
    }
}