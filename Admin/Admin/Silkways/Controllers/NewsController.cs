using Silkways.Models;
using Silkways.Models.SilkwaysFunction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Silkways.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult AddNews()
        {
           

            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                News Edit = new NewsModel().GetNewsByID(ID);
                Edit.Image = Constants.PathNewsImages + Edit.Image;
                return View(Edit);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNews(News news, HttpPostedFileBase file)
        {

            //if (news.DateTime != null)
            //{

            //    //DateTime now = DateTime.Now;
            //    //TimeSpan time = now.TimeOfDay;
            //    //news.DateTime = news.DateTime.Add(time);


            //    //DateTime today = DateTime.Now;
            //    //TimeSpan duration = new TimeSpan(36, 0, 0, 0);
            //    //DateTime answer = today.Add(duration);

            //}
      

            if (news.Description != null && news.Description != "")
            {
                if (file != null)
                {
                    news.Image = GernalFunction.SaveFile(file, Constants.PathNewsImages);
                }
                else
                {
                    news.Image = Constants.NoImage;
                }


                if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
                {
                    news.ID = Convert.ToInt32(Request.QueryString["ID"]);
                    //news.DateTime = DateTime.Now;
                    new NewsModel().Update(news);
                    TempData["AlertTask"] = "Record Successfully Update";
                }
                else
                {
                    //news.DateTime = DateTime.Now;
                    new NewsModel().Save(news);
                    TempData["AlertTask"] = "Record Successfully Saved";
                }
                return RedirectToAction("ManageNews");
            }
            else
            {
                TempData["AlertTask"] = "Please Enter the Description";
                return View();
            }
        }

        public ActionResult ManageNews()
        {
            List<News> lst = new NewsModel().GetAllNews();
            return View(lst);
        }

        public void Delete(int ID)
        {
            new NewsModel().Delete(ID);
            TempData["AlertTask"] = "Record Successfully Deleted";
            Response.Redirect("ManageNews");
        }
    }
}