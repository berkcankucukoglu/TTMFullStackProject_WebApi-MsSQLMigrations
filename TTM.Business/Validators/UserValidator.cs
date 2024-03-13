using TTM.Domain;

namespace TTM.Business.Validators
{
    internal class UserValidator
    {
        public ValidationResult Validate(User user)
        {
            var validationResult = new ValidationResult();
            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                validationResult.AddError("First Name cannot be empty!");
            }
            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                validationResult.AddError("Last Name cannot be empty!");
            }
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                validationResult.AddError("Email cannot be empty!");
            }
            else if (!user.Email.Contains('@') || !user.Email.Contains('.') || user.Email.Contains(' '))
            {
                validationResult.AddError("Wrong email informatinon!");
            }
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                validationResult.AddError("Password cannot be empty!");
            }
            return validationResult;
        }
    }
}
