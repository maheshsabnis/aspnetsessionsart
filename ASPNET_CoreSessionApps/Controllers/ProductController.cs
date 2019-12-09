using ASPNET_CoreSessionApps.Models;
using ASPNET_CoreSessionApps.Services;
using ASPNET_CoreSessionApps.SessionExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace ASPNET_CoreSessionApps.Controllers
{
    public class ProductController : Controller
    {
        
        IRepository<Product, int> _prdRepo;
        public ProductController(IRepository<Product, int> prd)
        {
            _prdRepo = prd;
        }
        // GET: /<controller>/
        // The CategoryId will be retrieved from the session and Products will 
        //be displayed based on the selected CategoryId
        public IActionResult Index()
        {
            var catId = HttpContext.Session.GetInt32("CategoryId");
            List<Product> Products = null;
            if (catId != 0)
            {
                Products = _prdRepo.Get().Where(p => p.CategoryId == catId).ToList();
            }
            return View(Products);
        }

        // This method will redirect to the Index view of the BillGenerator
        // The method will add UnitPrice and BillDetails object in the Session
        public IActionResult SelectForPurchase(int id)
        {
            var prd = _prdRepo.Get(id);
            var billDetails = new BillDetails()
            {
                ProductId = id,
                ProductName = prd.ProductName,
                Quantity = 0,
                RowPrice = 0
            };
            HttpContext.Session.SetInt32("UnitPrice", prd.UnitPrice);
            HttpContext.Session.SetSessionData<BillDetails>("SelProduct", billDetails);
            return RedirectToAction("Index","BillGenerator");
        }

        
    }
}
