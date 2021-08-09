using Silkways.Models;
using Silkways.Models.SilkwaysFunction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Silkways.Controllers
{
    public class SlidersController : Controller
    {
        //
        // GET: /Sliders/
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public ActionResult AddSlider()
        {
            if( Convert.ToInt32( Request.QueryString["ID"] ) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "" )
            {
                int ID = Convert.ToInt32( Request.QueryString["ID"] );
                tblSlider EditUser = new SlidersModel().GetByID( ID );
                EditUser.Image = Constants.PathSliderImages + EditUser.Image; 
                return View( EditUser );
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateInput( false )]
        public ActionResult AddSlider( tblSlider slider, HttpPostedFileBase file, string ImageName )
        {
            if( file != null )
            {
                if( file.ContentLength > 0 )
                {
                    slider.Image = GernalFunction.SaveFile(file, Constants.PathSliderImages);
                }
            }
            else if( ImageName != null )
            {
                slider.Image = Path.GetFileName( ImageName );
            }
            else
            {
                slider.Image = Constants.NoImage;
            }
            slider.Created_Date = DateTime.Now;
            if( Convert.ToInt32( Request.QueryString["ID"] ) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "" )
            {
                slider.ID = Convert.ToInt32( Request.QueryString["ID"] );
                new SlidersModel().Update( slider );
                TempData["AlertTask"] = "Record Successfully Update";
            }
            else
            {
                slider.IsActive = true;
                new SlidersModel().Save( slider );
                TempData["AlertTask"] = "Record Successfully Saved";
            }
            return RedirectToAction( "ManageSliders" );
        }
        public ActionResult ManageSliders()
        {
            List<tblSlider> lst = new SlidersModel().GetAll();
            return View( lst );
        }
        public void Delete( int ID )
        {
            new SlidersModel().Delete( ID );
            TempData["AlertTask"] = "Record Successfully Deleted";
            Response.Redirect( "ManageSliders" );
        }
	}
}