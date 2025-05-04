using System.ComponentModel.DataAnnotations;

namespace ProductRating.Data.Models
{
    public class AuthorizationModel
    {
        [Required(ErrorMessage = "������� ����� ��������")]
        [StringLength(15, ErrorMessage = "����� �������� �� ����� ���� ������ 15 ��������")]
        [RegularExpression(@"^\d+$", ErrorMessage = "����� �������� ������ ��������� ������ �����")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "������� ������")]
        [StringLength(30, ErrorMessage = "������ �� ����� ���� ������ 30 ��������")]
        public string Password { get; set; }
    }
}