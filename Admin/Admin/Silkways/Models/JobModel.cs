using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Silkways.Models;
using Silkways.ViewModels;

namespace Silkways.Models
{
    public class JobModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public bool Update(Job job)
        {
            try
            {
                db.Jobs.Attach(job);
                var Update = db.Entry(job);

                Update.Property(x => x.JobTitle).IsModified = true;
                Update.Property(x => x.Experience).IsModified = true;
                Update.Property(x => x.Active).IsModified = true;
                Update.Property(x => x.NoOfPeople).IsModified = true;
                Update.Property(x => x.Qualification).IsModified = true;
                Update.Property(x => x.Description).IsModified = true;
                Update.Property(x => x.DeadLine).IsModified = true;
         
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Job> GetAllJobs()
        {
            return db.Jobs.OrderByDescending(a => a.ID).ToList();
        }

        public bool Approve(int ID)
        {
            var Job = db.Jobs.FirstOrDefault(a => a.ID == ID);
            try
            {
                Job.Active = true; 
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(int ID)
        {
            try
            {
                var jobApplications = db.JobApplications.Where(m => m.JobID == ID).ToList();
                if (jobApplications.Count > 0)
                {
                    db.JobApplications.RemoveRange(jobApplications);
                    db.SaveChanges();
                }
                var item = db.Jobs.Where(x => x.ID == ID).FirstOrDefault();
                db.Jobs.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Job GetjobByID(int ID)
        {
            return db.Jobs.Where(x => x.ID == ID).FirstOrDefault();
        }
        public List<JobApplication> GetJobByApplicationsID(int ID)
        {
            List<JobApplication> data = db.JobApplications.Where(x => x.JobID == ID).ToList();
            return data;
        }

        public void SetShortList(int ID, int JobID)
        {
            var JobApplication = db.JobApplications.FirstOrDefault(a => a.ID == ID && a.JobID == JobID);
            try
            {
                JobApplication.IsShortListed = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}