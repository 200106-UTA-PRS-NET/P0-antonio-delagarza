using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class OrdersUserInfo
    {
        public OrdersUserInfo()
        {
            OrdersPizzaInfo = new HashSet<OrdersPizzaInfo>();
            StoreOrdersInfo = new HashSet<StoreOrdersInfo>();
        }

        public int OrderId { get; set; }
        public string Email { get; set; }
        public DateTime OrderDateTime { get; set; }

        public virtual Users EmailNavigation { get; set; }
        public virtual ICollection<OrdersPizzaInfo> OrdersPizzaInfo { get; set; }
        public virtual ICollection<StoreOrdersInfo> StoreOrdersInfo { get; set; }
    }
}
