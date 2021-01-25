using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryPrj.Models
{
    public class LoginBL
    {
        FactoryEntities1 db = new FactoryEntities1();

        public bool IsAuthenticated(string usrname, string pwd)
        {
            var result = db.Users.Where(x => x.UserName == usrname && x.Password == pwd);
            var user = db.Users.Where(x => x.UserName == usrname).First();

            if (result.Count() == 0)
            {
                return false;
            }
            else
            {
                if (user.Date != DateTime.Today)
                {
                    user.NumOfActions = 0;
                    db.SaveChanges();
               }
                return true;
            }
        }
        public bool IsCrossedLImitPerDay(string usrname)
        {
            var user = db.Users.Where(x => x.UserName == usrname).First();
            if (user.Date == DateTime.Today)
            {
                
                if (user.NumOfActions <= user.Credits)
                {
                    user.NumOfActions++;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                user.NumOfActions++;
                user.Date = DateTime.Now;
                db.SaveChanges();
                return true;
            }

        }


        public User getUser(string usrname)
        {
            return db.Users.Where(x => x.UserName == usrname).First();
        }

    }
}