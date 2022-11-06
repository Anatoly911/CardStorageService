using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Unit_of_Work;
using UsingUnitOfWork.Dal.Business.UnitOfWorks.Interfaces;

namespace UsingUnitOfWork.Dal.Business.UnitOfWorks.Services
{
    public class TransactionUnitOfWork : UnitOfWork, ITransactionUnitOfWork
    {
        #region Variables
        private readonly TransactionScope _transaction;
        private bool _transactionCompleted;
        #endregion
        public TransactionUnitOfWork()
        {
            _transaction = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions
            {
                IsolationLevel =
            IsolationLevel.ReadCommitted
            });
        }
        public void Commit()
        {
            Db.SaveChanges();
            if (_transactionCompleted)
                throw new InvalidOperationException("Current transaction is already commited");
                _transaction.Complete();
            _transaction.Dispose();
            _transactionCompleted = true;
        }
        public void Remove<T>(T entity) where T : Entity
        {
            Db.Set<T>().Remove(entity);
        }
        public void RemoveRange<T>(IEnumerable<T> entities) where T : Entity
        {
            Db.Set<T>().RemoveRange(entities);
        }
        public T Add<T>(T entity) where T : Entity
        {
            return Db.Set<T>().Add(entity);
        }
        public IEnumerable<T> AddRange<T>(IEnumerable<T> entities) where T : Entity
        {
            return Db.Set<T>().AddRange(entities);
        }
        public TEntity CreateNew<TEntity>() where TEntity : Entity
        {
            var set = Db.Set<TEntity>();
            var entity = set.Create();
            set.Add(entity);
            return entity;
        }
    }
}
