using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanglaChemical.Models.UserManagement
{
    public class MenuModel
    {
        public bool hasPermission { get; set; }
        public string MENU_ITEM_ID { get; set; }
        public string PARENT_MENU_ITEM_ID { get; set; }
        public string MENU_DESCRIPTION { get; set; }
        public string MENU_URL { get; set; }
        public string MENU_ICON { get; set; }
    }
}