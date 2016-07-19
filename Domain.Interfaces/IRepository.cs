using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class 
    {
        IEnumerable<T> GetAllActive();
        T Get(int id);
        

        void Add(T item);

        void Deactivate(int id);

        bool Exists(int id);
    }
}
