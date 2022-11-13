using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_of_Work;
using UsingUnitOfWork.Dal.Business.Interfaces;

namespace UsingUnitOfWork.Dal.Business.Services
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(ShopContext context) : base(context)
        {
        }
        public IQueryable<Sale> GetAll()
        {
            try
            {
                return Db.Sales;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAll() failed: {0}", ex);
                throw;
            }
        }
        public IQueryable<Sale> GetAllByCustomerId(int customerId)
        {
            try
            {
                return Db.Sales.Where(s => s.Customer.Id == customerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllByCustomerId() failed: {0}", ex);
                throw;
            }
        }
    }
}
