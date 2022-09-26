using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class ShipmentAdvisementController : Controller
    {
        // GET: ShipmentAdvisement
        public ActionResult ShipmentAdvisement()
        {
            return View("~/Views/RegularEntry/ShipmentAdvisement.cshtml");
        }
    }
}