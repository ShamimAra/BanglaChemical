using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class CountryController : Controller
    {
        // GET: Country
        public ActionResult Country()
        {
            return View("~/Views/RegularEntry/Country.cshtml");
        }
    }
}