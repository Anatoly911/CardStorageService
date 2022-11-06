using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_of_Work;

namespace UsingUnitOfWork.Dal.Business.Interfaces
{
    public interface ISaleRepository : IRepository<Sale>
    {
        IQueryable<Sale> GetAll();
        IQueryable<Sale> GetAllByCustomerId(int customerId);
    }
}
