using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class TestimonialModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(Testimonial sting)
        {
            try
            {
                db.Testimonials.Add(sting);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void Update(Testimonial sting)
        {
            try
            {
                db.Testimonials.Attach(sting);
                var Update = db.Entry(sting);
                Update.Property(x => x.Name).IsModified = true;
                Update.Property(x => x.Designation).IsModified = true;
                Update.Property(x => x.Comment).IsModified = true;
                Update.Property(x => x.SortOrder).IsModified = true;
                if (sting.Image != "" && sting.Image != null)
                {
                    Update.Property(x => x.Image).IsModified = true;
                }
              
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public List<Testimonial> GetAllTestimonials()
        {
            return db.Testimonials.OrderByDescending(x => x.ID).ToList();
        }

        public Testimonial GetTestimonialsByID(int ID)
        {
            return db.Testimonials.Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var item = db.Testimonials.Where(x => x.ID == ID).FirstOrDefault();
            db.Testimonials.Remove(item);
            db.SaveChanges();

        }
    }
}