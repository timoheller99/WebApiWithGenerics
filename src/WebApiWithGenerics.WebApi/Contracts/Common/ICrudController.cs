namespace WebApiWithGenerics.WebApi.Contracts.Common
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public interface ICrudController<TIdType, TCreateRequest, TCreateResponse, TGetResponse, TUpdateRequest, TUpdateResponse, TDeleteResponse>
    {
        Task<ActionResult<TCreateResponse>> CreateAsync(TCreateRequest request);

        Task<ActionResult<IEnumerable<TGetResponse>>> GetAllAsync();

        Task<ActionResult<TGetResponse>> GetByIdAsync(TIdType id);

        Task<ActionResult<TUpdateResponse>> UpdateAsync(TIdType id, TUpdateRequest request);

        Task<ActionResult<TDeleteResponse>> DeleteAsync(TIdType id);
    }
}
