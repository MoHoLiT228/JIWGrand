using JIWGrandAlpha.Domain.Entity;
using Object = JIWGrandAlpha.Domain.Entity.Object;

namespace JIWGrandAlpha.Domain.ViewModels.Journal;

public class JStudentViewModel
{
    public IEnumerable<DateOnly> Dates { get; set; }
    public IEnumerable<Object> Objects { get; set; }
}