using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class FAQsModel
    {

        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(FAQ faq)
        {
            try
            {
                db.FAQs.Add(faq);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void Update(FAQ faq)
        {
            try
            {
                db.FAQs.Attach(faq);
                var Update = db.Entry(faq);
                Update.Property(x => x.Title).IsModified = true;
                Update.Property(x => x.Decsription).IsModified = true;
                Update.Property(x => x.SortOrder).IsModified = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public List<FAQ> GetAllFaqs()
        {
            return db.FAQs.OrderByDescending(x => x.ID).ToList();
        }

        public FAQ GetfaqsByID(int ID)
        {
            return db.FAQs.Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var item = db.FAQs.Where(x => x.ID == ID).FirstOrDefault();
            db.FAQs.Remove(item);
            db.SaveChanges();

        }
    }
}