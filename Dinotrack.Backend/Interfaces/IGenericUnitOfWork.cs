using Dinotrack.Shared.Entities;
using Dinotrack.Shared.Responses;

namespace Dinotrack.Backend.Interfaces
{
    public interface IGenericUnitOfWork<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();

        Task<Response<T>> AddAsync(T entity);

        Task<Response<T>> UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task<T> GetAsync(int id);

        Task<Country> GetCountryAsync(int id);

        Task<State> GetStateAsync(int id);

    }
}