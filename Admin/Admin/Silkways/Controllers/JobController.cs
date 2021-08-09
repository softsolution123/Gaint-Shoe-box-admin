using Silkways.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Silkways.Controllers
{
    public class JobController : Controller
    {
        // GET: Job
        
        public ActionResult ManageJobs()
        {
            List<Job> jobs = new JobModel().GetAllJobs();
            return View(jobs);
        }

        public ActionResult Update()
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                var job = new JobModel().GetjobByID(ID);
                return View(job);
            }
            else
            {
                Job Edit = new Job();
                Edit.DeadLine = DateTime.Now;
                return View(Edit);
               
            }
           
          
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Job job)
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                bool IsUpdated = new JobModel().Update(job);
                TempData["AlertTask"] = "Record Successfully Update";
                if (IsUpdated)
                {
                    TempData["AlertTask"] = "Record Updated Successfully";
                }
                else
                {
                    TempData["AlertTask"] = "An Error Occured";
                }
            }
            else
            {

                GaintShoeBoxEntities db = new GaintShoeBoxEntities();
                job.date = DateTime.Now;
                db.Jobs.Add(job);
                db.SaveChanges();
                TempData["AlertTask"] = "Record Successfully Saved";
            }
           
           
            return RedirectToAction(nameof(JobController.ManageJobs));
        }
        public ActionResult Delete(int ID)
        {
            bool IsDelete = new JobModel().Delete(ID);
            if (IsDelete)
            {
                TempData["AlertTask"] = "Record Deleted Successfully";
            }
            else
            {
                TempData["AlertTask"] = "An Error Occured";
            }
            return RedirectToAction("ManageJobs");
        }
        public ActionResult Approve(int ID)
        {
            bool IsApproved = new JobModel().Approve(ID);
            if (IsApproved)
            {
                TempData["AlertTask"] = "Approves Successfully";
            }
            else
            {
                TempData["AlertTask"] = "An Error Occured";
            }
            return RedirectToAction("ManageJobs");
        }
        public ActionResult ManageJobApplication()
        {
            return View();
        }
        public ActionResult Resumes(int ID)
        {
            ViewBag.Job = new JobModel().GetjobByID(ID);
            List<JobApplication> lst = new List<JobApplication>();
            lst = new JobModel().GetJobByApplicationsID(ID);
            return View(lst);
        }
        public ActionResult ShortList(int ID, int JobID)
        {
            new JobModel().SetShortList(ID, JobID);
            return Redirect("/Job/Resumes?ID=" + JobID);
        }
    }
}