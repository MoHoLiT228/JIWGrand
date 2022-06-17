using System.ComponentModel.DataAnnotations;

namespace JIWGrandAlpha.Domain.ViewModels.User;

public class TeacherViewModel
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Введите пароль!")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}