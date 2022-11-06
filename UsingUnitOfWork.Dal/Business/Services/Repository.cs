using System.Data.Entity;
using System.Threading.Tasks;
using Unit_of_Work;
using UsingUnitOfWork.Dal.Business.Interfaces;

namespace UsingUnitOfWork.Dal.Business.Services
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ShopContext _db;
        protected Repository(ShopContext context)
        {
            _db = context;
        }
        public T Get(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("id");
            T entity = null;
            try
            {
                entity = GetEntity(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("db.Set<{2}>().Find({0}) threw exception: {1}",
                id, ex, typeof(T).Name);
                throw;
            }
            if (entity == null)
                throw new InvalidOperationException(string.Format("{0} with ID={1} was not found in the DB", typeof(T).Name, id));
            return entity;
        }
        public virtual T Save(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            Console.WriteLine("Saving '{0}'", entity);
            return entity.Id == 0 ? Add(entity) : Update(entity);
        }
        public void Dispose()
        {
        }
        protected ShopContext Db
        {
            get { return _db; }
        }
        protected virtual T GetEntity(int id)
        {
            return _db.Set<T>().Find(id);
        }
        protected virtual T GetDetachedEntity(int id)
        {
            return _db.Set<T>().AsNoTracking().FirstOrDefault(entry => entry.Id == id);
        }
        private T Add(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
            return entity;
        }
        private T Update(T entity)
        {
            _db.Set<T>().Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return entity;
        }
    }
}
