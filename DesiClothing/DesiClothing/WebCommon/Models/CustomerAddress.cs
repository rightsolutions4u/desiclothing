using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothingUI
{
    public partial class CustomerAddress
    {
        public int AddressId { get; set; }
        public int CustomerId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
