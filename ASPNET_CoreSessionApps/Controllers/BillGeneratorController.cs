using ASPNET_CoreSessionApps.Models;
using ASPNET_CoreSessionApps.SessionExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace ASPNET_CoreSessionApps.Controllers
{
    public class BillGeneratorController : Controller
    {
        List<BillDetails> selProducts;

        public BillGeneratorController()
        {
            selProducts = new List<BillDetails>();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.UnitPrice = HttpContext.Session.GetInt32("UnitPrice");
            var billDetails = HttpContext.Session.GetSessionData<BillDetails>("SelProduct");
            if (billDetails != null)
            {
                selProducts = HttpContext.Session.GetSessionData<List<BillDetails>>("PurchasedProduct");
            }
            return View(billDetails);
        }

        [HttpPost]
        public IActionResult Index(BillDetails b, string purchase)
        {
            if (purchase == "Save and Continue Purchase")
            {
                //Check if the session contains List of purchased prducts
                selProducts = HttpContext.Session.GetSessionData<List<BillDetails>>("PurchasedProduct");
                if (selProducts == null)
                {
                    selProducts = new List<Models.BillDetails>();
                }

                //Save the selected product in Session
                selProducts.Add(b);
                HttpContext.Session.SetSessionData<List<BillDetails>>("PurchasedProduct", selProducts);
                //Go to the ProductMVC     
                return RedirectToAction("Index", "Product");
            }
            else if(purchase == "Save and CheckOut")
            {
                selProducts = HttpContext.Session.GetSessionData<List<BillDetails>>("PurchasedProduct");
                if (selProducts == null)
                {
                    selProducts = new List<Models.BillDetails>();
                }
                var selBill = HttpContext.Session.GetSessionData<BillDetails>("SelProduct");
                ViewBag.UnitPrice = HttpContext.Session.GetInt32("UnitPrice");
                selBill.Quantity = b.Quantity;
                selBill.RowPrice = b.RowPrice;
                selProducts.Add(selBill);
                HttpContext.Session.SetSessionData<List<BillDetails>>("PurchasedProduct", selProducts);
                return RedirectToAction("Index", "FinalBill");
            }
            return View();
        }
    }
}