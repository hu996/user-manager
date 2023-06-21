using Domain.Entity;
using Infarstructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstructure.IRepository.ServicesRepository
{
    public class ServicesLogCategory : IServicesLogRepository<LogCategory>
    {
        private readonly ApplicationDbcontext _context;
        public ServicesLogCategory(ApplicationDbcontext context)
        {
                _context= context;
        }
        public bool Delete(Guid Id, Guid UserId)
        {
            try
            {
                var logCategory = new LogCategory
                {
                    ID=Guid.NewGuid(),
                    Action="Delete",
                    Date=DateTime.Now,
                    UserId=UserId,
                    CategoryId=Id
                    
                };
                _context.logCategories.Add(logCategory);
                _context.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public LogCategory FindBy(Guid Id)
        {
            try
            {
                var result=_context.logCategories.Include(x=>x.category).FirstOrDefault(x => x.ID == Id);
                return result;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public List<LogCategory> GetAll()
        {
           return _context.logCategories.Include(x=>x.category).OrderBy(x=>x.Action).ToList();
        }

        public bool Save(Guid Id, Guid UserId)
        {
            try
            {
                var logCategory = new LogCategory
                {
                    ID = Guid.NewGuid(),
                    Action = "Create",
                    Date = DateTime.Now,
                    UserId = UserId,
                    CategoryId = Id

                };
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool Update(Guid Id, Guid UserId)
        {
            try
            {
                var logCategory = new LogCategory
                {
                    ID = Guid.NewGuid(),
                    Action = "Update",
                    Date = DateTime.Now,
                    UserId = UserId,
                    CategoryId = Id
                };
                _context.logCategories.Add(logCategory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
