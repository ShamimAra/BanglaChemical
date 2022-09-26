using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class ManufactureController : Controller
    {
        // GET: Manufacture
        public ActionResult Manufacture()
        {
            return View("~/Views/RegularEntry/Manufacture.cshtml");
        }
    }
}