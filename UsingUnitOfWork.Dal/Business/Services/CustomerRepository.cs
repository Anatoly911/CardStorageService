using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_of_Work;
using UsingUnitOfWork.Dal.Business.Interfaces;

namespace UsingUnitOfWork.Dal.Business.Services
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ShopContext context) : base(context)
        {
        }
        public IQueryable<Customer> GetAll()
        {
            try
            {
                return Db.Customers;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAll() failed: {0}", ex);
                throw;
            }
        }
        public Customer GetCustomer(long id)
        {
            try
            {
                return Db.Customers.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetCustomer() failed: {0}", ex);
                throw;
            }
        }
    }
}
