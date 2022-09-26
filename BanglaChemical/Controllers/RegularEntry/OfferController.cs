using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class OfferController : Controller
    {
        // GET: Requisition
        public ActionResult Offer()
        {
            return View("~/Views/RegularEntry/Offer.cshtml");
        }
    }
}