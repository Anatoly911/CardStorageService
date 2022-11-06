using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_of_Work;
using UsingUnitOfWork.Dal.Business.Interfaces;

namespace UsingUnitOfWork.Dal.Business.Services
{
    public class SaleItemRepository : Repository<SaleItem>, ISaleItemRepository
    {
        public SaleItemRepository(ShopContext context) : base(context)
        {
        }
        public IQueryable<SaleItem> GetAll()
        {
            try
            {
                return Db.SaleItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAll() failed: {0}", ex);
                throw;
            }
        }
        public IQueryable<SaleItem> GetAllByCustomerId(int customerId)
        {
            try
            {
                return Db.SaleItems.Where(s => s.CustomerId == customerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllByCustomerId() failed: {0}", ex);
                throw;
            }
        }
        public IQueryable<SaleItem> GetAllByProductId(int productId)
        {
            try
            {
                return Db.SaleItems.Where(s => s.ProductId == productId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllByProductId() failed: {0}", ex);
                throw;
            }
        }
    }
}
