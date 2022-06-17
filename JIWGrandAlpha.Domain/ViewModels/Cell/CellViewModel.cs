using System.ComponentModel;
using JIWGrandAlpha.Domain.Enum;

namespace JIWGrandAlpha.Domain.ViewModels.Cell;

public class CellViewModel
{
    public long Id { get; set; }
    [DisplayName("Значение")]
    public sbyte? Value { get; set; }
    [DisplayName("Тип")]
    public TypeValue Type { get; set; }
    public long IdStudent { get; set; }
    public long IdClass { get; set; }
}