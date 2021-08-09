using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Silkways.Models;
using System.IO;
using Silkways.Models.SilkwaysFunction;

namespace Silkways.Controllers
{
    public class CMSPagesController : Controller
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        // GET: CMSPages
        public ActionResult URLBasedCMSPages()
        {
            List<URLBasedCMSPage> lst = new URLBasedCMS().GetAllCMSPages();
            return View(lst);
        }
        public ActionResult AddCMSPage()
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                URLBasedCMSPage Edit = new URLBasedCMS().GetCMSPageByID(ID);
                ViewBag.img += "<li style='width:215px; height:215px;'><img src='" + Constants.PathCMSImages + "" + Edit.Pageimage + "' value='" + Edit.ID + "'  width='200px'  height='200px' style='float: left; height:200px; width=200px; margin: 0px; ' /><span value='" + Edit.ID + " ' onclick='ImageRemove(this);' class='close'>X</span><input type='text' value='" + Edit.Pageimage + "' style='display:none;' /></li>";
                return View(Edit);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddCMSPage(URLBasedCMSPage page, string hdn_UserID, HttpPostedFileBase file)
        {
            if (page.Description != "" && page.Description != null)
            {
                page.Date = DateTime.Now;
                page.UserID = GernalFunction.GetLoginUserID();
                if (page.UserID == 0)
                {
                    return Redirect("/user/login");
                }
                if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
                {
                    if (file != null)
                    {
                        if (file != null)
                        {
                            page.Pageimage = GernalFunction.SaveFile(file, Constants.PathCMSImages);
                        }
                        else
                        {
                            page.Pageimage = Constants.NoImage;
                        }
                    }
                    page.ID = Convert.ToInt32(Request.QueryString["ID"]);
                    new URLBasedCMS().Update(page);
                    TempData["AlertTask"] = "Record Successfully Update";
                }
                else if (db.URLBasedCMSPages.Where(x => x.URL == page.URL).FirstOrDefault() == null)
                {
                    if (file != null)
                    {
                        page.Pageimage = GernalFunction.SaveFile(file, Constants.PathCMSImages);
                    }
                    else
                    {
                        page.Pageimage = Constants.NoImage;
                    }
                    new URLBasedCMS().Save(page);
                    TempData["AlertTask"] = "Record Successfully Saved";

                }
                else
                {
                    TempData["AlertTask"] = "Record already exists";
                    return View();
                }
                return RedirectToAction("URLBasedCMSPages");
            }
            else
            {
                TempData["AlertTask"] = "Please Enter the Description";
                return View();
            }
        }

        [HttpPost]
        public ActionResult SearchByUrl(string url)
        {
            URLBasedCMSPage page = new URLBasedCMS().GetCMSPageByURL(url);
            return View(page);
        }
        public void Delete(int id)
        {
            var fileName = db.URLBasedCMSPages.Where(x => x.ID == id).FirstOrDefault().Pageimage;
            if (System.IO.File.Exists(Server.MapPath(Constants.PathCMSImages + fileName)) == true)
            {
                System.IO.File.Delete(Server.MapPath(Constants.PathCMSImages + fileName));
            }
            new URLBasedCMS().Delete(id);
            TempData["AlertTask"] = "Record Successfully Deleted";
            Response.Redirect("URLBasedCMSPages");
        }

        [HttpPost]
        public void DeleteImage(int id)
        {
            var fileName = db.URLBasedCMSPages.Where(x => x.ID == id).FirstOrDefault().Pageimage;
            if (System.IO.File.Exists(Server.MapPath(Constants.PathCMSImages + fileName)) == true)
            {
                System.IO.File.Delete(Server.MapPath(Constants.PathCMSImages + fileName));
            }
            URLBasedCMSPage page = new URLBasedCMSPage();
            page.Pageimage = null;
            new URLBasedCMS().ImageUpdate(page);
        }
    }
}