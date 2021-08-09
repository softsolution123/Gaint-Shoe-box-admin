using Silkways.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using Silkways.Models.SilkwaysFunction;

namespace Silkways.ViewModels
{
    public class Response
    {
        public string status { get; set; }
        public string success { get; set; }
        public string order_refernce_code { get; set; }
        public string order_code { get; set; }
        public string message { get; set; }
        public string msg { get; set; }
        public string Error_message { get; set; }
        public string cn { get; set; }
    }


    public class Links
    {
        public string order { get { return "https://bigazure.com/api/demo/json/serverjson.php"; } }
        public string status { get { return "https://bigazure.com/api/demo/json/status/serverjson.php"; } }
        public string citylink { get { return "https://bigazure.com/api/demo/json/cities/serverjson.php"; } }
        public string tarifflink { get { return "http://113.203.238.11/customerportal/calculatetarrif.py"; } }
        public string trackinglink { get { return "https://bigazure.com/api/demo/json/tracking/serverjson.php"; } }
        public string CancelOrderlink { get { return "https://bigazure.com/api/live/json/cancel/serverjson.php"; } }
    }
    public class testbit
    {
        public string Yes { get { return "y"; } }
        public string No { get { return "n"; } }

    }
    public class cn_generate
    {
        public string Yes { get { return "y"; } }
        public string No { get { return "n"; } }

    }


    public class RequestMethods
    {
        public string Post { get { return "POST"; } }
        public string Get { get { return "GET"; } }
    }

    public class OrdersDetail
    {
        public string acno { get; set; }
        public string testbit { get; set; }
        public string userid { get; set; }
        public string password { get; set; }
        public string cn_generate { get; set; }
        public string customer_country { get; set; }
        public string customer_comment { get; set; }
        public OrderShippingDetail orderShippingDetail { get; set; }
        public List<ShippingProductDetail> products_detail { get; set; }
    }

    public class OrderStaticData
    {
        public static string acno { get { return "MUX-00038"; } }
        public static string API_KEY { get { return "ehkjshhtgyjku15"; } }
        //public static string acno { get { return "LHE-00001"; } }
        public static string userid { get { return "chapter2"; } }
        public static string password { get { return "Casper84"; } }
        public string order_refernce_code { get; set; }
        public static string cn_generate { get { return "y"; } }
        public static string testbit { get { return "y"; } }
        public static Links Links { get { return new Links(); } }
        public static RequestMethods RequestMethods { get { return new RequestMethods(); } }
        static List<_0> _CityListByBlueExApi;



        #region BlueEx Api use function and properties

        #region BlueEx Api Properties

        public string MyProperty { get; set; }
        public static Task<List<_0>> CityListByBlueExApi
        {
            get
            {
                return Task.Factory.StartNew(() =>
                {
                    if (_CityListByBlueExApi != null && _CityListByBlueExApi.Count() > 0)
                    {
                        return _CityListByBlueExApi;
                    }
                    else
                    {
                        _CityListByBlueExApi = GetCityListByBlueEx();
                        return _CityListByBlueExApi;
                    }
                });
            }
        }

        #endregion

        #region BlueEx Api Functions

        public static List<_0> GetCityListByBlueEx()
        {
            string uriString = string.Format(OrderStaticData.Links.citylink + "?acno={0}", OrderStaticData.acno);

            System.Uri address = new System.Uri(uriString);
            System.Net.WebClient client = new System.Net.WebClient();
            try
            {
                string tinyUrl = client.DownloadString(address);
                if (tinyUrl != null && tinyUrl != string.Empty)
                {
                    CityListResponce Cities = JsonConvert.DeserializeObject<CityListResponce>(tinyUrl.Replace("0", "_0"));
                    if (Cities != null && Cities._0 != null && Cities._0.Count() > 0)
                    {
                        List<_0> Citylist = Cities._0.ToList();
                        if (Citylist != null && Citylist.Count() > 0)
                        {
                            return Citylist;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return new List<_0>();

        }



        public async Task<bool> AddOrderShippingDetail(OrdersDetail obj)
        {

            if (obj != null)
            {
                if (obj.orderShippingDetail != null && obj.products_detail != null && obj.products_detail.Count() > 0)
                {
                    var v = await new OrderModel().AddOrderShippingDetail(obj.orderShippingDetail, obj.products_detail);
                    return v;
                }
            }
            return false;

        }

        public static async Task<Response> AddBlueExOrderStatus(long BlueexOrderID, string BlueexOrederNumber)
        {
            Response Response = new Response();
            BlueexStatus BlueexStatus = new BlueexStatus();
            BlueexStatus.acno = OrderStaticData.acno;
            BlueexStatus.userid = OrderStaticData.userid;
            BlueexStatus.password = OrderStaticData.password;
            BlueexStatus.order_refernce_code = BlueexOrederNumber;
            String strPost = JsonConvert.SerializeObject(BlueexStatus);
            if (strPost != null && strPost != string.Empty)
            {
                strPost = "request=" + strPost;
            }
            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(OrderStaticData.Links.status);
            objRequest.Method = "POST"; objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost);
            //objRequest.ContentType = "application/json";
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myWriter.Close();
            }

            try
            {
                using (HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                    {
                        var a = sr.ReadToEnd(); a = a.Replace("null", "0");
                        sr.Close();

                        if (a != null && a != string.Empty)
                        {
                            Response = JsonConvert.DeserializeObject<Response>(a);
                            if (Response != null && Response.status != "0")
                            {
                                var check = await new OrderModel().UpdateBlueexOrderStatus(BlueexOrderID, Response);
                                if (check)
                                {
                                    return Response;
                                }
                            }
                        }
                    }
                }
            }
            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                }
            }
            catch (System.Web.Services.Protocols.SoapException se) { }
            return Response;
        }

        public static async Task<Response> CacelBlueExOrder(long BlueexOrderID, string BlueexOrederNumber)
        {
            Response Response = new Response();
            CancelOrderRequest CancelOrderRequest = new CancelOrderRequest();
            CancelOrderRequest.acno = OrderStaticData.acno;
            CancelOrderRequest.userid = OrderStaticData.userid;
            CancelOrderRequest.password = OrderStaticData.password;
            CancelOrderRequest.consignee_number = BlueexOrederNumber;
            String strPost = JsonConvert.SerializeObject(CancelOrderRequest);
            if (strPost != null && strPost != string.Empty)
            {
                strPost = "request=" + strPost;
            }
            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(OrderStaticData.Links.CancelOrderlink);
            objRequest.Method = "POST"; objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost);
            //objRequest.ContentType = "application/json";
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myWriter.Close();
            }

            try
            {
                using (HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                    {
                        var a = sr.ReadToEnd(); a = a.Replace("null", "0");
                        sr.Close();

                        if (a != null && a != string.Empty)
                        {
                            Response = JsonConvert.DeserializeObject<Response>(a);
                            if (Response != null && Response.status != "0")
                            {
                                Response.message = "Cancelled";
                                var check = await new OrderModel().UpdateBlueexOrderStatus(BlueexOrderID, Response);
                                if (check)
                                {
                                    SendMailForCancelOrders(BlueexOrederNumber);
                                    return Response;
                                }
                            }
                            else
                            {
                                SendMailForCancelOrders(BlueexOrederNumber);
                            }
                        }
                    }
                }
            }
            catch (WebException webex)
            {
                SendMailForCancelOrders(BlueexOrederNumber);

                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                }
            }
            catch (System.Web.Services.Protocols.SoapException se) {
                SendMailForCancelOrders(BlueexOrederNumber);
            }
            return Response;
        }


        public static void SendMailForCancelOrders(string cn)
        {
            var bodyHTML = GernalFunction.GetHmtlContentsFromDirectory("~/EmailsHtml/basicEmail.html");
            var bodyHTML1 = GernalFunction.GetHmtlContentsFromDirectory("~/EmailsHtml/basicEmail.html");
            bodyHTML = bodyHTML.Replace("@Name", "Blue-ex");
            var text = "Please cancel Chapter2.pk order. Order consignee number is " + cn;
            bodyHTML = bodyHTML.Replace("We look forward to your comments and future concerns", text);
            var Subject = "Cancel Order";
            var Email = "asif.hanif@blue-ex.com";
            GernalFunction.SendEmailSMPT(Email, bodyHTML, Subject);
            bodyHTML1 = bodyHTML1.Replace("@Name", "Blue-ex");
            bodyHTML1 = bodyHTML1.Replace("We look forward to your comments and future concerns", "your order cancel email is send to asif.hanif@blue-ex.com. <br> order consignee number is " + cn);
            //var Chapter2Email = "dvel.mode@gmail.com";
            var Chapter2Email = "info@chapter2.pk";
            GernalFunction.SendEmailSMPT(Chapter2Email, bodyHTML1, Subject);
        }
        static List<OrderShippingDetail> _ShippingOrderDetailList;
        public static  List<OrderShippingDetail> ShippingOrderDetailList
        {
            get
            {
                _ShippingOrderDetailList =  new OrderModel().GetOrderDetailList();
                return _ShippingOrderDetailList;
            }
            
        }


        public async Task<bool> AddBlueExOrder(int orederID)
        {
            OrdersDetail obj = await new OrderModel().GetOrderDetailByID(orederID);
            orderRequest orderRequest = new orderRequest();
            if (obj != null)
            {
                orderRequest.acno = obj.acno;
                orderRequest.testbit = obj.testbit;
                orderRequest.userid = obj.userid;
                orderRequest.password = obj.password;
                orderRequest.cn_generate = obj.cn_generate;
                orderRequest.customer_name = obj.orderShippingDetail.CustomerName;
                orderRequest.customer_email = obj.orderShippingDetail.CustomerEmail;
                orderRequest.customer_contact = obj.orderShippingDetail.CustomerContactNo.Replace(" ","");
                orderRequest.customer_address = obj.orderShippingDetail.CustomerAddress;
                orderRequest.customer_city = obj.orderShippingDetail.CustomerCityCode;
                orderRequest.customer_country = "PK";
                orderRequest.customer_comment = "Chapter2.pk coustomer product";
                orderRequest.shipping_charges = obj.orderShippingDetail.OrderShippingPrice;
                orderRequest.payment_type = obj.orderShippingDetail.OrderPaymentType;
                orderRequest.shipper_origion_city = obj.orderShippingDetail.ShippingOriginCity;
                orderRequest.total_order_amount = obj.orderShippingDetail.TotalOrderAmount;
                orderRequest.order_refernce_code = obj.orderShippingDetail.OrderReferenceCode;
                if (obj.products_detail != null && obj.products_detail.Count() > 0)
                {
                    List<Products_Detail> Products_DetailList = new List<Products_Detail>();
                    foreach (var item in obj.products_detail)
                    {
                        Products_Detail Products_Detail = new Products_Detail();
                        Products_Detail.product_code = item.ProductCode;
                        Products_Detail.product_name = item.ProductName;
                        Products_Detail.product_price = item.ProductPrice;
                        Products_Detail.product_quantity = item.Productquantity;
                        Products_Detail.product_variations = item.ProductVariation;
                        Products_Detail.sku_code = item.SKUCode;
                        Products_DetailList.Add(Products_Detail);
                    }
                    orderRequest.products_detail = Products_DetailList.ToArray();
                }
                String strPost = JsonConvert.SerializeObject(orderRequest);
                if (strPost != null && strPost != string.Empty)
                {
                    strPost = "request=" + strPost;
                }

                StreamWriter myWriter = null;
                HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(OrderStaticData.Links.order);
                //objRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
                objRequest.Method = "POST"; objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost);
                //objRequest.ContentType = "application/json";
                objRequest.ContentType = "application/x-www-form-urlencoded";
                try
                {
                    myWriter = new StreamWriter(objRequest.GetRequestStream());
                    myWriter.Write(strPost);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    myWriter.Close();
                }


                try
                {
                    using (HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse())
                    {
                        using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                        {
                            var a = sr.ReadToEnd(); a = a.Replace("null", "0");
                            sr.Close();

                            if (a != null && a != string.Empty)
                            {

                                Response Response = JsonConvert.DeserializeObject<Response>(a);
                                if (Response != null && Response.status != "0")
                                {
                                    obj.orderShippingDetail.CNCode = Response.cn;
                                    obj.orderShippingDetail.BlueExOrderCode = Response.order_code;
                                    obj.orderShippingDetail.Status = "Pending";
                                    obj.orderShippingDetail.ShippingDate = DateTime.Now;
                                    var check = await AddOrderShippingDetail(obj);
                                    return check;
                                }
                                else
                                    return false;
                            }

                        }
                    }

                }
                catch (WebException webex)
                {
                    WebResponse errResp = webex.Response;
                    using (Stream respStream = errResp.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(respStream);
                        string text = reader.ReadToEnd();
                    }

                }
                catch (System.Web.Services.Protocols.SoapException se)
                {


                }
            }

            return false;


        }





        /*public decimal GetTariffPrice(string Acno,string Origin,string Destination, string service, string wgt)
       {
           String strPost = null;
           strPost = "Row=" + Convert.ToString(RowId) + "&Top=22&CategoryID=-1&LevelID=-1&SubjectID=-1&CountryID=-1&TargetGroupID=1&" + "SearchKeyWord=" + string.Empty;

           StreamWriter myWriter = null;
           HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create("https://ishallwin.com/ScholarshipServices.asmx/GetScholarships");
           //objRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
           objRequest.Method = "POST"; objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost);
           objRequest.ContentType = "application/x-www-form-urlencoded";
           try
           {
               myWriter = new StreamWriter(objRequest.GetRequestStream());
               myWriter.Write(strPost);
           }
           catch (Exception ex)
           {
               throw ex;
           }

           finally
           {
               myWriter.Close();
           }


           try
           {
               using (HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse())
               {
                   using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                   {
                       var a = sr.ReadToEnd(); a = a.Replace("null", "0");
                       sr.Close();
                       List<IKDScholarship> ListOfScholarship = new JavaScriptSerializer().Deserialize<ScholarshipsContainer>(a).Scholarships.ToList();
                       return ListOfScholarship;
                   }
               }

           }
           catch (WebException webex)
           {
               WebResponse errResp = webex.Response;
               using (Stream respStream = errResp.GetResponseStream())
               {
                   StreamReader reader = new StreamReader(respStream);
                   string text = reader.ReadToEnd();
               }
               return null;
           }
           catch (System.Web.Services.Protocols.SoapException se)
           {

               return null;
           }

       }*/

        #endregion

        #endregion




    }

    #region City List


    public class CityListResponce
    {
        public string status { get; set; }
        public string success { get; set; }
        public _0[] _0 { get; set; }
    }

    public class _0
    {
        public City_Name city_name { get; set; }
        public City_Code city_code { get; set; }
    }

    public class City_Name
    {
        public string _0 { get; set; }
    }

    public class City_Code
    {
        public string _0 { get; set; }
    }


    #endregion

    #region Tariff 


    public class TariffRequest
    {
        public string acno { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public string service { get; set; }
        public string wgt { get; set; }
    }


    public class TariffResponce
    {
        public string result { get; set; }
    }


    #endregion

    #region Order
    public class orderRequest
    {
        public string acno { get; set; }
        public string testbit { get; set; }
        public string userid { get; set; }
        public string password { get; set; }
        public string cn_generate { get; set; }
        public string customer_name { get; set; }
        public string customer_email { get; set; }
        public string customer_contact { get; set; }
        public string customer_address { get; set; }
        public string customer_city { get; set; }
        public string customer_country { get; set; }
        public string customer_comment { get; set; }
        public string shipping_charges { get; set; }
        public string payment_type { get; set; }
        public string shipper_origion_city { get; set; }
        public string total_order_amount { get; set; }
        public string order_refernce_code { get; set; }
        public Products_Detail[] products_detail { get; set; }
    }

    public class Products_Detail
    {
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string product_price { get; set; }
        public string product_quantity { get; set; }
        public string product_variations { get; set; }
        public string sku_code { get; set; }
    }

    #endregion

    #region Status  

    public class BlueexStatus
    {
        public string acno { get; set; }
        public string userid { get; set; }
        public string password { get; set; }
        public string order_refernce_code { get; set; }
    }

    #endregion


   
    public class CancelOrderRequest
    {
        public string acno { get; set; }
        public string userid { get; set; }
        public string password { get; set; }
        public string consignee_number { get; set; }
    }





}