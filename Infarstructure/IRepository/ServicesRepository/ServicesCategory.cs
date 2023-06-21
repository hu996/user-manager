using Domain.Entity;
using Infarstructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstructure.IRepository.ServicesRepository
{
    public class ServicesCategory : IServicesRepository<Category>
    {
        private readonly ApplicationDbcontext _dbcontext;
        public ServicesCategory(ApplicationDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public bool Delete(Guid Id)
        {
            try
            {
                var result = _dbcontext.categories.FirstOrDefault(x => x.ID == Id);
                if (result != null)
                {

                    _dbcontext.categories.Remove(result);
                    _dbcontext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;

                }
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public Category FindById(Guid Id)
        {
            try
            {
                var result = _dbcontext.categories.FirstOrDefault(x => x.ID == Id);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public Category FindByName(string Name)
        {
            var result = _dbcontext.categories.FirstOrDefault(x => x.Name == Name);
            return result;
        }

        public List<Category> GetAll()
        {
            try
            {
                var result = _dbcontext.categories.OrderBy(x => x.Name).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Save(Category model)
        {
            try
            {
                var result = _dbcontext.categories.FirstOrDefault(x => x.ID == model.ID);

                if (result == null)
                {
                    //Add New Category
                    model.ID = Guid.NewGuid();
                    _dbcontext.categories.Add(model);
                    _dbcontext.SaveChanges();
                    return true;
                }
                else
                {
                    //update category
                    result.Name = model.Name;
                    result.CurrentStatus = model.CurrentStatus;
                    result.Description = model.Description;
                    _dbcontext.categories.Update(result);
                    _dbcontext.SaveChanges();
                    return true;
                }
                ;/*_dbcontext.SaveChanges();*/
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
