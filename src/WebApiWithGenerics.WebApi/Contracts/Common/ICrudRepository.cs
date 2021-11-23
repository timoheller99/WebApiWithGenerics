namespace WebApiWithGenerics.WebApi.Contracts.Common
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICrudRepository<TIdType, TDbContract>
    {
        Task<TDbContract> CreateAsync(TDbContract entity);

        Task<IEnumerable<TDbContract>> GetAllAsync();

        Task<TDbContract> GetByIdAsync(TIdType id);

        Task<TDbContract> UpdateAsync(TDbContract entity);

        Task<TDbContract> DeleteAsync(TIdType id);
    }
}
