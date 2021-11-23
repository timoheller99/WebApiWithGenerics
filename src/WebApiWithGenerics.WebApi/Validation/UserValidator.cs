namespace WebApiWithGenerics.WebApi.Validation
{
    using FluentValidation;

    using WebApiWithGenerics.WebApi.Contracts.User;

    public class UserValidator : AbstractValidator<IUser>
    {
        public UserValidator()
        {
            this.RuleFor(m => m)
                .NotNull();

            this.RuleFor(m => m.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}
