using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_of_Work;

namespace UsingUnitOfWork.Dal.Business.Interfaces
{
    public interface ISaleItemRepository : IRepository<SaleItem>
    {
        IQueryable<SaleItem> GetAll();
        IQueryable<SaleItem> GetAllByCustomerId(int customerId);
        IQueryable<SaleItem> GetAllByProductId(int productId);
    }
}
