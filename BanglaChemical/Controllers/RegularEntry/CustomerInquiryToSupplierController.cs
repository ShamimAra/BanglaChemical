using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class CustomerInquiryToSupplierController : Controller
    {
        // GET: CustomerInquiryToSupplierController
        public ActionResult CustomerInquiryToSupplier()
        {
            return View("~/Views/RegularEntry/CustomerInquiryToSupplier.cshtml");
        }
    }
}