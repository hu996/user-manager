using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstructure.IRepository
{
    public interface IServicesRepository<T>where T:class
    {
        List<T> GetAll();
        T FindById(Guid Id);
        T FindByName(string Name);
        bool Save(T model);
        bool Delete(Guid Id);
    }
}
