using System.ComponentModel.DataAnnotations;

namespace ProductRating.Data.Models
{
    public class AuthorizationModel
    {
        [Required(ErrorMessage = "Введите номер телефона")]
        [StringLength(15, ErrorMessage = "Номер телефона не может быть длинее 15 символов")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Номер телефона должен содержать только цифры")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(30, ErrorMessage = "Пароль не может быть длинее 30 символов")]
        public string Password { get; set; }
    }
}