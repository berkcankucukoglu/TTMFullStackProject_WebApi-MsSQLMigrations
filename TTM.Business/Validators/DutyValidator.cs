using TTM.Domain;

namespace TTM.Business.Validators
{
    internal class DutyValidator
    {
        public ValidationResult Validate(Duty duty)
        {
            var validationResult = new ValidationResult();
            if (string.IsNullOrWhiteSpace(duty.Name))
            {
                validationResult.AddError("Duty name cannot be empty!");
            }
            if (duty.ProjectId == null )
            {
                validationResult.AddError("It is not possible to accept duty without a project!");
            }
            return validationResult;
        }
    }
}
