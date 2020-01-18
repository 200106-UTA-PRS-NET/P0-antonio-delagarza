using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class OrdersPizzaInfo
    {
        public int OrderId { get; set; }
        public int PizzaId { get; set; }
        public decimal Price { get; set; }

        public virtual OrdersUserInfo Order { get; set; }
        public virtual Pizzas OrderNavigation { get; set; }
    }
}
