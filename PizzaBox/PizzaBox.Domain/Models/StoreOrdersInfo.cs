using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class StoreOrdersInfo
    {
        public int StoreId { get; set; }
        public int OrderId { get; set; }

        public virtual OrdersUserInfo Order { get; set; }
        public virtual StoreInfo Store { get; set; }
    }
}
