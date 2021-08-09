using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Silkways.Models
{
    public class TechnologyProfileModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(TechnologyProfile profile)
        {
            try
            {
                db.TechnologyProfiles.Add(profile);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void Update(TechnologyProfile profile)
        {
            try
            {
                db.TechnologyProfiles.Attach(profile);
                var Update = db.Entry(profile);
                Update.Property(x => x.AffiliationName).IsModified = true;
                Update.Property(x => x.Name).IsModified = true;
                Update.Property(x => x.EntityName).IsModified = true;
                Update.Property(x => x.Qualification).IsModified = true;
                Update.Property(x => x.AffiliationName).IsModified = true;
                Update.Property(x => x.Nationality).IsModified = true;  
                Update.Property(x => x.ContactDetails).IsModified = true;
                Update.Property(x => x.ContactDetailsOfTechnicalResources).IsModified = true;
                Update.Property(x => x.TechnologyName).IsModified = true;
                Update.Property(x => x.Synopsis).IsModified = true;
                Update.Property(x => x.PrototypeAvailable).IsModified = true;
                Update.Property(x => x.Purpose_of_Technology).IsModified = true;
                Update.Property(x => x.TechnologyTargets).IsModified = true;
                Update.Property(x => x.DevelopedYear).IsModified = true;
                Update.Property(x => x.ApprovalsRequired).IsModified = true;
                Update.Property(x => x.OtherApprovalsRequired).IsModified = true;
                Update.Property(x => x.PotentialBenefiaciaries).IsModified = true;
                Update.Property(x => x.EndUserOfTechnology).IsModified = true;


                Update.Property(x => x.BenefitsOfTechnology).IsModified = true;
                Update.Property(x => x.BenefitsOfTechnology2).IsModified = true;
                Update.Property(x => x.ReserchCentreToDevelopTechnology).IsModified = true;
                Update.Property(x => x.FacilityAvailability).IsModified = true;
                Update.Property(x => x.RawMaterialAvailability).IsModified = true;
                Update.Property(x => x.ResourcesToEnablePilotPro).IsModified = true;
                Update.Property(x => x.ProductionCapacity).IsModified = true;



                Update.Property(x => x.ProductionCost).IsModified = true;
                Update.Property(x => x.OneUnitCost).IsModified = true;
                Update.Property(x => x.TechnologySold).IsModified = true;
                Update.Property(x => x.SalePrice).IsModified = true;
                Update.Property(x => x.competingProductsSalesMarketing).IsModified = true;
                Update.Property(x => x.UniqueSellingPointSalesMarketing).IsModified = true;
                Update.Property(x => x.HowGreen).IsModified = true;



                Update.Property(x => x.SustainableProducts).IsModified = true;
                Update.Property(x => x.CompetingProductsSustainability).IsModified = true;
                Update.Property(x => x.UniqueSellingPointSustainability).IsModified = true;
                Update.Property(x => x.PerceivedImpactOnLocalEconomy).IsModified = true;
                Update.Property(x => x.PerceivedImpactOnServiceDelivery).IsModified = true;
                Update.Property(x => x.PerceivedImpactOnEmploymentGeneration).IsModified = true;
                Update.Property(x => x.PerceivedImpactOnEnvironment).IsModified = true;



                Update.Property(x => x.Society).IsModified = true;
                Update.Property(x => x.AttachAnnexures).IsModified = true;
                Update.Property(x => x.EstamatedImpact).IsModified = true;
                Update.Property(x => x.Supportexpectation).IsModified = true;






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
        public List<TechnologyProfile> GetAllProfiles()
        {
            return db.TechnologyProfiles.OrderByDescending(x => x.ID).ToList();
        }

        public TechnologyProfile GetProfileByID(int ID)
        {
            return db.TechnologyProfiles.Include(x=>x.ProfileVideos).Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var item = db.News.Where(x => x.ID == ID).FirstOrDefault();
            db.News.Remove(item);
            db.SaveChanges();

        }
    }
}