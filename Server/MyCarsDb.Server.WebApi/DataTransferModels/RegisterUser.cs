﻿namespace MyCarsDb.Server.WebApi.DataTransferModels
{
    public class RegisterUser
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}