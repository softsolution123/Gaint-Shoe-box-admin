using Silkways.Models;
using Silkways.Models.SilkwaysFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Silkways.Controllers
{
    public class ClientsController : Controller
    {
        public ActionResult AddClients()
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                Client Edit = new ClientsModel().GetClientsByID(ID);
                return View(Edit);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddClients(Client sting, HttpPostedFileBase file)
        {
            if (file != null)
            {
                sting.Image = GernalFunction.SaveFile(file, Constants.PathClientImage);
            }
            else if (!string.IsNullOrEmpty(sting.Image))
            {
                sting.Image = sting.Image;
            }
            else
            {
                sting.Image = Constants.NoImage;
            }
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                sting.ID = Convert.ToInt16(Request.QueryString["ID"]);
                new ClientsModel().Update(sting);
                TempData["AlertTask"] = "Record Successfully Update";
            }
            else
            {
                new ClientsModel().Save(sting);
                TempData["AlertTask"] = "Record Successfully Saved";
            }
            return RedirectToAction("ManageClients");
        }

        public ActionResult ManageClients()
        {
            List<Client> lst = new ClientsModel().GetAllClients();
            return View(lst);
        }

        public void Delete(int ID)
        {
            new ClientsModel().Delete(ID);
            TempData["AlertTask"] = "Record Successfully Delete";
            Response.Redirect("ManageClients");
        }
    }
}