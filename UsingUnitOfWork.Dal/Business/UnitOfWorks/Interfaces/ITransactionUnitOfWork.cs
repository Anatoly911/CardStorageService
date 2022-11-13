using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_of_Work;

namespace UsingUnitOfWork.Dal.Business.UnitOfWorks.Interfaces
{
    public interface ITransactionUnitOfWork : IUnitOfWork
    {
        void Commit();
        void Remove<T>(T entity) where T : Entity;
        void RemoveRange<T>(IEnumerable<T> entities) where T : Entity;
        T Add<T>(T entity) where T : Entity;
        IEnumerable<T> AddRange<T>(IEnumerable<T> entities) where T : Entity;
        TEntity CreateNew<TEntity>() where TEntity : Entity;
    }
}
