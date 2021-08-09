using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class PolicyCategoryModel
    {
        protected GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(PolicyCategory policyCategory)
        {
            try
            {

                db.PolicyCategories.Add(policyCategory);
                db.SaveChanges();


            }

            catch (Exception ex )
            {


            }


        }
        public void Update(PolicyCategory policyCategory)
        {
            db.PolicyCategories.Attach(policyCategory);
            var update = db.Entry(policyCategory);
            update.Property(x => x.CategoryName).IsModified = true;
            update.Property(x => x.SortNumber).IsModified = true;
            db.SaveChanges();
        }

        public List<PolicyCategory> GetAll()
        {
            return db.PolicyCategories.OrderByDescending(x => x.ID).ToList();
        }

        public PolicyCategory GetbyID(int ID)
        {
            return db.PolicyCategories.OrderByDescending(x => x.ID).Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int CategoryID)
        {
            var item = db.PolicyCategories.Where(x => x.ID == CategoryID).FirstOrDefault();
            db.PolicyCategories.Remove(item);
            db.SaveChanges();

        }

        //public List<GetParentChildCategories_Result> ParentsChildCategories()
        //{
        //    return db.GetParentChildCategories().ToList();
        //}
    }
}