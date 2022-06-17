using System.ComponentModel;
using JIWGrandAlpha.Domain.Enum;

namespace JIWGrandAlpha.Domain.ViewModels.Class;

public class ClassViewModel
{

    public long Id { get; set; }
    [DisplayName("Тип")]
    public TypeClass Type { get; set; }
    public long IdGroup { get; set; }
    public long IdObject { get; set; }
    [DisplayName("Дата")]
    public string Date1 { get; set; }
}