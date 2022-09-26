using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class SupplierToCustomerController : Controller
    {
        // GET: SupplierToCustomer
        public ActionResult SupplierToCustomer()
        {
            return View("~/Views/RegularEntry/SupplierToCustomer.cshtml");
        }
    }
}