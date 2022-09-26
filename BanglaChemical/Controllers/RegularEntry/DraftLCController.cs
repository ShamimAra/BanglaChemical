using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class DraftLCController : Controller
    {
        // GET: DraftLC
        public ActionResult DraftLC()
        {
            return View("~/Views/RegularEntry/DraftLC.cshtml");
        }
    }
}