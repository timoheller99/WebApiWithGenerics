namespace WebApiWithGenerics.WebApi.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using WebApiWithGenerics.WebApi.Contracts.Common;

    public abstract class CrudService<TRepository, TDbContract, TCreateRequest, TCreateResponse, TGetResponse, TUpdateRequest, TUpdateResponse, TDeleteResponse> : ICrudService<Guid, TCreateRequest, TCreateResponse, TGetResponse, TUpdateRequest, TUpdateResponse, TDeleteResponse>
        where TDbContract : IWithId
        where TRepository : ICrudRepository<Guid, TDbContract>
    {
        protected CrudService(IMapper mapper, TRepository repository)
        {
            this.Mapper = mapper;
            this.Repository = repository;
        }

        protected IMapper Mapper { get; }

        protected TRepository Repository { get; }

        public async Task<TCreateResponse> CreateAsync(TCreateRequest request)
        {
            var dbContract = this.Mapper.Map<TDbContract>(request);
            dbContract.Id = Guid.NewGuid();

            var createdEntity = await this.Repository.CreateAsync(dbContract);

            return this.Mapper.Map<TCreateResponse>(createdEntity);
        }

        public async Task<IEnumerable<TGetResponse>> GetAllAsync()
        {
            var dbContracts = await this.Repository.GetAllAsync();

            return this.Mapper.Map<IEnumerable<TGetResponse>>(dbContracts);
        }

        public async Task<TGetResponse> GetByIdAsync(Guid id)
        {
            var dbContract = await this.Repository.GetByIdAsync(id);

            return this.Mapper.Map<TGetResponse>(dbContract);
        }

        public async Task<TUpdateResponse> UpdateAsync(Guid id, TUpdateRequest request)
        {
            var dbContract = this.Mapper.Map<TDbContract>(request);
            dbContract.Id = id;

            var createdEntity = await this.Repository.UpdateAsync(dbContract);

            return this.Mapper.Map<TUpdateResponse>(createdEntity);
        }

        public async Task<TDeleteResponse> DeleteAsync(Guid id)
        {
            var dbContract = await this.Repository.DeleteAsync(id);

            return this.Mapper.Map<TDeleteResponse>(dbContract);
        }
    }
}
