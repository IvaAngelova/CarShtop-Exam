using System.Collections.Generic;

using CarShop.Models.Users;

using static CarShop.Data.DataConstants;

namespace CarShop.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateUserRegistration(RegisterUserFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < UserMinUsername
                || model.Username.Length > DefaultMaxLength )
            {
                errors.Add($"Username '{model.Username}' is not valid! It must be between {UserMinUsername} and {DefaultMaxLength}.");
            }

            if (model.Password.Length < DefaultMinLength
                || model.Username.Length > DefaultMaxLength)
            {
                errors.Add($"The provided password is not valid! It must be between {DefaultMinLength} and {DefaultMaxLength}.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Parssword and its confirmation are different.");
            }

            if (model.UserType != UserTypeMechanic 
                && model.UserType != UserTypeClient)
            {
                errors.Add($"User should be either a '{UserTypeMechanic}' or '{UserTypeClient}'.");
            }

            return errors;
        }
    }
}
