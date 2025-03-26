using System.ComponentModel.DataAnnotations;

namespace ProductRating.WebApplication.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Введите номер телефона")]
        [StringLength(30, ErrorMessage = "Номер телефона не может быть длинее 30 символов")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Номер телефона должен содержать только цифры")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите имя пользователя")]
        [StringLength(30, ErrorMessage = "Имя пользователя не может быть длинее 30 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(30, ErrorMessage = "Пароль не может быть длинее 30 символов")]
        public string Password { get; set; }
    }
}