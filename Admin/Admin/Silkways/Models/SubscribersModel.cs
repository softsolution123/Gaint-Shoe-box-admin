using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class SubscribersModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public string Save(Subscriber sub)
        {
            try
            {
                if (db.Subscribers.Where(x => x.Email==sub.Email).Count()>0)
                {
                    return "This email already exists";
                }
                else
                {
                    db.Subscribers.Add(sub);
                    db.SaveChanges();
                    return "Record Successfully Saved";
                }
            }
            catch (Exception ex)
            {
                return "error in saving data";
            }
        }

        public string Update(Subscriber sub)
        {
            try
            {
                if (db.Subscribers.Where(x => x.Email == sub.Email).Count() > 0)
                {
                    return "This email already exists";
                }
                else
                {
                    db.Subscribers.Attach(sub);
                    var Update = db.Entry(sub);
                    Update.Property(x => x.Email).IsModified = true;

                    db.SaveChanges();
                    return "Record Successfully Updated";
                }
            }
            catch (Exception ex)
            {
                return "error in saving data !";

            }
        }

        public List<Subscriber> GetAllSubscriber()
        {
            return db.Subscribers.OrderByDescending(x => x.ID).ToList();
        }

        public Subscriber GetSubscriberByID(int ID)
        {
            return db.Subscribers.Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var item = db.Subscribers.Where(x => x.ID == ID).FirstOrDefault();
            db.Subscribers.Remove(item);
            db.SaveChanges();

        }
    }
}