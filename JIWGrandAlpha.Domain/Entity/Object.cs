using System;
using System.Collections.Generic;

namespace JIWGrandAlpha.Domain.Entity
{
    public partial class Object
    {
        public Object()
        {
            Classes = new HashSet<Class>();
            GroupsObjects = new HashSet<GroupObject>();
            TeacherObjects = new HashSet<TeacherObject>();
        }

        public long Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<GroupObject> GroupsObjects { get; set; }
        public virtual ICollection<TeacherObject> TeacherObjects { get; set; }
    }
}
