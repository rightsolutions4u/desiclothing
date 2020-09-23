using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothingUI
{
    public partial class ProductTag
    {
        public ProductTag()
        {
            ProductProductTagMappings = new HashSet<ProductProductTagMapping>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductProductTagMapping> ProductProductTagMappings { get; set; }
    }
}
