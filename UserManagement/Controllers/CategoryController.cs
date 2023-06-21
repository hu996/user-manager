using Domain.Entity;
using Infarstructure.IRepository;
using Infarstructure.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace UserManagement.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IServicesRepository<Category> _category;
        private readonly IServicesLogRepository<LogCategory> _logCategory;
        private readonly UserManager<ApplicationUser> _userManager;
        public CategoryController(IServicesRepository<Category> category, IServicesLogRepository<LogCategory> logCategory,
            UserManager<ApplicationUser> userManager)
        {
            _category = category;
            _logCategory = logCategory;
            _userManager = userManager;
        }
        public IActionResult Categgories()
        {
            return View(new CategoryViewModel
            {
                category = _category.GetAll(),
                logCategory = _logCategory.GetAll(),
                NewCategory = new Category()
            });
        }
        public IActionResult Delete(Guid Id)
        {
            var userID = _userManager.GetUserId(User);
            if (_category.Delete(Id) && _logCategory.Delete(Id, Guid.Parse(userID)))
            {
                return RedirectToAction("Categgories");
            }
            else
            {
                return RedirectToAction("Categgories");
            }
        }
        public IActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                if (model.NewCategory.ID == Guid.Parse(Guid.Empty.ToString()))
                {
                    // Add New Category
                    if (_category.FindByName(model.NewCategory.Name) != null)
                    {
                        //not success
                        HttpContext.Session.SetString("MessageType", "Error");
                        HttpContext.Session.SetString("Title", "Duplicate Name");
                        HttpContext.Session.SetString("Message", "No category had been Added");
                    }
                    else
                    {
                        if (_category.Save(model.NewCategory) && _logCategory.Save(model.NewCategory.ID, Guid.Parse(userId)))
                        {
                            HttpContext.Session.SetString("MessageType", "Success");
                            HttpContext.Session.SetString("Title", "Saved Successfully");
                            HttpContext.Session.SetString("Message", "New category Had Been Added Successfully");
                        }
                       else{
                            HttpContext.Session.SetString("MessageType", "Error");
                            HttpContext.Session.SetString("Title", "not valid ");
                            HttpContext.Session.SetString("Message", "No category had been Added");
                        }
                    }
                }
                else
                {
                    //Updae Category
                    if (_category.Save(model.NewCategory) && _logCategory.Update(model.NewCategory.ID, Guid.Parse(userId)))
                    {
                        HttpContext.Session.SetString("MessageType", "Success");
                        HttpContext.Session.SetString("Title", "Saved Successfully");
                        HttpContext.Session.SetString("Message", "New category Had Been Added Successfully");
                    }
                    else
                    {
                        HttpContext.Session.SetString("MessageType", "Error");
                        HttpContext.Session.SetString("Title", "not valid ");
                        HttpContext.Session.SetString("Message", "No category had been Added");
                    }
                }
            }

            return RedirectToAction("Categories");
        }
        public IActionResult Index()
        {

            return View();
        }


    }
}

