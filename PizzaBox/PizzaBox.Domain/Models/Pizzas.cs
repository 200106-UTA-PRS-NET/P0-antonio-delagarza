using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class Pizzas
    {
        public Pizzas()
        {
            OrdersPizzaInfo = new HashSet<OrdersPizzaInfo>();
        }

        public int PizzaId { get; set; }
        public string Size { get; set; }
        public string Crust { get; set; }
        public string CrustFlavor { get; set; }
        public string Sauce { get; set; }
        public string SauceAmount { get; set; }
        public string CheeseAmount { get; set; }
        public string Topping1 { get; set; }
        public string Topping2 { get; set; }
        public string Topping3 { get; set; }
        public string Veggie1 { get; set; }
        public string Veggie2 { get; set; }
        public string Veggie3 { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<OrdersPizzaInfo> OrdersPizzaInfo { get; set; }
    }
}
