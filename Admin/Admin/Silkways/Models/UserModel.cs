using Silkways.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class UserModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            catch (Exception ex)
            { 
            
            }
        }

        public void Update(User user)
        {
            try
            {
                db.Users.Attach(user);
                var Update = db.Entry(user);
                Update.Property(x => x.FullName).IsModified = true;
                Update.Property(x => x.UserName).IsModified = true;
                Update.Property(x => x.Email).IsModified = true;
                Update.Property(x => x.Password).IsModified = true;
                Update.Property(x => x.Address).IsModified = true;
                Update.Property(x => x.ContactNo).IsModified = true;
                if (user.Image != "NoImage.jpg")
                {
                    Update.Property(x => x.Image).IsModified = true;
                }
                Update.Property(x => x.IsActive).IsModified = true;

                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public List<User> GetAllUser()
        {
            return db.Users.OrderByDescending(x => x.ID).ToList();
        }

        public User GetUserByID(int UserID)
        {
            User user = db.Users.Where(x => x.ID == UserID).FirstOrDefault();
            return user;
        }

        public User GetUserByEmail(string Email)
        {
            return db.Users.Where(x => x.Email == Email).FirstOrDefault();
        }
        public void ChangePass(User user)
        {
            try
            {
                db.Users.Attach(user);
                var Update = db.Entry(user);
                Update.Property(x => x.Password).IsModified = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public User GetUserByEmailandPassword(string username, string Password)
        {
            return db.Users.Where(x => x.UserName == username && x.Password == Password).FirstOrDefault();
        }
        public User GetMasterAdmin()
        {
            return db.Users.Where(x => x.IsMasterAdmin).FirstOrDefault();
        }

        public bool UsernameExist(string Username)
        {
            bool ExistUsername = true;
            var item = db.Users.Where(x => x.UserName == Username).FirstOrDefault();
            if (item != null)
            {
                ExistUsername = false;
                return ExistUsername;
            }
            else
            {
                return ExistUsername;
            }

        }


        public bool EmailExist(string Email)
        {
            bool ExistEmail = true;
            var item = db.Users.Where(x => x.Email == Email).FirstOrDefault();
            if (item != null)
            {
                ExistEmail = false;
                return ExistEmail;
            }
            else
            {
                return ExistEmail;
            }

        }

        public void DeleteUser(int ? UserID)
        {
            var item = db.Users.Where(x => x.ID == UserID).FirstOrDefault();
            db.Users.Remove(item);
            db.SaveChanges();

        }
        

        public bool IsUserExists(int ID)
        {
            User item = db.Users.Where(x => x.ID == ID).FirstOrDefault();
            bool Exists = false;
            if (item != null)
            {
                Exists = true;
            }
            return Exists;
        }
    }
}