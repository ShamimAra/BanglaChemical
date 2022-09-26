using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class LCAmendmentController : Controller
    {
        // GET: LCAmendment
        public ActionResult LCAmendment()
        {
            return View("~/Views/RegularEntry/LCAmendment.cshtml");
        }
    }
}