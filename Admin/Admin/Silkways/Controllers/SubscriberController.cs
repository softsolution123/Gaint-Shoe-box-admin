using EmailFunctions;
using Silkways.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Silkways.Controllers
{
    public class email
    {
        public string Email { get; set; }
        public int ID { get; set; }
        public string status { get; set; }
    }
    public class SubscriberController : Controller
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public ActionResult Import()
        {
            return View();
        }
        public JsonResult onebyone(string obj)
        {
            
           

                if (IsValidEmail(obj))
                {
                    Subscriber subscriber = new Subscriber();
                    subscriber.Date = DateTime.Now;
                    subscriber.Email = obj;
                 if(db.Subscribers.Where(u=>u.Email==obj).Any())
                {
                    return Json(new { status = "double", ID = 0}, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Subscribers.Add(subscriber);
                    db.SaveChanges(); return Json(new { status = "true", ID = subscriber.ID }, JsonRequestBehavior.AllowGet);
                }
                   


            }
                else
                {
                return Json(new { status = "false", ID = 0 }, JsonRequestBehavior.AllowGet);


            }


           
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //public void delete (int ID)
        //{
        //    var data = db.Subscribers.Where(u => u.ID == ID).FirstOrDefault();
        //    db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
        //    db.SaveChanges();
        //}
        public JsonResult saveexcel(List<email> obj)
        {
            List<email> valid = new List<email>();
            List<email> nonvalid = new List<email>();
           
            foreach (var item in obj)
            {
              
                if(IsValidEmail(item.Email))
                {
                    Subscriber subscriber = new Subscriber();
                    subscriber.Date = DateTime.Now;
                    subscriber.Email = item.Email;
                    if (db.Subscribers.Where(u => u.Email == item.Email).Any())
                    {
                        nonvalid.Add(new email
                        {
                            Email = item.Email,
                            status = "Already Exist"
                        });
                    }
                    else
                    {
                        db.Subscribers.Add(subscriber);
                        db.SaveChanges();
                        valid.Add(new email
                        {
                            Email = item.Email,
                            ID = subscriber.ID,
                            status = "Imported"
                        });
                    }
                   
                }
                else
                {
                    nonvalid.Add(new email
                    {
                        Email = item.Email,
                        status="Invalid Email"
                    });

                }
                
            }
            return Json(new { valid, nonvalid }, JsonRequestBehavior.AllowGet) ;
        }
        public JsonResult saveexcel2(string obj)
        {
            List<email> valid = new List<email>();
            List<email> nonvalid = new List<email>();
            string[] words = obj.Split(',');
            foreach (var item in words)
            {

                if (IsValidEmail(item))
                {
                    Subscriber subscriber = new Subscriber();
                    subscriber.Date = DateTime.Now;
                    subscriber.Email = item;
                    if (db.Subscribers.Where(u => u.Email == item).Any())
                    {
                        nonvalid.Add(new email
                        {
                            Email = item,
                            status = "Already Exist"
                        });
                    }
                    else
                    {
                        db.Subscribers.Add(subscriber);
                        db.SaveChanges();
                        valid.Add(new email
                        {
                            Email = item,
                            ID = subscriber.ID,
                            status = "Imported"
                        });
                    }
                   
                }
                else if(item==" ")
                {

                }
                else if (item == "")
                {

                }
                else
                {
                    nonvalid.Add(new email
                    {
                        Email = item,
                        status="Invalid Email"
                    });

                }

            }
            return Json(new { valid, nonvalid }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddSubscriber()
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                Subscriber Edit = new SubscribersModel().GetSubscriberByID(ID);
                return View(Edit);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddSubscriber(string Email)
        {
            Subscriber sub = new Subscriber();
            sub.Email = Email;
            sub.Date = DateTime.Now;
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                sub.ID = Convert.ToInt32(Request.QueryString["ID"]);
               TempData["AlertTask"]=new SubscribersModel().Update(sub);
            }
            else
            {
                TempData["AlertTask"] = new SubscribersModel().Save(sub);
            }
            
            if (TempData["AlertTask"] == "Record Successfully Saved" || TempData["AlertTask"] == "Record Successfully Updated")
            {
                return RedirectToAction("ManageSubscriber");
            }
            return View("AddSubscriber");
        }

        public ActionResult ManageSubscriber()
        {
            List<Subscriber> lst = new SubscribersModel().GetAllSubscriber();
            return View(lst);
        }

        public void Delete(int ID)
        {
            new SubscribersModel().Delete(ID);
            TempData["AlertTask"] = "Record Successfully Deleted";
            Response.Redirect("ManageSubscriber");
        }

        public ActionResult SendEmailToSubscriber()
        {
            List<Subscriber> lst = new SubscribersModel().GetAllSubscriber();
            ViewBag.Count = lst.Count();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendEmailToSubscriber(string Name, string EmailSubject, string Email, string EmailContent)
        {
            string Subject = EmailSubject;
            string Emailfrom = Email;
            string Title = Name;
            string EmailMessage = EmailContent;
            if (EmailContent != "" && EmailContent != null)
            {
                List<Subscriber> lst = new SubscribersModel().GetAllSubscriber();
                foreach (var item in lst)
                {
                    try
                    {
                        EmailSender.EmailSend(Subject, Emailfrom, Title, item.Email, EmailMessage);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                TempData["AlertTask"] = "Emails Successfully Sent to " + lst.Count() + "Subscribers";

                return RedirectToAction("DashboardIndex", "user");
            }
            else
            {
                TempData["AlertTask"] = "Email cannot sent! Please fill all fields";
                return View();
            }
        }
    }
}