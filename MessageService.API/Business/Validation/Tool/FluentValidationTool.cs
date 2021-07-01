using FluentValidation;

namespace MessageService.API.Business.Validation
{
    public static class FluentValidationTool
    {
        public static void Validate<T>(IValidator<T> validator, T entity)
        {
            var result = validator.Validate(entity);
            if (!result.IsValid)
            {
                throw new ValidationException("Validation failed", result.Errors);
            }
        }
    }
}