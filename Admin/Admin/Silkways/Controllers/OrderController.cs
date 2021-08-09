using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Silkways.Models;
using Silkways.ViewModels;
using Silkways.Models.SilkwaysFunction;
using System.IO;
using System.Threading.Tasks;

namespace Silkways.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            List<Order> order = new OrderModel().GetAllOrder();
            return View(order);
        }

        public ActionResult ShippingOrders()
        {
            return View(new OrderModel().GetAllSippingOrder());
        }

        public async Task<ActionResult> CancelBlueexOrder(int ID)
        {
            var BlueexOrderDetail = new OrderModel().GetSingleSippingOrderBYID(ID);
            bool check = false; 
            if (BlueexOrderDetail != null)
            {
                check = true;
                var Responce = await OrderStaticData.CacelBlueExOrder(BlueexOrderDetail.ID, BlueexOrderDetail.CNCode);
            }
            if (check)
            {
                TempData["AlertTask"] = "Blue-ex order Cancelled successfully";
            }
            else
            {
                TempData["AlertTask"] = "Blue-ex order not Cancel but Mail is sended to blue-ex.com. Order Cancel Fast as possible";
            }
            return Redirect("/order/index");
        }

        [HttpGet]
        public async Task<ActionResult> AddBlueExOrder(int ID)
        {
            OrderStaticData OrderStaticData = new OrderStaticData();
            var check = await OrderStaticData.AddBlueExOrder(ID);
            if (check)
            {
                TempData["AlertTask"] = "Blue-ex order placed and saved successfully";
            }
            else
            {
                TempData["AlertTask"] = "Blue-ex order not placed and not saved successfully";
            }
            return Redirect("/order/index");
        }

        public async Task<ActionResult> Details(int ID)
        {
            Order order = new OrderModel().GetOrderByID(ID);
            var Delivery = new OrderModel().GetAllDeliveriesStatus();
            var BlueexOrderDetail = new OrderModel().GetSingleSippingOrder(ID);
            if (BlueexOrderDetail != null)
            {
                var status = await OrderStaticData.AddBlueExOrderStatus(BlueexOrderDetail.ID, BlueexOrderDetail.OrderReferenceCode);
                ViewBag.BlueexOrderStatus = status.message;
            }
            
            ViewBag.DeliveryStatus = Delivery;
            ViewBag.BlueexOrderDetail = BlueexOrderDetail;
            
            return View(order);
        }
        public void DeleteOrder(int ID)
        {
            new OrderModel().DeleteOrder(ID);
            TempData["AlertTask"] = "Record Successfully Deleted";
            Response.Redirect("Index");
        }
    }
}