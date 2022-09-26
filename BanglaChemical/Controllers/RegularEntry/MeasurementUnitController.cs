using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class MeasurementUnitController : Controller
    {
        // GET: MeasurementUnit
        public ActionResult MeasurementUnit()
        {
            return View("~/Views/RegularEntry/MeasurementUnit.cshtml");
        }
    }
}