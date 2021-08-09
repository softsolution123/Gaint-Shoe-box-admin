using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class MemberModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public tblMember GetMemberByID(int? MemberID)
        {
            return db.tblMembers.FirstOrDefault(x => x.memberId == MemberID);
        }
    }
}