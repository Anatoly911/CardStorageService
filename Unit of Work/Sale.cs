using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_of_Work
{
    public class Sale : Entity
    {
        public long CustomerId { get; set; }
        public virtual List<SaleItem> SaleItems { get; set; }
        public virtual Customer Customer { get; set; }
        public double TotalCost { get; set; }
        public double Discount { get; set; }
    }
}
