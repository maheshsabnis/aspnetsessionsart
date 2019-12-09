using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ASPNET_CoreSessionApps.Services;
using ASPNET_CoreSessionApps.Models;
using ASPNET_CoreSessionApps.SessionExtensions;


namespace ASPNET_CoreSessionApps.Controllers
{
    public class FinalBillController : Controller
    {
        IBill finalBill;
        
        public FinalBillController(IBill fb)
        {
            finalBill = fb;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var selProducts = HttpContext.Session.GetSessionData<List<BillDetails>>("PurchasedProduct");

            var billMaster = new BillMaster();
            //Calculate the Total Bill Amount
            foreach (var item in selProducts)
            {
                billMaster.BillAmount += item.RowPrice;
            }

             billMaster.BillDetails = selProducts;

            finalBill.GenerateBill(billMaster, billMaster.BillDetails.ToArray());

            return View(billMaster);
        }

         
    }
}
