using System;
using System.Collections.Generic;
using JIWGrandAlpha.Domain.Enum;

namespace JIWGrandAlpha.Domain.Entity
{
    public partial class Cell
    {
        public long Id { get; set; }
        public sbyte? Value { get; set; }
        public TypeValue Type { get; set; }
        public long IdStudent { get; set; }
        public long IdClass { get; set; }

        public virtual Class Class { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
