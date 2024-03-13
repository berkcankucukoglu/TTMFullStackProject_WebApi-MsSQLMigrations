using TTM.Domain;

namespace TTM.Business.Validators
{
    internal class ProjectValidator
    {
        public ValidationResult Validate(Project project)
        {
            var validationResult = new ValidationResult();
            if (string.IsNullOrWhiteSpace(project.Name))
            {
                validationResult.AddError("Project name cannot be empty!");
            }
            if (project.CategoryId == null)
            {
                validationResult.AddError("Project category cannot be empty!");
            }
            return validationResult;
        }
    }
}
