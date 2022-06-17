using System;
using System.Collections.Generic;

namespace JIWGrandAlpha.Domain.Entity
{
    public partial class TeacherObject
    {
        public long Id { get; set; }
        public long IdTeacher { get; set; }
        public long IdObject { get; set; }

        public virtual Object Object { get; set; } = null!;
        public virtual Teacher Teacher { get; set; } = null!;
    }
}
