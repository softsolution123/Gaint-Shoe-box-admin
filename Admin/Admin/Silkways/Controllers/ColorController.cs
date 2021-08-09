using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Silkways.Models;
using Facebook;
using System.IO;
using System.Net;

using NetOffice;
using Excel = NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;
using NetOffice.VBIDEApi.Enums;
using NetOffice.OfficeApi.Enums;
using Silkways.Models.SilkwaysFunction;

namespace Silkways.Controllers
{
    public class ColorController : Controller
    {
        // GET: Color
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public ActionResult AddColor()
        {
            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            ViewBag.ID = ID;
            if (ID != 0)
            {
                ViewBag.Edit = "true";
                Session["Id"] = ID;
                ViewBag.Name = new ColorModel().GetbyID(ID).ColorName;
                ViewBag.Val = new ColorModel().GetbyID(ID).ColorValue;
                ViewBag.Company = new ColorModel().GetbyID(ID).CompanyColorValue;
            }
            return View(new ColorModel());
        }

        [HttpPost]
        public ActionResult AddColor(tblColor color)
        {
            
            int ID = 0;
            if (Session["Id"] != null)
            {
                ID = Convert.ToInt32(Session["Id"].ToString());

            }
            if (ID != 0)
            {
                color.ID = ID;
                new ColorModel().Update(color);
                TempData["AlertTask"] = "Record Updated Successfully";
                return View(new ColorModel());
            }
            else
            {
                new ColorModel().Save(color);
                TempData["AlertTask"] = "Record Saved Successfully";
                return View(new ColorModel());
            }
        }
        public ActionResult Delete(int ID)
        {

            if (new ColorModel().DeleteColorProduct(ID) == true)
            {
                new ColorModel().Delete(ID);
            }
            else
            {
                TempData["AlertTask"] = "Color is exist in order. It can not be deleted";
            }

            return RedirectToAction("AddColor", new ColorModel());
        }

        public ActionResult Index()
        {
            var homeimge = db.HomePageImages.Where(x => x.ID == 1).FirstOrDefault();
            return View(homeimge);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            HomePageImage img = new HomePageImage();

        
                if (file != null)
                {
                    string HostedFilePath = "";
                    string FileName = "";

                    img.ImageName = file.FileName;
                    string FilePath = Server.MapPath("/Resources/News/" + img.ImageName);
                    file.SaveAs(FilePath);

                    Silkways.Models.SilkwaysFunction.GernalFunction.FTPBE be = new Silkways.Models.SilkwaysFunction.GernalFunction.FTPBE();
                    be.HostFilePath = FilePath;
                    be.TransferFilePath = "ftp://91.205.175.115/httpdocs/Resources/" + img.ImageName;
                    be.FileName = img.ImageName;
                    bool ISDone = Silkways.Models.SilkwaysFunction.GernalFunction.FtpUploads.FtpUploadUsingFileUpload(be);
                }
                else
                {
                    img.ImageName = "NoImage.jpg";
                }

                    img.ID = 1 ;
                    new NewsModel().UpdateHomeImage(img);
                    TempData["AlertTask"] = "Record Successfully Update";
                return View();
        }
        private Uri RediredtUri
        {

            get
            {

                var uriBuilder = new UriBuilder(Request.Url);

                uriBuilder.Query = null;

                uriBuilder.Fragment = null;

                uriBuilder.Path = Url.Action("FacebookCallback");

                return uriBuilder.Uri;

            }

        }

        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Test(string text, HttpPostedFileBase file)
        {
            Session["text"] = text;
            if (file != null)
            {
                string name = file.FileName;
                string path = Server.MapPath("~/Resources/" + name);
                file.SaveAs(path);

            }

            var fb = new FacebookClient();

            var loginUrl = fb.GetLoginUrl(new

            {

                client_id = "266466697147513",

                client_secret = "6ca56e06c2409e8b273aba5be02e1a98",

                redirect_uri = RediredtUri.AbsoluteUri,

                response_type = "code",

                scope = "email"

            });

            return Redirect(loginUrl.AbsoluteUri);

        }

        //string AppId = "266466697147513";
        //Uri _loginUrl;
        //string _ExtendedPermissions = "user_about_me,publish_stream,offline_access";
        //FacebookClient fbclient = new FacebookClient();

        public ActionResult FacebookCallback(string code)
        {


            string app_id = "266466697147513";
            string app_secret = "6ca56e06c2409e8b273aba5be02e1a98";
            string scope = "publish_stream,manage_pages";

            Dictionary<string, string> tokens = new Dictionary<string, string>();

            string url = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}",
                app_id, Request.Url.AbsoluteUri, scope, Request["code"].ToString(), app_secret);

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string vals = reader.ReadToEnd();

                foreach (string token in vals.Split('&'))
                {
                    //meh.aspx?token1=steve&token2=jake&...
                    tokens.Add(token.Substring(0, token.IndexOf("=")),
                        token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
                }
            }

            string access_token = tokens["access_token"];

            var client = new FacebookClient(access_token);

            client.Post("/me/feed", new { message = "markhagan.me video tutorial" });
            return View();
        }
    }
}