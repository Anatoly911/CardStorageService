using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_of_Work
{
    public class SaleItem : Entity
    {
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string Comment { get; set; }
    }
}
