using System;
using System.Collections.Generic;

namespace JIWGrandAlpha.Domain.Entity
{
    public partial class Group
    {
        public Group()
        {
            Classes = new HashSet<Class>();
            GroupsObjects = new HashSet<GroupObject>();
            Students = new HashSet<Student>();
        }

        public long Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<GroupObject> GroupsObjects { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
