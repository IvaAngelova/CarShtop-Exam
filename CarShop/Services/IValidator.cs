﻿using System.Collections.Generic;

using CarShop.Models.Users;

namespace CarShop.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserFormModel model);
    }
}