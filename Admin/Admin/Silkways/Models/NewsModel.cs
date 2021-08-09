using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class NewsModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(News news)
        {
            try
            {
                db.News.Add(news);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void Update(News news)
        {
            try
            {
                db.News.Attach(news);
                var Update = db.Entry(news);
                Update.Property(x => x.Heading).IsModified = true;
                Update.Property(x => x.ShortDescription).IsModified = true;
                Update.Property(x => x.Description).IsModified = true;
                Update.Property(x => x.MetaTitle).IsModified = true;
                Update.Property(x => x.MetaKeyword).IsModified = true;
                Update.Property(x => x.MetaDescription).IsModified = true;
                Update.Property(x => x.DateTime).IsModified = true;
                Update.Property(x => x.Url).IsModified = true;


                if (news.Image != "NoImage.jpg")
                {
                    Update.Property(x => x.Thumbnail).IsModified = true;
                    Update.Property(x => x.Image).IsModified = true;
                    //Update.Property(x => x.Url).IsModified = true;
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateHomeImage(HomePageImage news)
        {
            try
            {
                db.HomePageImages.Attach(news);
                var Update = db.Entry(news);
                Update.Property(x => x.ImageName).IsModified = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public List<News> GetAllNews()
        {
            return db.News.OrderByDescending(x => x.ID).ToList();
        }

        public News GetNewsByID(int ID)
        {
            return db.News.Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            //var item = db.TechnologyProfiles.Where(x => x.ID == ID).FirstOrDefault();
            //db.TechnologyProfiles.Remove(item);
            //db.SaveChanges();


            var item = db.News.Where(x => x.ID == ID).FirstOrDefault();
            db.News.Remove(item);
            db.SaveChanges();


        }
    }
}