namespace WebApiWithGenerics.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Net.Mime;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using WebApiWithGenerics.WebApi.Contracts.User;
    using WebApiWithGenerics.WebApi.CustomExceptions;
    using WebApiWithGenerics.WebApi.Validation;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase, IUserController
    {
        private readonly ILogger<UserController> logger;

        private readonly IUserService service;

        private readonly ValidationService<UserValidator, IUser> validationService;

        public UserController(ILogger<UserController> logger, IUserService service, ValidationService<UserValidator, IUser> validationService)
        {
            this.logger = logger;
            this.service = service;
            this.validationService = validationService;
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserCreateResponse), StatusCodes.Status200OK)]
        [Route("")]
        public async Task<ActionResult<UserCreateResponse>> CreateAsync(UserCreateRequest request)
        {
            try
            {
                var validationResult = await this.validationService.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    this.logger.LogError("Request model is not valid: {@Errors}", validationResult.Errors);
                    return this.BadRequest(validationResult.Errors);
                }

                var result = await this.service.CreateAsync(request);

                return this.Ok(result);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "Could not create '{EntityName}': '{Message}'", UserDbContract.GetEntityName(), e.Message);

                return this.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<UserGetResponse>), StatusCodes.Status200OK)]
        [Route("")]
        public async Task<ActionResult<IEnumerable<UserGetResponse>>> GetAllAsync()
        {
            try
            {
                var result = await this.service.GetAllAsync();

                return this.Ok(result);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "Could not get all '{EntityName}' entries: {Message}", UserDbContract.GetEntityName(), e.Message);

                return this.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserGetResponse), StatusCodes.Status200OK)]
        [Route("id/{id}")]
        public async Task<ActionResult<UserGetResponse>> GetByIdAsync([Required] Guid id)
        {
            try
            {
                var result = await this.service.GetByIdAsync(id);

                return this.Ok(result);
            }
            catch (EntityNotFoundException<UserDbContract> e)
            {
                this.logger.LogError(
                    e,
                    "Could not get '{EntityName}' with ID: '{Id}': {Message}",
                    UserDbContract.GetEntityName(),
                    id,
                    e.Message);

                return this.NotFound($"Could not find '{UserDbContract.GetEntityName()}' with ID: '{id}'");
            }
            catch (Exception e)
            {
                this.logger.LogError(
                    e,
                    "Could not get '{EntityName}' with ID: '{Id}': {Message}",
                    UserDbContract.GetEntityName(),
                    id,
                    e.Message);

                return this.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserGetResponse), StatusCodes.Status200OK)]
        [Route("email/{email}")]
        public async Task<ActionResult<UserGetResponse>> GetByEmailAsync([Required] string email)
        {
            try
            {
                var result = await this.service.GetByEmailAsync(email);

                return this.Ok(result);
            }
            catch (EntityNotFoundException<UserDbContract> e)
            {
                this.logger.LogError(
                    e,
                    "Could not get '{EntityName}' with email: '{Id}': {Message}",
                    UserDbContract.GetEntityName(),
                    email,
                    e.Message);

                return this.NotFound($"Could not find '{UserDbContract.GetEntityName()}' with ID: '{email}'");
            }
            catch (Exception e)
            {
                this.logger.LogError(
                    e,
                    "Could not get '{EntityName}' with email: '{Id}': {Message}",
                    UserDbContract.GetEntityName(),
                    email,
                    e.Message);

                return this.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserUpdateResponse), StatusCodes.Status200OK)]
        [Route("id/{id}")]
        public async Task<ActionResult<UserUpdateResponse>> UpdateAsync([Required] Guid id, UserUpdateRequest request)
        {
            try
            {
                var validationResult = await this.validationService.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    this.logger.LogError("Request model is not valid: {@Errors}", validationResult.Errors);
                    return this.BadRequest(validationResult.Errors);
                }

                var result = await this.service.UpdateAsync(id, request);

                return this.Ok(result);
            }
            catch (EntityNotFoundException<UserDbContract> e)
            {
                this.logger.LogError(
                    e,
                    "Could not update '{EntityName}' with ID: '{Id}': {Message}",
                    UserDbContract.GetEntityName(),
                    id,
                    e.Message);

                return this.NotFound();
            }
            catch (Exception e)
            {
                this.logger.LogError(
                    e,
                    "Could not update '{EntityName}' with ID: '{Id}': {Message}",
                    UserDbContract.GetEntityName(),
                    id,
                    e.Message);

                return this.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserDeleteResponse), StatusCodes.Status200OK)]
        [Route("id/{id}")]
        public async Task<ActionResult<UserDeleteResponse>> DeleteAsync([Required] Guid id)
        {
            try
            {
                var result = await this.service.DeleteAsync(id);

                return this.Ok(result);
            }
            catch (EntityNotFoundException<UserDbContract> e)
            {
                this.logger.LogError(
                    e,
                    "Could not delete '{EntityName}' with ID: '{Id}': {Message}",
                    UserDbContract.GetEntityName(),
                    id,
                    e.Message);

                return this.NotFound();
            }
            catch (Exception e)
            {
                this.logger.LogError(
                    e,
                    "Could not delete '{EntityName}' with ID: '{Id}': {Message}",
                    UserDbContract.GetEntityName(),
                    id,
                    e.Message);

                return this.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
