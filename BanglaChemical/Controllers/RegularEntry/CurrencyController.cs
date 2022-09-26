using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class CurrencyController : Controller
    {
        // GET: Currency
        public ActionResult Currency()
        {
            return View("~/Views/RegularEntry/Currency.cshtml");
        }
    }
}