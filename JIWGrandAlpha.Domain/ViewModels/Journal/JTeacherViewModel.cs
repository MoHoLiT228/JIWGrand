using JIWGrandAlpha.Domain.Entity;
using Object = JIWGrandAlpha.Domain.Entity.Object;

namespace JIWGrandAlpha.Domain.ViewModels.Journal;

public class JTeacherViewModel
{
    public IEnumerable<Entity.Class> Classes { get; set; }
    public IEnumerable<Student> Students { get; set; }
    public IEnumerable<Group> Groups { get; set; }
    public IEnumerable<Object> Objects { get; set; }
    public long idGroup { get; set; }
    public long idObject { get; set; }
}