using Silkways.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Silkways.Models
{
    public class OrderModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public List<Order> GetAllOrder()
        {
            
            return db.Orders.Where(x => x.IsDelete == false).OrderByDescending(x => x.Id).ToList();
        }

        public List<OrderShippingDetail> GetAllSippingOrder()
        {
            return db.OrderShippingDetails.OrderByDescending(x => x.ShippingDate).ToList();
        }

        public OrderShippingDetail GetSingleSippingOrder(int ordercode)
        {
            var Chapter2ordercode = ordercode.ToString();
            return db.OrderShippingDetails.Where(x=>x.OrderReferenceCode == Chapter2ordercode).FirstOrDefault();
        }

        public OrderShippingDetail GetSingleSippingOrderBYID(int ID)
        {
            return db.OrderShippingDetails.Where(x => x.ID == ID).FirstOrDefault();
        }


        public void DeleteOrder(int OrderID)
        {
            var item = db.Orders.Where(x => x.Id == OrderID).FirstOrDefault();
            item.IsDelete = true;
            db.SaveChanges();

        }
        public Order GetOrderByID(int ID)
        {
            return db.Orders.FirstOrDefault(x => x.Id == ID);
        }


        public List<OrderShippingDetail> GetOrderDetailList()
        {
            return db.OrderShippingDetails.ToList();
        }

                public async Task<OrdersDetail> GetOrderDetailByID(int ID)
        {
            var citylist = await OrderStaticData.CityListByBlueExApi;
            return await Task.Factory.StartNew(() =>
             {
                 Order order = db.Orders.FirstOrDefault(x => x.Id == ID);
                 OrdersDetail OrdersDetail = new OrdersDetail();
                 if (order != null)
                 {
                     OrdersDetail.cn_generate = OrderStaticData.cn_generate;
                     OrdersDetail.testbit = OrderStaticData.testbit;
                     OrdersDetail.userid = OrderStaticData.userid;
                     OrdersDetail.password = OrderStaticData.password;
                     OrdersDetail.acno = OrderStaticData.acno;
                     OrdersDetail.orderShippingDetail = new OrderShippingDetail();
                     OrdersDetail.orderShippingDetail.OrderReferenceCode = order.Id.ToString();
                     OrdersDetail.orderShippingDetail.CustomerName = order.tblMember.memberName;
                     OrdersDetail.orderShippingDetail.CustomerEmail = order.tblMember.email;
                     OrdersDetail.orderShippingDetail.CustomerContactNo = order.tblMember.phoneNo;
                     OrdersDetail.orderShippingDetail.CustomerAddress = order.Area + ", " + order.Address;
                     OrdersDetail.orderShippingDetail.CustomerCity = order.Address;

                     OrdersDetail.orderShippingDetail.CustomerCityCode = citylist.Where(x => x.city_name._0 == order.Address).FirstOrDefault().city_code._0;
                     if (order.PaymentMode.Name != "Cash on delivery")
                     {
                         OrdersDetail.orderShippingDetail.OrderPaymentType = "CC";
                     }

                     else
                     {
                         OrdersDetail.orderShippingDetail.OrderPaymentType = "COD";
                     }
                     OrdersDetail.orderShippingDetail.OrderShippingPrice = Math.Round(order.ShippingPrice, 0).ToString();
                     OrdersDetail.orderShippingDetail.ShippingOriginCity = "MUX";
                     OrdersDetail.orderShippingDetail.TotalOrderAmount = order.TotalPrice.ToString();


                     if (order.OrderDetails != null && order.OrderDetails.Count() > 0)
                     {
                         OrdersDetail.products_detail = new List<ShippingProductDetail>();
                         var orderedProductList = order.OrderDetails.ToList();
                         foreach (var item in orderedProductList)
                         {
                             ShippingProductDetail ShippingProductDetail = new ShippingProductDetail();
                             ShippingProductDetail.ProductCode = item.ProductId.ToString();
                             ShippingProductDetail.ProductName = item.Product.ProductName;
                             if (item.Product.TotalPrice != null && item.Product.TotalPrice > 0)
                             {
                                 decimal price = Math.Round(Convert.ToDecimal(item.Product.TotalPrice), 0);
                                 ShippingProductDetail.ProductPrice = price.ToString();
                             }
                             ShippingProductDetail.Productquantity = item.Qty.ToString();
                             ShippingProductDetail.ProductVariation = item.Product.ProductName;
                             ShippingProductDetail.SKUCode = item.Product.SKU;
                             OrdersDetail.products_detail.Add(ShippingProductDetail);
                         }
                     }
                     OrdersDetail.customer_comment = "Chapter2.pk coustomer product";
                     OrdersDetail.customer_country = "PK";
                 }
                 return OrdersDetail;
             });
                
        }

        public List<DliveryStatu> GetAllDeliveriesStatus()
        {
            return db.DliveryStatus.ToList();
        }
        public void UpdateOrder(int OrderID, byte StatusID)
        {
            Order order1 = db.Orders.FirstOrDefault(x => x.Id == OrderID);
            if (order1 != null)
            {
                order1.DliveryStatusId = StatusID;
                var order = new Order()
                {
                    Id = OrderID,
                    DliveryStatusId = StatusID
                };

                //db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                using (var dbMember = new GaintShoeBoxEntities())
                {
                    dbMember.Orders.Attach(order);
                    var update = dbMember.Entry(order);
                    update.Property(x => x.DliveryStatusId).IsModified = true;
                    dbMember.SaveChanges();
                }
            }

        }

        #region BlueEx Data

        public Task<bool> UpdateBlueexOrderStatus(long id,Response obj)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    OrderShippingDetail OrderShippingDetail = db.OrderShippingDetails.Where(x => x.ID == id).FirstOrDefault();
                    if (OrderShippingDetail != null)
                    {
                        OrderShippingDetail.Status = obj.message;
                        db.Entry<OrderShippingDetail>(OrderShippingDetail).State = EntityState.Modified;
                        db.SaveChangesAsync();
                        return true;
                    }
                }
                catch (Exception e)
                {

                    
                }
                
                return false;
            });
        }

        public Task<bool> AddOrderShippingDetail(OrderShippingDetail obj, List<ShippingProductDetail> ShippingProductList)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    if (obj != null)
                    {
                        var OrderShippingDetail = db.OrderShippingDetails.Add(obj);
                        db.SaveChanges();

                        if (ShippingProductList != null && ShippingProductList.Count() > 0)
                        {
                            foreach (var item in ShippingProductList)
                            {
                                ShippingProductDetail ShippingProductDetail = new ShippingProductDetail();
                                ShippingProductDetail.ProductCode = item.ProductCode;
                                ShippingProductDetail.ProductName = item.ProductName;
                                ShippingProductDetail.ProductPrice = item.ProductPrice;
                                ShippingProductDetail.Productquantity = item.Productquantity;
                                ShippingProductDetail.ProductVariation = item.ProductVariation;
                                ShippingProductDetail.ShippingOrderID = OrderShippingDetail.ID;
                                ShippingProductDetail.SKUCode = item.SKUCode;
                                db.ShippingProductDetails.Add(ShippingProductDetail);
                                db.SaveChanges();
                            }
                        }
                    }
                    return true;
                }
                catch (Exception e) { }

                return false;
            });
        }
        #endregion
    }
}