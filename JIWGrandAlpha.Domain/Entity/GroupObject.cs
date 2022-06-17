using System;
using System.Collections.Generic;

namespace JIWGrandAlpha.Domain.Entity
{
    public partial class GroupObject
    {
        public long Id { get; set; }
        public long IdGroup { get; set; }
        public long IdObject { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual Object Object { get; set; } = null!;
    }
}
