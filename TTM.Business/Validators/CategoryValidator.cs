using TTM.Domain;

namespace TTM.Business.Validators
{
    internal class CategoryValidator
    {
        public ValidationResult Validate(Category category)
        {
            var validationResult = new ValidationResult();
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                validationResult.AddError("Category name cannot be empty!");
            }
            return validationResult;
        }
    }
}
