using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class IndentController : Controller
    {
        // GET: Indent
        public ActionResult Indent()
        {
            return View("~/Views/RegularEntry/Indent.cshtml");
        }
    }
}