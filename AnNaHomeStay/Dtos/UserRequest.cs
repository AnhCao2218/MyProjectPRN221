using System.ComponentModel.DataAnnotations;

namespace AnNaHomeStay.Dtos
{
    public class UserRequest
    {
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }
    }
}
