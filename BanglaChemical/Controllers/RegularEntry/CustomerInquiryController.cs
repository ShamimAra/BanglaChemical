using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class CustomerInquiryController : Controller
    {
        // GET: Requisition
        public ActionResult CustomerInquiry()
        {
            return View("~/Views/RegularEntry/CustomerInquiry.cshtml");
        }
    }
}