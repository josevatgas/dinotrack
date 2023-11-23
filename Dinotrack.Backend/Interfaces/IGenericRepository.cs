using Dinotrack.Shared.Entities;
using Dinotrack.Shared.Responses;

namespace Dinotrack.Backend.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAsync();

        Task<Response<T>> AddAsync(T entity);

        Task DeleteAsync(int id);

        Task<Response<T>> UpdateAsync(T entity);

        Task<Country> GetCountryAsync(int id);

        Task<State> GetStateAsync(int id);

    }
}