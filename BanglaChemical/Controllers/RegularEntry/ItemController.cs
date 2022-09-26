using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaChemical.Controllers.RegularEntry
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Item()
        {
            return View("~/Views/RegularEntry/Item.cshtml");
        }
    }
}