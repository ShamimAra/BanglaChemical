using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class OriginalLCController : Controller
    {
        // GET: OriginalLC
        public ActionResult OriginalLC()
        {
            return View("~/Views/RegularEntry/OriginalLC.cshtml");
        }
    }
}