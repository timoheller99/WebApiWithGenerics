namespace WebApiWithGenerics.WebApi.Validation
{
    using System.Threading.Tasks;

    using FluentValidation;
    using FluentValidation.Results;

    public class ValidationService<TValidator, TValidationModel>
        where TValidator : AbstractValidator<TValidationModel>
    {
        private readonly TValidator validator;

        public ValidationService(TValidator validator)
        {
            this.validator = validator;
        }

        public async Task<ValidationResult> ValidateAsync(TValidationModel model)
        {
            return await this.validator.ValidateAsync(model);
        }
    }
}
