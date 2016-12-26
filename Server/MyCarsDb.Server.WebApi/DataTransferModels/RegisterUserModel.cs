namespace MyCarsDb.Server.WebApi.DataTransferModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserModel
    {
        [RegularExpression(@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z]{2,})+$", ErrorMessage = "Invalid email.")]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}