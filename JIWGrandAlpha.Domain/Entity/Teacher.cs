using System;
using System.Collections.Generic;

namespace JIWGrandAlpha.Domain.Entity
{
    public partial class Teacher
    {
        public Teacher()
        {
            TeacherObjects = new HashSet<TeacherObject>();
        }

        public long Id { get; set; }
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Middlename { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<TeacherObject> TeacherObjects { get; set; }
    }
}
