using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_of_Work;
using UsingUnitOfWork.Dal.Business.Interfaces;

namespace UsingUnitOfWork.Dal.Business.UnitOfWorks.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository GetCustomerRepository();
        ISaleItemRepository GetSaleItemRepository();
        ISaleRepository GetSaleRepository();
        IProductRepository GetProductRepository();
        List<T> ExecuteCustomQuery<T>(string command, params KeyValuePair<string,
        object>[] parameters);
        T Get<T>(int id) where T : Entity;
        IQueryable<T> GetAll<T>() where T : Entity;
        void AsNoLazyLoading();
    }
}
