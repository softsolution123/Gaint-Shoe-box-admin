using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class PromoModel
    {
        protected GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(tblPromoCode PromoCode)
        {
            db.tblPromoCodes.Add(PromoCode);
            db.SaveChanges();
        }
        public void Update(tblPromoCode PromoCode)
        {
            db.tblPromoCodes.Attach(PromoCode);
            var update = db.Entry(PromoCode);
            update.Property(x => x.IsActive).IsModified = true;
            update.Property(x => x.PromoCodeName).IsModified = true;
            update.Property(x => x.DiscountPercentage).IsModified = true;
            db.SaveChanges();
        }

        public List<tblPromoCode> GetPromoCodes()
        {
            return db.tblPromoCodes.OrderByDescending(x => x.ID).ToList();
        }


        public void Delete(int PromoCodeID)
        {
            var item = db.tblPromoCodes.Where(x => x.ID == PromoCodeID).FirstOrDefault();
            db.tblPromoCodes.Remove(item);
            db.SaveChanges();

        }


        public tblPromoCode GetbyID(int PromoCodeID)
        {
            var item = db.tblPromoCodes.Where(x => x.ID == PromoCodeID).FirstOrDefault();
            return item;

        }

      

      

    }
}