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

    using WebApiWithGenerics.WebApi.Contracts.Todo;
    using WebApiWithGenerics.WebApi.CustomExceptions;
    using WebApiWithGenerics.WebApi.Validation;

    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase, ITodoController
    {
        private readonly ILogger<TodoController> logger;

        private readonly ITodoService service;

        private readonly ValidationService<TodoValidator, ITodo> validationService;

        public TodoController(ILogger<TodoController> logger, ITodoService service, ValidationService<TodoValidator, ITodo> validationService)
        {
            this.logger = logger;
            this.service = service;
            this.validationService = validationService;
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(TodoCreateResponse), StatusCodes.Status200OK)]
        [Route("")]
        public async Task<ActionResult<TodoCreateResponse>> CreateAsync(TodoCreateRequest request)
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
                this.logger.LogError(e, "Could not create '{EntityName}': '{Message}'", TodoDbContract.GetEntityName(), e.Message);

                return this.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<TodoGetResponse>), StatusCodes.Status200OK)]
        [Route("")]
        public async Task<ActionResult<IEnumerable<TodoGetResponse>>> GetAllAsync()
        {
            try
            {
                var result = await this.service.GetAllAsync();

                return this.Ok(result);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "Could not get all '{EntityName}' entries: {Message}", TodoDbContract.GetEntityName(), e.Message);

                return this.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(TodoGetResponse), StatusCodes.Status200OK)]
        [Route("id/{id}")]
        public async Task<ActionResult<TodoGetResponse>> GetByIdAsync([Required] Guid id)
        {
            try
            {
                var result = await this.service.GetByIdAsync(id);

                return this.Ok(result);
            }
            catch (EntityNotFoundException<TodoDbContract> e)
            {
                this.logger.LogError(
                    e,
                    "Could not get '{EntityName}' with ID: '{Id}': {Message}",
                    TodoDbContract.GetEntityName(),
                    id,
                    e.Message);

                return this.NotFound($"Could not find '{TodoDbContract.GetEntityName()}' with ID: '{id}'");
            }
            catch (Exception e)
            {
                this.logger.LogError(
                    e,
                    "Could not get '{EntityName}' with ID: '{Id}': {Message}",
                    TodoDbContract.GetEntityName(),
                    id,
                    e.Message);

                return this.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(TodoGetResponse), StatusCodes.Status200OK)]
        [Route("todoListId/{todoListId}")]
        public async Task<ActionResult<TodoGetResponse>> GetByTodoListIdAsync([Required] string todoListId)
        {
            try
            {
                var result = await this.service.GetByTodoListIdAsync(todoListId);

                return this.Ok(result);
            }
            catch (EntityNotFoundException<TodoDbContract> e)
            {
                this.logger.LogError(
                    e,
                    "Could not get '{EntityName}' with '{AttributeName}': '{Id}': {Message}",
                    TodoDbContract.GetEntityName(),
                    nameof(TodoDbContract.TodoListId),
                    todoListId,
                    e.Message);

                return this.NotFound($"Could not find '{TodoDbContract.GetEntityName()}' with '{nameof(TodoDbContract.TodoListId)}': '{todoListId}'");
            }
            catch (Exception e)
            {
                this.logger.LogError(
                    e,
                    "Could not get '{EntityName}' with '{AttributeName}': '{Id}': {Message}",
                    TodoDbContract.GetEntityName(),
                    nameof(TodoDbContract.TodoListId),
                    todoListId,
                    e.Message);

                return this.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(TodoUpdateResponse), StatusCodes.Status200OK)]
        [Route("id/{id}")]
        public async Task<ActionResult<TodoUpdateResponse>> UpdateAsync([Required] Guid id, TodoUpdateRequest request)
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
            catch (EntityNotFoundException<TodoDbContract> e)
            {
                this.logger.LogError(
                    e,
                    "Could not update '{EntityName}' with ID: '{Id}': {Message}",
                    TodoDbContract.GetEntityName(),
                    id,
                    e.Message);

                return this.NotFound();
            }
            catch (Exception e)
            {
                this.logger.LogError(
                    e,
                    "Could not update '{EntityName}' with ID: '{Id}': {Message}",
                    TodoDbContract.GetEntityName(),
                    id,
                    e.Message);

                return this.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(TodoDeleteResponse), StatusCodes.Status200OK)]
        [Route("id/{id}")]
        public async Task<ActionResult<TodoDeleteResponse>> DeleteAsync([Required] Guid id)
        {
            try
            {
                var result = await this.service.DeleteAsync(id);

                return this.Ok(result);
            }
            catch (EntityNotFoundException<TodoDbContract> e)
            {
                this.logger.LogError(
                    e,
                    "Could not delete '{EntityName}' with ID: '{Id}': {Message}",
                    TodoDbContract.GetEntityName(),
                    id,
                    e.Message);

                return this.NotFound();
            }
            catch (Exception e)
            {
                this.logger.LogError(
                    e,
                    "Could not delete '{EntityName}' with ID: '{Id}': {Message}",
                    TodoDbContract.GetEntityName(),
                    id,
                    e.Message);

                return this.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
