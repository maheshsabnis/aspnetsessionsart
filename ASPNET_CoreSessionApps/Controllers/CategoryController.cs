using ASPNET_CoreSessionApps.Models;
using ASPNET_CoreSessionApps.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNET_CoreSessionApps.Controllers
{
    public class CategoryController : Controller
    {
        IRepository<Category,int> _catRepo;

        public CategoryController(IRepository<Category, int> cat)
        {
            _catRepo = cat;
        }
       

        // GET: /<controller>/
        public IActionResult Index()
        {
            var cats = _catRepo.Get();
            return View(cats);
        }
        //The LoadProducts method will create a new Session Key "CategoryId" and 
        // it will store the CategoryId in it. 
        // This method will redirect to the Index method of the Product Controller
        public IActionResult LoadProducts(int id)
        {
            HttpContext.Session.SetInt32("CategoryId", id);
            return RedirectToAction("Index", "Product");
        }
    }
}
