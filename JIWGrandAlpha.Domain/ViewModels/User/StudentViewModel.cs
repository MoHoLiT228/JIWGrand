using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace JIWGrandAlpha.Domain.ViewModels.User;

public class StudentViewModel
{
    [Required]
    public string Lastname { get; set; } = null!;
    public long IdGroup { get; set; }
    [Required(ErrorMessage = "Введите дату рождения!")]
    public string Birthday { get; set; }
}