using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class StoreInfo
    {
        public StoreInfo()
        {
            StoreOrdersInfo = new HashSet<StoreOrdersInfo>();
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public decimal StorePrice { get; set; }

        public virtual ICollection<StoreOrdersInfo> StoreOrdersInfo { get; set; }
    }
}
