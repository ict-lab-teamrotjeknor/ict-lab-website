using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(string id);
        T GetByName(string name);
        void Add(T entity);
        void Delete(T entity);
    }
}
