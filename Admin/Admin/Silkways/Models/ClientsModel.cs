using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class ClientsModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(Client sting)
        {
            try
            {
                db.Clients.Add(sting);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void Update(Client sting)
        {
            try
            {
                db.Clients.Attach(sting);
                var Update = db.Entry(sting);
                Update.Property(x => x.Name).IsModified = true;
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

        public List<Client> GetAllClients()
        {
            return db.Clients.OrderByDescending(x => x.ID).ToList();
        }

        public Client GetClientsByID(int ID)
        {
            return db.Clients.Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var item = db.Clients.Where(x => x.ID == ID).FirstOrDefault();
            db.Clients.Remove(item);
            db.SaveChanges();

        }
    }
}