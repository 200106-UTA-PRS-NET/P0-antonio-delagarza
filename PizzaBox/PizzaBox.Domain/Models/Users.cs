using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class Users
    {
        public Users()
        {
            OrdersUserInfo = new HashSet<OrdersUserInfo>();
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<OrdersUserInfo> OrdersUserInfo { get; set; }
    }
}
