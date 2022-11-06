using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unit_of_Work;
namespace UsingUnitOfWork.Dal.Business.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IQueryable<Customer> GetAll();
        Customer GetCustomer(long id);
    }
}
