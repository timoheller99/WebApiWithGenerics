namespace WebApiWithGenerics.WebApi.Validation
{
    using System;

    using FluentValidation;

    using WebApiWithGenerics.WebApi.Contracts.Todo;

    public class TodoValidator : AbstractValidator<ITodo>
    {
        public TodoValidator()
        {
            this.RuleFor(m => m)
                .NotNull();

            this.RuleFor(m => m.Name)
                .NotNull()
                .NotEmpty();

            this.RuleFor(m => m.TodoListId)
                .NotNull()
                .NotEmpty();

            this.RuleFor(m => m.Status)
                .NotNull()
                .NotEmpty();
        }
    }
}
