using Silkways.Models;
using Silkways.Models.SilkwaysFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.ViewModels
{
    public class ProductListingViewModel
    {
        public string pagination { get; set; }
        List<Product> pro;
        public List<Product> Pro
        {
            get
            {
                if (pro != null)
                {
                    return pro;
                }
                else
                {
                    int pageno = 0;
                    if (HttpContext.Current.Request.QueryString["pageno"] != null)
                    {
                        pageno = Convert.ToInt32(HttpContext.Current.Request.QueryString["pageno"]) - 1;
                    }

                    pro = new ProductModel().SearchPro(pageno);
                    int count = new ProductModel().GetCountPro();
                    pagination = GernalFunction.BuildBootstrapPagination(count, "Product/ManageProduct", pageno, 50);
                    return pro;
                }
            }
        }
    }
}