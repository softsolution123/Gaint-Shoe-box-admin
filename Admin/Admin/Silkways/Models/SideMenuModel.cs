using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class SideMenuModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public List<PanelMenu> GetMenuUserWise(int? MemberID)
        {
            List<PanelMenu> ListMenus = db.PanelMenuUserRelations.Where(a => a.UserID == MemberID).Select(a => a.PanelMenu).Distinct().ToList();
            return ListMenus;
        }
    }
}