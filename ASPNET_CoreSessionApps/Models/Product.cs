using System;
using System.Collections.Generic;

namespace ASPNET_CoreSessionApps.Models
{
    public partial class Product
    {
        public Product()
        {
            BillDetails = new HashSet<BillDetails>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<BillDetails> BillDetails { get; set; }
    }
}
