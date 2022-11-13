using System.Data.SqlClient;
using Unit_of_Work;
using UsingUnitOfWork.Dal.Business.Interfaces;
using UsingUnitOfWork.Dal.Business.Services;
using UsingUnitOfWork.Dal.Business.UnitOfWorks.Interfaces;

namespace UsingUnitOfWork.Dal.Business.UnitOfWorks.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProductRepository _productRepository;
        private ICustomerRepository _customerRepository;
        private ISaleItemRepository _saleItemRepository;
        private ISaleRepository _saleRepository;
        protected readonly ShopContext Db;
        public UnitOfWork()
        {
            Db = new ShopContext();
        }
        public virtual void Dispose()
        {
            if (_productRepository != null)
                _productRepository.Dispose();
            if (_customerRepository != null)
                _customerRepository.Dispose();
            if (_saleItemRepository != null)
                _saleItemRepository.Dispose();
            if (_saleRepository != null)
                _saleRepository.Dispose();
        }
        public ICustomerRepository GetCustomerRepository()
        {
            return _customerRepository ?? (_customerRepository = new CustomerRepository(Db));
        }
        public ISaleItemRepository GetSaleItemRepository()
        {
            return _saleItemRepository ?? (_saleItemRepository = new SaleItemRepository(Db));
        }
        public ISaleRepository GetSaleRepository()
        {
            return _saleRepository ?? (_saleRepository = new SaleRepository(Db));
        }
        public IProductRepository GetProductRepository()
        {
            return _productRepository ?? (_productRepository = new ProductRepository(Db));
        }
        public List<T> ExecuteCustomQuery<T>(string command, params KeyValuePair<string, object>[] parameters)
        {
            var _params = new object[parameters.Length];
            for (var i = 0; i < parameters.Length; ++i)
                _params[i] = new SqlParameter(parameters[i].Key,
                parameters[i].Value);
            return Db.ExecuteStoreQuery<T>(command, _params).ToList();
        }
        public T Get<T>(int id) where T : Entity
        {
            var result = Db.Set<T>().Find(id);
            if (result == null)
            {
                Console.WriteLine("Entity {0}:{1} not found in database", typeof(T), id);
                throw new Exception(string.Format("Entity {0}:{1} not found in database", typeof(T), id));
            }
            return result;
        }
        public IQueryable<T> GetAll<T>() where T : Entity
        {
            var result = Db.Set<T>();
            if (result == null)
            {
                Console.WriteLine("Entity type {0} not found in database",
                typeof(T));
                throw new Exception(string.Format("Entity type {0} not found in database", typeof(T)));
            }
            return result;
        }
        public void AsNoLazyLoading()
        {
            Db.Configuration.LazyLoadingEnabled = false;
            Db.Configuration.ProxyCreationEnabled = false;
        }
    }
}