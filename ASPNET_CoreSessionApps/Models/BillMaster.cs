using System;
using System.Collections.Generic;

namespace ASPNET_CoreSessionApps.Models
{
    public partial class BillMaster
    {
        public BillMaster()
        {
            BillDetails = new HashSet<BillDetails>();
        }

        public int BillNo { get; set; }
        public int BillAmount { get; set; }

        public virtual ICollection<BillDetails> BillDetails { get; set; }
    }
}
