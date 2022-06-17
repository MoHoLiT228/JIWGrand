using System;
using System.Collections.Generic;
using JIWGrandAlpha.Domain.Enum;

namespace JIWGrandAlpha.Domain.Entity
{
    public partial class Class
    {

        public long Id { get; set; }
        public TypeClass Type { get; set; }
        public long IdGroup { get; set; }
        public long IdObject { get; set; }
        public DateOnly Date1 { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual Object Object { get; set; } = null!;
        public virtual Cell Cell { get; set; }
    }
}
