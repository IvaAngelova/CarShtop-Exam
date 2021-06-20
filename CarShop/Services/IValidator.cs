using System.Collections.Generic;

using CarShop.Models.Cars;
using CarShop.Models.Users;

namespace CarShop.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserFormModel model);

        ICollection<string> ValidateCarCreation(AddCarForModel model);
    }
}
