using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Silkways.Models;
using Silkways.ViewModels;
using Silkways.Models.SilkwaysFunction;

namespace Silkways
{
    /// <summary>
    /// Summary description for PlugNPointService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PlugNPointService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string ChangeOrderStatus(int OrderID, byte StatusID)
        {
            //Order order = new OrderModel().GetOrderByID(OrderID);
            string Success;
            try
            {
                string Email = "";
                string Subject = "Order Status Notification";
                new OrderModel().UpdateOrder(OrderID, StatusID);
                Order order = new OrderModel().GetOrderByID(OrderID);
                Success = order.DliveryStatu.Name;
                //tblMember Mem = new MemberModel().GetMemberByID(order.MemberId);

                //string bodyHTML = GernalFunction.GetHmtlContentsFromDirectory("/EmailsHtml/basicEmail.html");
                //bodyHTML = bodyHTML.Replace("@Status", Success);
                ////bodyHTML = bodyHTML.Replace("@Name", Mem.memberName);
                //Email = Mem.email;
                //GernalFunction.SendEmailSMPT(Email, bodyHTML, Subject);
            }
            catch
            {
                Success = "Fail";
            }
            return Success;

        }
        
    }
}
