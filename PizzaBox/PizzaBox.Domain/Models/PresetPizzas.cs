using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class PresetPizzas
    {
        public PresetPizzas()
        {
            StorePresetPizzas = new HashSet<StorePresetPizzas>();
        }

        public string PizzaName { get; set; }
        public string Size { get; set; }
        public string Crust { get; set; }
        public string CrustFlavor { get; set; }
        public string Sauce { get; set; }
        public string SauceAmount { get; set; }
        public string CheeseAmount { get; set; }
        public string Topping1 { get; set; }
        public string Topping2 { get; set; }
        public string Topping3 { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<StorePresetPizzas> StorePresetPizzas { get; set; }
    }
}
