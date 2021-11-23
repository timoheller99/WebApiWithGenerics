namespace WebApiWithGenerics.WebApi.Contracts.Common
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICrudService<TIdType, TCreateRequest, TCreateResponse, TGetResponse, TUpdateRequest, TUpdateResponse, TDeleteResponse>
    {
        Task<TCreateResponse> CreateAsync(TCreateRequest request);

        Task<IEnumerable<TGetResponse>> GetAllAsync();

        Task<TGetResponse> GetByIdAsync(TIdType id);

        Task<TUpdateResponse> UpdateAsync(TIdType id, TUpdateRequest request);

        Task<TDeleteResponse> DeleteAsync(TIdType id);
    }
}
