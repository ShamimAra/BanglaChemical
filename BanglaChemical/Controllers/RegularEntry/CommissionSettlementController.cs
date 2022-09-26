using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class CommissionSettlementController : Controller
    {
        // GET: CommissionSettlement
        public ActionResult CommissionSettlement()
        {
            return View("~/Views/RegularEntry/CommissionSettlement.cshtml");
        }
    }
}