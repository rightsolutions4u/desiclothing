﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DesiClothingUI
{
    public partial class GiftCardUsageHistory
    {
        public int Id { get; set; }
        public int GiftCardId { get; set; }
        public int UsedWithOrderId { get; set; }
        public decimal UsedValue { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public virtual GiftCard GiftCard { get; set; }
        public virtual Order UsedWithOrder { get; set; }
    }
}
