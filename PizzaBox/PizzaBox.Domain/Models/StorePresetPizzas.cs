using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class StorePresetPizzas
    {
        public int StoreId { get; set; }
        public string PizzaName { get; set; }

        public virtual PresetPizzas PizzaNameNavigation { get; set; }
        public virtual StoreInfo Store { get; set; }
    }
}
