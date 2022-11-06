using Unit_of_Work;
using UsingUnitOfWork.Dal.Business.Interfaces;

namespace UsingUnitOfWork.Dal.Business.Services
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ShopContext context) : base(context)
        {
        }
        public IQueryable<Product> GetAll()
        {
            try
            {
                return Db.Products;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAll() failed: {0}", ex);
                throw;
            }
        }
    }
}
