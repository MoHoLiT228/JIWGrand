using System;
using System.Collections.Generic;

namespace JIWGrandAlpha.Domain.Entity
{
    public partial class Student
    {
        public Student()
        {
            Cells = new HashSet<Cell>();
        }

        public long Id { get; set; }
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Middlename { get; set; } = null!;
        public DateOnly Birthday { get; set; }
        public long IdGroup { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual ICollection<Cell> Cells { get; set; }
    }
}
