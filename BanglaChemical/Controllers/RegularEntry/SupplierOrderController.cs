using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class SupplierOrderController : Controller
    {
        // GET: SupplierOrder
        public ActionResult SupplierOrder()
        {
            return View("~/Views/RegularEntry/SupplierOrder.cshtml");
        }
    }
}