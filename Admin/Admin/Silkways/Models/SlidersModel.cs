using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class SlidersModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public void Save( tblSlider slider )
        {
            slider.IsActive = true;
            db.tblSliders.Add( slider );
            db.SaveChanges();
        }

        public void Update( tblSlider slider )
        {

            slider.IsActive = true;
            db.tblSliders.Attach( slider );
            var Update = db.Entry( slider );
            Update.Property( x => x.Created_Date ).IsModified = true;
            Update.Property( x => x.SortOrder ).IsModified = true;
            Update.Property( x => x.Title ).IsModified = true;
            Update.Property( x => x.SubHeading ).IsModified = true;
            Update.Property( x => x.SubHeadingTwo ).IsModified = true;
            Update.Property( x => x.Link ).IsModified = true;
            Update.Property( x => x.IsActive ).IsModified = true;
            Update.Property(x => x.IsExclusive).IsModified = true;
            Update.Property(x => x.Description).IsModified = true;
            if (slider.Image != null)
            {
                Update.Property(x => x.Image).IsModified = true;
            }
            db.SaveChanges();
        }

        public tblSlider GetByID( int ID )
        {
            return db.tblSliders.Where( x => x.ID == ID  ).FirstOrDefault();
        }

        public List<tblSlider> GetAll()
        {
            return db.tblSliders.OrderByDescending( x => x.ID ).ToList();
        }

        public void Delete( int ID )
        {
            var item = db.tblSliders.Where( x => x.ID == ID ).FirstOrDefault();
            db.tblSliders.Remove( item );
            db.SaveChanges();
        }
        
    }
}