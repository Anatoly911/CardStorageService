using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingUnitOfWork.Dal.Business.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        T Get(int id);
        T Save(T entity);
    }
}
