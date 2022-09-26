using System;
using System.Web.Mvc;
using Connection;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using BanglaChemical.Models;
using BanglaChemical.Models.UserManagement;
using BanglaChemical.Models.SetUp;

namespace BanglaChemical.Controllers
{
    public class HomeController : Controller
    {
        string ConnString = "IMS";
        public static List<MenuModel> globalMenuList;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string UserId, string Password)
        {
            // string userName = UserId.ToLower();

            if (UserId != null && Password != null)
            {
                Database_ora db = new Database_ora(true, ConnString, "ODP");

                Oracle.DataAccess.Client.OracleParameter[] Params = new Oracle.DataAccess.Client.OracleParameter[3];
                Params[0] = db.MakeOutParameter("o_status", Oracle.DataAccess.Client.OracleDbType.Int16, ParameterDirection.Output);
                Params[1] = db.MakeInParameter("p_User_Id", UserId, Oracle.DataAccess.Client.OracleDbType.Varchar2);
                Params[2] = db.MakeInParameter("p_User_Pass", Password, Oracle.DataAccess.Client.OracleDbType.Varchar2);
                Int32 loginStatus = db.RunProcedureWithReturnVal("IMS.DPG_ADMIN_LOGIN.DPD_ADMIN_LOGIN", Params);

                if (UserId == "" || UserId == null)
                {
                    ModelState.AddModelError("", "User name Field is empty.");
                    return View("~/Views/Home/Index.cshtml");
                }

                if (Password == "" || Password == null)
                {
                    ModelState.AddModelError("", "Password Field is empty.");
                    return View("~/Views/Home/Index.cshtml");
                }

                if (loginStatus == 0)
                {
                    string errorMessage = "User name or Password is incorrect.";
                    ModelState.AddModelError("", errorMessage);
                    return View("~/Views/Home/Index.cshtml");
                }

                Oracle.DataAccess.Client.OracleParameter[] prmLoginData = new Oracle.DataAccess.Client.OracleParameter[2];
                prmLoginData[0] = db.MakeOutParameter("cur_data", Oracle.DataAccess.Client.OracleDbType.RefCursor, ParameterDirection.Output);
                prmLoginData[1] = db.MakeInParameter("p_User_Id", UserId, Oracle.DataAccess.Client.OracleDbType.Varchar2);
                DataTable dtLoginData = db.GetDataSet("IMS.DPG_ADMIN_LOGIN.DPD_ADMIN_LOGIN_DATA", prmLoginData).Tables[0];

                Session["dtLoginData"] = dtLoginData;

                return RedirectToAction("Dashboard", "Home");
            }

            return View();
        }

        public ActionResult LogOff()
        {
            //Removes all keys and values from the session-state collection.
            HttpContext.Session.Clear();
            //Cancels the current session.
            HttpContext.Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Dashboard()
        {
            if (Session["dtLoginData"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            DataTable dtLoginData = new DataTable();

            string userCode = string.Empty;
            string roleID = string.Empty;
            string ORG_CODE = string.Empty;

            if (Session["dtLoginData"] != null)
            {
                dtLoginData = (DataTable)Session["dtLoginData"];
            }

            if (dtLoginData.Rows.Count > 0)
            {
                userCode = Convert.ToString(dtLoginData.Rows[0]["USER_CODE"]);
                roleID = Convert.ToString(dtLoginData.Rows[0]["ROLE_ID"]);
                ORG_CODE = Convert.ToString(dtLoginData.Rows[0]["ORG_CODE"]);
            }

            DataTable dtMenuInfo = new DataTable();

            Database_ora db = new Database_ora(true, ConnString, "ODP");

            Oracle.DataAccess.Client.OracleParameter[] prmMenuInfo = new Oracle.DataAccess.Client.OracleParameter[3];
            prmMenuInfo[0] = db.MakeOutParameter("cur_data", Oracle.DataAccess.Client.OracleDbType.RefCursor, ParameterDirection.Output);
            prmMenuInfo[1] = db.MakeInParameter("p_User_Id", userCode, Oracle.DataAccess.Client.OracleDbType.Varchar2);
            prmMenuInfo[2] = db.MakeInParameter("p_Role_Id", roleID, Oracle.DataAccess.Client.OracleDbType.Varchar2);
            dtMenuInfo = db.GetDataSet("IMS.DPG_ADMIN_LOGIN.DPD_ADMIN_MENU", prmMenuInfo).Tables[0];

            
            globalMenuList = makeMenuData(dtMenuInfo);
            ViewBag.menuList = globalMenuList;
            return View("~/Views/Home/Dashboard.cshtml");
        }

        //make Menu Data funciton
        public List<MenuModel> makeMenuData(DataTable dtMenuInfo)
        {
            List<MenuModel> dataBundle = new List<MenuModel>();

            for (int i = 0; i < dtMenuInfo.Rows.Count; i++)
            {
                MenuModel MenuModel = new MenuModel();
                MenuModel.MENU_ITEM_ID = dtMenuInfo.Rows[i]["MENU_ITEM_ID"].ToString();
                MenuModel.PARENT_MENU_ITEM_ID = dtMenuInfo.Rows[i]["PARENT_MENU_ITEM_ID"].ToString();
                MenuModel.MENU_DESCRIPTION = dtMenuInfo.Rows[i]["MENU_DESCRIPTION"].ToString();
                MenuModel.MENU_URL = dtMenuInfo.Rows[i]["MENU_URL"].ToString();
                MenuModel.MENU_ICON = dtMenuInfo.Rows[i]["MENU_ICON"].ToString();
                dataBundle.Add(MenuModel);
            }
            return dataBundle;
        }

        public ActionResult ChangePassword()
        {
            if (Session["dtLoginData"] == null)
            {
                //Removes all keys and values from the session-state collection.
                HttpContext.Session.Clear();
                //Cancels the current session.
                HttpContext.Session.Abandon();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.menuList = globalMenuList;
            return View();
        }

        public ActionResult ChangePasswordSave(string userId, string oldPassword, string newPassword, string user)
        {

            string[] message = new string[3];
            Database_ora db = new Database_ora(true, ConnString, "ODP");

            Oracle.DataAccess.Client.OracleParameter[] Params = new Oracle.DataAccess.Client.OracleParameter[5];
            Params[0] = db.MakeOutParameter("o_Status", Oracle.DataAccess.Client.OracleDbType.Int16, ParameterDirection.Output);
            Params[1] = db.MakeInParameter("p_User_Id", userId, Oracle.DataAccess.Client.OracleDbType.Varchar2);
            Params[2] = db.MakeInParameter("p_Old_Pass", oldPassword, Oracle.DataAccess.Client.OracleDbType.Varchar2);
            Params[3] = db.MakeInParameter("p_New_Pass", newPassword, Oracle.DataAccess.Client.OracleDbType.Varchar2);
            Params[4] = db.MakeInParameter("p_User", user, Oracle.DataAccess.Client.OracleDbType.Varchar2);
            Int32 status = db.RunProcedureWithReturnVal("IMS.DPG_ADMIN_LOGIN.DPD_ADMIN_PASS_CHANGE", Params);

            if (status == 1)
            {
                message[0] = "Password changed successfully !!";
                message[1] = "#5cb85c";
                message[2] = status.ToString();
            }
            else
            {
                message[0] = "Password changing failed !!";
                message[1] = "#e35b5a";
                message[2] = status.ToString();
            }

            //Removes all keys and values from the session-state collection.
            HttpContext.Session.Clear();
            //Cancels the current session.
            HttpContext.Session.Abandon();

            return Json(message, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
    }
}