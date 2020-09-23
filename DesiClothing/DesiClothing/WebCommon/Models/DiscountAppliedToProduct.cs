using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothingUI
{
    public partial class DiscountAppliedToProduct
    {
        public int DiscountId { get; set; }
        public int ProductId { get; set; }

        public virtual Discount Discount { get; set; }
        public virtual Product Product { get; set; }
    }
}
