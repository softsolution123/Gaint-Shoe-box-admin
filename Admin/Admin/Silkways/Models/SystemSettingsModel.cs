using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class SystemSettingsModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(SystemSetting sting)
        {
            try
            {
                db.SystemSettings.Add(sting);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void Update(SystemSetting sting)
        {
            try
            {
                db.SystemSettings.Attach(sting);
                var Update = db.Entry(sting);
                Update.Property(x => x.Email).IsModified = true;
                Update.Property(x => x.FTPIP).IsModified = true;
                Update.Property(x => x.FtpName).IsModified = true;
                Update.Property(x => x.FtpPassword).IsModified = true;
                Update.Property(x => x.FTPPath).IsModified = true;
                Update.Property(x => x.IsSSL).IsModified = true;
                Update.Property(x => x.Port).IsModified = true;
                Update.Property(x => x.SenderEmail).IsModified = true;
                Update.Property(x => x.SenderPassword).IsModified = true;
                Update.Property(x => x.SMTPHost).IsModified = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public List<SystemSetting> GetAllSystemSettings()
        {
            return db.SystemSettings.OrderByDescending(x => x.ID).ToList();
        }
        public SystemSetting GetDefaultSystemSettings()
        {
            return db.SystemSettings.FirstOrDefault();
        }
        public SystemSetting GetSystemSettingsByID(int ID)
        {
            return db.SystemSettings.Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var item = db.SystemSettings.Where(x => x.ID == ID).FirstOrDefault();
            db.SystemSettings.Remove(item);
            db.SaveChanges();

        }
    }
}