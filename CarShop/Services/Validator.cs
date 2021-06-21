using System.Collections.Generic;
using System.Text.RegularExpressions;
using CarShop.Models.Cars;
using CarShop.Models.Issues;
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
        
        public ICollection<string> ValidateCarCreation(AddCarForModel model)
        {
            var errors = new List<string>();

            if (model.Model.Length < UserMinUsername
                || model.Model.Length > DefaultMaxLength)
            {
                errors.Add($"Model '{model.Model}' is not valid! It must be between {UserMinUsername} and {DefaultMaxLength}.");
            }

            if (model.Year < CarYearMinValue || model.Year>CarYearMaxValue)
            {
                errors.Add($"Year '{model.Year}' is not valid! It must be between {CarYearMinValue} and {CarYearMaxValue}.");
            }

            if (!Regex.IsMatch(model.PlateNumber, CarPlateNumberRegularExpression))
            {
                errors.Add($"Plate number '{model.PlateNumber}' is not valid! It should be in format 'AA0000AA'.");
            }

            return errors;
        }

        public ICollection<string> ValidateIssuesCreation(AddIssueForModel model)
        {
            var errors = new List<string>();

            if (model.Description.Length < DefaultMinLength)
            {
                errors.Add($"Description '{model.Description}' is not valid! Min Length must be {DefaultMinLength}.");
            }

            return errors;
        }
    }
}
