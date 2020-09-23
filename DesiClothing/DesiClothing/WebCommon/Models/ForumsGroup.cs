using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothingUI
{
    public partial class ForumsGroup
    {
        public ForumsGroup()
        {
            ForumsForums = new HashSet<ForumsForum>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }

        public virtual ICollection<ForumsForum> ForumsForums { get; set; }
    }
}
