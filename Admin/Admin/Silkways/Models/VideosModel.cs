using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class VideosModel
    {

        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public void Save( tblVideo vid )
        {
            db.tblVideos.Add( vid );
            db.SaveChanges();
        }

        public void Update( tblVideo vid )
        {
            db.tblVideos.Attach( vid );
            var Update = db.Entry( vid );
            Update.Property( x => x.Name ).IsModified = true;
            Update.Property( x => x.VideoUrl ).IsModified = true;
            Update.Property( x => x.MetaTitle ).IsModified = true;
            Update.Property( x => x.MetaKeyword ).IsModified = true;
            Update.Property( x => x.MetaDesc ).IsModified = true;
            Update.Property( x => x.Url ).IsModified = true;
            Update.Property( x => x.IsTop ).IsModified = true;
            Update.Property( x => x.IsActive ).IsModified = true;
            Update.Property( x => x.Detail ).IsModified = true;
            Update.Property( x => x.AltTag ).IsModified = true;
            Update.Property( x => x.Dated ).IsModified = true;
            db.SaveChanges();
        }

        public tblVideo GetByID( int ID )
        {
            return db.tblVideos.Where( x => x.ID == ID && x.IsDeleted == false ).FirstOrDefault();
        }

        public List<tblVideo> GetAll()
        {
            return db.tblVideos.Where( x => x.IsDeleted == false ).OrderByDescending( x => x.ID ).ToList();
        }

        public void Delete( int ID )
        {
            var item = db.tblVideos.Where( x => x.ID == ID ).FirstOrDefault();
            db.tblVideos.Remove( item );
            db.SaveChanges();
        }
    }
}