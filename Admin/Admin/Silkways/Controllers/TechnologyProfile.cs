using iText.Forms.Fields;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Silkways.Models;
using Silkways.Models.SilkwaysFunction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Table = iText.Layout.Element.Table;

namespace Silkways.Controllers
{
    public class TechnologyProfileController : Controller
    {
        private static GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public ActionResult Add()
        {
           

            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                TechnologyProfile Edit = new TechnologyProfileModel().GetProfileByID(ID);

                List<ProfileImage> lstImages = db.ProfileImages.Where(a => a.TechnologyProfileID == ID).ToList();
                foreach (var item in lstImages)
                {
                    ViewBag.img += "<li style='width:215px; height:215px;'><img src='" + Constants.PathProfileImages + item.Image + "' value='" + item.ID + "'  width='200px'  height='200px' style='float: left; height:200px; width=200px; margin: 0px; ' /><span value='" + item.ID + " ' onclick='ImageRemove(this);' class='close'>X</span><input type='text' value='" + item.Image + "' style='display:none;' /></li>";
                }

                //Edit.Image = Constants.PathNewsImages + Edit.Image;
                return View(Edit);
            }
            else
            {
                return View();
            }
        }



        public static string Create_ProfilePDFFile(TechnologyProfile profileDetail, string filename)
        {


            //string filename = "TechnologyProfile";




           //PdfWriter writer = new PdfWriter(filename);



            PdfWriter writer = new PdfWriter(filename);


            // PdfWriter writer = new PdfWriter("D:\\Zeeshan\\SoftSolutions Projects\\Updated Data\\Stedec Project\\Admin - 2\\Admin - 2\\Admin\\Silkways\\"+ filename);
            PdfDocument pdf = new PdfDocument(writer);



            Document document = new Document(pdf);


            Table table = new Table(8, false);
            Cell heading1 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Date"));
            table.AddCell(heading1);
            Cell heading2 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Wpn"));
            table.AddCell(heading2);
            Cell heading3 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Round"));
            table.AddCell(heading3);
            Cell heading4 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Hits"));
            table.AddCell(heading4);
            Cell heading5 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Miss"));
            table.AddCell(heading5);
            Cell heading6 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Range"));
            table.AddCell(heading6);
            Cell heading7 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Typt of firer"));
            table.AddCell(heading7);
            Cell heading8 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Gp"));
            table.AddCell(heading8);

            Paragraph newline = new Paragraph(new Text("\n"));
            //Paragraph header = new Paragraph(profileDetail.Name).SetTextAlignment(TextAlignment.CENTER).SetFontSize(20).SetBold();

            Paragraph header = new Paragraph();
            Text heading = new Text("Government of Pakistan Ministry of Science and Technology \n").SetTextAlignment(TextAlignment.CENTER).SetFontSize(17);
            Text heading10 = new Text("STEDEC Technology Commercialization Corporation \n").SetTextAlignment(TextAlignment.CENTER).SetFontSize(17);
            Text heading11 = new Text("Proposed Technology Profile \n").SetTextAlignment(TextAlignment.CENTER).SetFontSize(17);

            header.Add(heading);
            header.Add(heading10);
            header.Add(heading11);



            //PdfFormField personal = PdfFormField.createEmptyField(pdfDoc);
            PdfTextFormField name =
                    PdfFormField.CreateText(pdf, new Rectangle(36, 760, 108, 30),
                            "name", "");
            //personal.addKid(name);


            //PdfFormField personal = PdfFormField.createEmptyField(pdfDoc);
            //personal.setFieldName("personal");
            //PdfTextFormField name =
            //        PdfFormField.createText(pdfDoc, new Rectangle(36, 760, 108, 30),
            //                "name", "");
            //personal.addKid(name);



            //Paragraph PracticeHistory = new Paragraph("PracticeHistory").SetFontSize(18).SetBold();


            Paragraph DeveloperInformtion = new Paragraph("1. INFORMATION ABOUT DEVELOPER").SetFontSize(16).SetBold();
            Paragraph Name = new Paragraph("a) Name :").SetFontSize(10);

            if(profileDetail.Name!=null || profileDetail.Name != "")
            {
                Text Nametext = new Text("\n" + profileDetail.Name).SetFontSize(14);
                Name.Add(Nametext);
            }

          
      

            //PdfFormField personal = PdfFormField.CreateEmptyField(pdf);
            //personal.SetFieldName("personal");
            //PdfTextFormField name1 =
            //        PdfFormField.CreateText(pdf, new Rectangle(36, 760, 108, 30),
            //                "name", profileDetail.Name);
            //PdfTextFormField EntityName1 =
            //       PdfFormField.CreateText(pdf, new Rectangle(36, 760, 108, 30),
            //               "entity", profileDetail.EntityName);

            //personal.AddKid(name1);
            //personal.AddKid(EntityName1);

            //PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);


            //        PdfTextFormField nameField = PdfTextFormField.CreateText(pdf,
            //new Rectangle(99, 753, 425, 15), "name");

            //PdfAcroForm form = PdfAcroForm.getAcroForm(doc.getPdfDocument(), true);
            //PdfTextFormField nameField = PdfTextFormField.createText(doc.getPdfDocument(),
            // new Rectangle(99, 753, 425, 15), "name", "");
            //form.addField(nameField);

            //        PdfTextFormField nameField = PdfTextFormField.createText(doc.getPdfDocument(),
            //new Rectangle(99, 753, 425, 15), "name", "");
            //ParagraphBorder border = new ParagraphBorder();
            //border.setActive(true); 
            Paragraph EntityName = new Paragraph("  Entity Name :").SetFontSize(10);


            if (profileDetail.EntityName != null || profileDetail.EntityName != "")
            {
                Text Entitytext = new Text("\n" + profileDetail.EntityName.ToString()).SetFontSize(14);
                EntityName.Add(Entitytext);
            }

          

            Paragraph Qualifcation = new Paragraph("  Qualification  :").SetFontSize(10);


            if (profileDetail.Qualification != null )
            {
                Text QualificationText = new Text("\n" + profileDetail.Qualification.ToString()).SetFontSize(14);
                Qualifcation.Add(QualificationText);
            }
         


            Paragraph AffiliationName = new Paragraph("  Affiliation Name: ").SetFontSize(10);



            if (profileDetail.AffiliationName != null )
            {
                Text AffiliationNameText = new Text("\n" + profileDetail.AffiliationName.ToString()).SetFontSize(14);
                AffiliationName.Add(AffiliationNameText);
            }

     



            Paragraph Nationality = new Paragraph("  Nationality: ").SetFontSize(10);


            if (profileDetail.Nationality != null)
            {

                Text NationalityText = new Text("\n" + profileDetail.Nationality.ToString()).SetFontSize(14);
                Nationality.Add(NationalityText);
            }




            Paragraph Nationality2 = new Paragraph("  Nationality: ").SetFontSize(10);

            if(profileDetail.Nationality2!=null)
            {
                Text Nationality2Text = new Text("\n" + profileDetail.Nationality2.ToString()).SetFontSize(14);
                Nationality2.Add(Nationality2Text);
            }

     
          


            Paragraph Nationality3 = new Paragraph("  Nationality: ").SetFontSize(10);

            if (profileDetail.Nationality3 != null)
            {

                Text Nationality3Text = new Text("\n" + profileDetail.Nationality3.ToString()).SetFontSize(14);
                Nationality3.Add(Nationality3Text);

            }




            Paragraph ContactDetails = new Paragraph("b) Cotact Details , if more than one person developed the technolgy , Provide All Above: ").SetFontSize(10);

            if (profileDetail.ContactDetails != null)
            {

                Text ContactDetailsText = new Text("\n" + profileDetail.ContactDetails.ToString()).SetFontSize(14);
                ContactDetails.Add(ContactDetailsText);

            }


        



            Paragraph ContactDetailsofTechRes = new Paragraph("c) Cotact Details of technical resources available to support technology manufacturing : ").SetFontSize(10);

            if (profileDetail.ContactDetailsOfTechnicalResources != null)
            {
                Text ContactDetailsofTechResText = new Text("\n" + profileDetail.ContactDetailsOfTechnicalResources.ToString()).SetFontSize(14);
                ContactDetailsofTechRes.Add(ContactDetailsofTechResText);

            }


    


            Paragraph TechnologyDevelopment = new Paragraph("2. TECHNOLOGY DEVELOPMENT").SetFontSize(16).SetBold();
            Paragraph ProuctName = new Paragraph("a) Technology/Product/Process Name: ").SetFontSize(10);

            if (profileDetail.TechnologyName != null)
            {

                Text ProuctNameText = new Text("\n" + profileDetail.TechnologyName.ToString()).SetFontSize(14);
                ProuctName.Add(ProuctNameText);

            }





            Paragraph Synopsis = new Paragraph("b)  Synopsis: ").SetFontSize(10);


            if (profileDetail.Synopsis != null)
            {

                Text SynopsisText = new Text("\n" + profileDetail.Synopsis.ToString()).SetFontSize(14);
                Synopsis.Add(SynopsisText);

            }

        


            Paragraph ProtypeAvailability = new Paragraph("c) Is the prototype available?: ").SetFontSize(10);


            if (profileDetail.PrototypeAvailable == true  || profileDetail.PrototypeAvailable == false)
            {

                Text ProtypeAvailabilityText = new Text("\n" + profileDetail.PrototypeAvailable.ToString()).SetFontSize(14);
                ProtypeAvailability.Add(ProtypeAvailabilityText);

            }


         

            Paragraph PurposeTechnology = new Paragraph("d) Purpose of this Technology: ").SetFontSize(10);


            if (profileDetail.Purpose_of_Technology != null)
            {

                Text PurposeTechnologyText = new Text("\n" + profileDetail.Purpose_of_Technology.ToString()).SetFontSize(14);
                PurposeTechnology.Add(PurposeTechnologyText);

            }

      

            Paragraph Targets = new Paragraph("e)  Which industry sector the technology targets? ").SetFontSize(10);
            if (profileDetail.TechnologyTargets != null)
            {

                Text TargetsText = new Text("\n" + profileDetail.TechnologyTargets.ToString()).SetFontSize(14);
                Targets.Add(TargetsText);

            }
        

            Paragraph YearDeveloped = new Paragraph("f) Year in which you developed this technology? ").SetFontSize(10);
            if (profileDetail.DevelopedYear != null)
            {

                Text YearDevelopedText = new Text("\n" + profileDetail.DevelopedYear.ToString()).SetFontSize(14);
                YearDeveloped.Add(YearDevelopedText);

            }

        



            Paragraph Approval = new Paragraph("g) Any patent filed, regulatory, or other approvals required (list): ").SetFontSize(10);
            if (profileDetail.ApprovalsRequired != null)
            {


                Text ApprovalText = new Text("\n" + profileDetail.ApprovalsRequired.ToString()).SetFontSize(14);
                Approval.Add(ApprovalText);

            }




            Paragraph ApprovalOther = new Paragraph("h) Any patent filed, regulatory, or other approvals required (list): ").SetFontSize(10);
            if (profileDetail.OtherApprovalsRequired != null)
            {


                Text ApprovalOtherText = new Text("\n" + profileDetail.OtherApprovalsRequired.ToString()).SetFontSize(14);
                ApprovalOther.Add(ApprovalOtherText);

            }






            Paragraph PotentialBene = new Paragraph("i) Potential beneficiaries of this technology: ").SetFontSize(10);

            if (profileDetail.PotentialBenefiaciaries != null)
            {



                Text PotentialBeneText = new Text("\n" + profileDetail.PotentialBenefiaciaries.ToString()).SetFontSize(14);
                PotentialBene.Add(PotentialBeneText);

            }




            Paragraph EndUser = new Paragraph("j) Who would be the end user of the technology/product/process? ").SetFontSize(10);
            if (profileDetail.EndUserOfTechnology != null)
            {



                Text EndUserText = new Text("\n" + profileDetail.EndUserOfTechnology.ToString()).SetFontSize(14);
                EndUser.Add(EndUserText);

            }





            Paragraph EndUserBenefit = new Paragraph("k) How would the end user benefit from the technology? ").SetFontSize(10);

            if (profileDetail.BenefitsOfTechnology != null)
            {


                Text EndUserBenefitText = new Text("\n" + profileDetail.BenefitsOfTechnology.ToString()).SetFontSize(14);
                EndUserBenefit.Add(EndUserBenefitText);

            }


    


            Paragraph EndUserBenefit2 = new Paragraph("l) How would the end user benefit from the technology? ").SetFontSize(10);
            if (profileDetail.BenefitsOfTechnology2 == true || profileDetail.BenefitsOfTechnology2 == false)
            {


                Text EndUserBenefit2Text = new Text("\n" + profileDetail.BenefitsOfTechnology2.ToString()).SetFontSize(14);
                EndUserBenefit2.Add(EndUserBenefit2Text);

            }






            Paragraph TechnologyProCapacity = new Paragraph("3. TECHNOLOGY PRODUCTION CAPACITY").SetFontSize(16).SetBold();

            Paragraph ResearchCentre = new Paragraph("a) Research Centre/ Lab/ Facility used to develop the Technology: ").SetFontSize(10);

            if (profileDetail.ReserchCentreToDevelopTechnology != null)
            {


                Text ResearchCentreText = new Text("\n" + profileDetail.ReserchCentreToDevelopTechnology.ToString()).SetFontSize(14);
                ResearchCentre.Add(ResearchCentreText);

            }


      



            Paragraph AvailabilityFacility = new Paragraph("b) Is the facility currently available to develop the product / technology on Pilot scale:").SetFontSize(10);
            if (profileDetail.FacilityAvailability != null)
            {

                Text AvailabilityFacilityText = new Text("\n" + profileDetail.FacilityAvailability.ToString()).SetFontSize(14);
                AvailabilityFacility.Add(AvailabilityFacilityText);

            }

           


            Paragraph RawMeterialAvailability = new Paragraph("c) Are raw material available to develop the product / technology on Pilot scale:").SetFontSize(10);
            if (profileDetail.RawMaterialAvailability != null)
            {

                Text RawMeterialAvailabilityText = new Text("\n" + profileDetail.RawMaterialAvailability.ToString()).SetFontSize(14);
                RawMeterialAvailability.Add(RawMeterialAvailabilityText);

            }

           


            Paragraph EnablePilot = new Paragraph("d) If pilot production facility is unavailable, identify what resources and investment would be required to enable pilot production:").SetFontSize(10);

            if (profileDetail.ResourcesToEnablePilotPro != null)
            {

                Text EnablePilotText = new Text("\n" + profileDetail.ResourcesToEnablePilotPro.ToString()).SetFontSize(14);
                EnablePilot.Add(EnablePilotText);

            }
          



            Paragraph ProductionCapacity = new Paragraph("e) Present production capacity (daily/ monthly/ annually):").SetFontSize(10);

            if (profileDetail.ProductionCapacity != null)
            {


                Text ProductionCapacityText = new Text("\n" + profileDetail.ProductionCapacity.ToString()).SetFontSize(14);
                ProductionCapacity.Add(ProductionCapacityText);


            }


            Paragraph ProductionCostOf = new Paragraph("4. COST OF PRODUCTION").SetFontSize(16).SetBold();

            Paragraph ProductionCost = new Paragraph("a)  What is the cost of producing one unit within own facility:").SetFontSize(10);

            if (profileDetail.ProductionCost != null)
            {


                Text ProductionCostText = new Text("\n" + profileDetail.ProductionCost.ToString()).SetFontSize(14);
                ProductionCost.Add(ProductionCostText);


            }

       




            Paragraph OneUnit = new Paragraph("b) What is the cost of producing one unit, if at mass scale production (State figures):").SetFontSize(10);
            if (profileDetail.OneUnitCost != null)
            {



                Text OneUnitText = new Text("\n" + profileDetail.OneUnitCost.ToString()).SetFontSize(14);
                OneUnit.Add(OneUnitText);


            }


            Paragraph SalesandMar = new Paragraph("5.SALES AND MARKETING ").SetFontSize(16).SetBold();
            Paragraph Sold = new Paragraph("a) Has Technology/Product/Process been sold yet?").SetFontSize(10);

            if (profileDetail.TechnologySold == true || profileDetail.TechnologySold == false)
            {


                Text SoldText = new Text("\n" + profileDetail.TechnologySold.ToString()).SetFontSize(14);
                Sold.Add(SoldText);


            }

        

            Paragraph SalePrice = new Paragraph("b) Sale Price of Technology:").SetFontSize(10);

            if (profileDetail.SalePrice != null)
            {



                Text SalePriceText = new Text("\n" + profileDetail.SalePrice.ToString()).SetFontSize(14);
                SalePrice.Add(SalePriceText);


            }
         




            Paragraph CompetingLocal = new Paragraph("c) What are the competing local / imported products?").SetFontSize(10);

            if (profileDetail.CompetingProductsSustainability != null)
            {



                Text CompetingLocalText = new Text("\n" + profileDetail.CompetingProductsSustainability.ToString()).SetFontSize(14);
                CompetingLocal.Add(CompetingLocalText);


            }
         



            Paragraph UniqueSellingPoint = new Paragraph("d) What is the Unique Selling Point (USP) of the Technology?").SetFontSize(10);

            if (profileDetail.UniqueSellingPointSalesMarketing != null)
            {



                Text UniqueSellingPointText = new Text("\n" + profileDetail.UniqueSellingPointSalesMarketing.ToString()).SetFontSize(14);
                UniqueSellingPoint.Add(UniqueSellingPointText);


            }
     


            Paragraph Sustain = new Paragraph("6. SUSTAINABILITY ").SetFontSize(16).SetBold();
            Paragraph HowGreen = new Paragraph("a) How “Green” (environmental friendly and ecologically responsible) is your technology, and what is its ‘carbon footprint’*?* Carbon footprint definition: The total amount of greenhouse gases(including carbon dioxide and methane) that are generated by our actions.").SetFontSize(10);


            if (profileDetail.HowGreen != null)
            {



                Text HowGreenText = new Text("\n" + profileDetail.HowGreen.ToString()).SetFontSize(14);
                HowGreen.Add(HowGreenText);


            }
           



            Paragraph Sustainable = new Paragraph("b) Are you using sustainable products and processes for the development of your technology?").SetFontSize(10);


            if (profileDetail.SustainableProducts != null)
            {


                Text SustainableText = new Text("\n" + profileDetail.SustainableProducts.ToString()).SetFontSize(14);
                Sustainable.Add(SustainableText);


            }
       



            Paragraph CompeteingLocalSus = new Paragraph("What are the competing local / imported products?").SetFontSize(10);

            if (profileDetail.CompetingProductsSustainability != null)
            {


                Text CompeteingLocalSusText = new Text("\n" + profileDetail.CompetingProductsSustainability.ToString()).SetFontSize(14);
                CompeteingLocalSus.Add(CompeteingLocalSusText);


            }
     





            Paragraph USPSus = new Paragraph("What is the Unique Selling Point (USP) of the Technology?").SetFontSize(10);

            if (profileDetail.UniqueSellingPointSustainability != null)
            {



                Text USPSusText = new Text("\n" + profileDetail.UniqueSellingPointSustainability.ToString()).SetFontSize(14);
                USPSus.Add(USPSusText);


            }



            Paragraph Impact = new Paragraph("7. PERCEIVED ESTIMATED IMPACT ").SetFontSize(16).SetBold();
            Paragraph LocalEconomy = new Paragraph("a) What is the perceived impact of this technology on: Local Economy").SetFontSize(10);



            if (profileDetail.PerceivedImpactOnLocalEconomy != null)
            {

                Text LocalEconomyText = new Text("\n" + profileDetail.PerceivedImpactOnLocalEconomy.ToString()).SetFontSize(14);
                LocalEconomy.Add(LocalEconomyText);

            }

          



            Paragraph NationalEconomy = new Paragraph(" National Economy").SetFontSize(10);


            if (profileDetail.PerceivedImpactOnLocalEconomy != null)
            {

                Text NationalEconomyText = new Text("\n" + profileDetail.PerceivedImpactOnLocalEconomy.ToString()).SetFontSize(14);
                NationalEconomy.Add(NationalEconomyText);

            }




          


            Paragraph ServiceDelivery = new Paragraph("Service Delivery").SetFontSize(10);

            if (profileDetail.PerceivedImpactOnServiceDelivery != null)
            {



                Text ServiceDeliveryText = new Text("\n" + profileDetail.PerceivedImpactOnServiceDelivery.ToString()).SetFontSize(14);
                ServiceDelivery.Add(ServiceDeliveryText);


            }
          



            Paragraph EmploymentGeneration = new Paragraph("Employment Generation").SetFontSize(10);

            if (profileDetail.PerceivedImpactOnEmploymentGeneration != null)
            {


                Text EmploymentGenerationText = new Text("\n" + profileDetail.PerceivedImpactOnEmploymentGeneration.ToString()).SetFontSize(14);
                EmploymentGeneration.Add(EmploymentGenerationText);


            }

          



            Paragraph Environment = new Paragraph("Environment").SetFontSize(10);
            if (profileDetail.PerceivedImpactOnEnvironment != null)
            {

                Text EnvironmentText = new Text("\n" + profileDetail.PerceivedImpactOnEnvironment.ToString()).SetFontSize(14);
                Environment.Add(EnvironmentText);


            }



       


            Paragraph Society = new Paragraph("Society / Culture").SetFontSize(10);

            if (profileDetail.Society != null)
            {
                Text SocietyText = new Text("\n" + profileDetail.Society.ToString()).SetFontSize(14);
                Society.Add(SocietyText);


            }

       


            Paragraph impactassessment = new Paragraph("b) Any environmental impact assessment conducted/required?").SetFontSize(10);

            if (profileDetail.AttachAnnexures != null)
            {

                Text impactassessmentText = new Text("\n" + profileDetail.AttachAnnexures.ToString()).SetFontSize(14);
                impactassessment.Add(impactassessmentText);

            }







            Paragraph estimatedImpact = new Paragraph("Perceived Estimated Impact?").SetFontSize(10);

            if (profileDetail.EstamatedImpact != null)
            {
                Text estimatedImpactText = new Text("\n" + profileDetail.EstamatedImpact.ToString()).SetFontSize(14);
                estimatedImpact.Add(estimatedImpactText);

            }


 

            Paragraph AnySupport = new Paragraph("8. WHAT SUPPORT DO YOU EXPECT FROM STEDEC?  ").SetFontSize(16).SetBold();
            Paragraph Support = new Paragraph("What Support Do You Expect From Stedec?").SetFontSize(10);

            if (profileDetail.Supportexpectation != null)
            {
                Text SupportText = new Text("\n" + profileDetail.Supportexpectation.ToString()).SetFontSize(14);
                Support.Add(SupportText);

            }
          





            ////Paragraph GP = new Paragraph("GP : ").SetFontSize(16);
            ////Text GPtext = new Text(FirerDetail.GP.ToString()).SetFontSize(14);
            ////Ranking.Add(GPtext);
            //Paragraph Rank = new Paragraph("Rank : ").SetFontSize(16);
            //Text Ranktext = new Text(FirerDetail.Rank).SetFontSize(14);
            //Rank.Add(Ranktext);
            //Paragraph Unit = new Paragraph("unit : ").SetFontSize(16);
            //Text Unittext = new Text(FirerDetail.Unit).SetFontSize(14);
            //Unit.Add(Unittext);
            //Paragraph Headquater = new Paragraph("Headquater : ").SetFontSize(16);
            //Text Headquatertext = new Text(FirerDetail.Headquater).SetFontSize(14);
            //Headquater.Add(Headquatertext);
            //Paragraph Service = new Paragraph("Service : ").SetFontSize(16);
            //Text Servicetext = new Text(FirerDetail.Service).SetFontSize(14);
            //Service.Add(Servicetext);

            document.Add(header);
            //document.Add(header10);
            //document.Add(header11);
            //document.Add(newline);

            document.Add(DeveloperInformtion);
            document.Add(Name);
            //PdfFormField personal = PdfFormField.CreateEmptyField(pdf);
            //personal.SetFieldName("personal");
            //PdfTextFormField name1 =
            //        PdfFormField.CreateText(pdf, new Rectangle(200, 660, 300, 20),
            //                "name", profileDetail.Name);            
            //personal.AddKid(name1);

            //document.Add(nameField);

            document.Add(EntityName);
            // document.Add(EntityName);

            //PdfTextFormField EntityName1 =
            //       PdfFormField.CreateText(pdf, new Rectangle(200, 640, 300, 20),
            //               "entity", profileDetail.EntityName);
            //personal.AddKid(EntityName1);
            //PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);

            document.Add(Qualifcation);
            //PdfTextFormField Qualifcation1 =
            //   PdfFormField.CreateText(pdf, new Rectangle(200, 620, 300, 20),
            //           "qualification", profileDetail.Qualification);
            //personal.AddKid(Qualifcation1);
            //PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);

            document.Add(AffiliationName);
            //   PdfTextFormField AffiliationName1 =
            //PdfFormField.CreateText(pdf, new Rectangle(200, 600, 300, 20),
            //        "affiliationName", profileDetail.AffiliationName);
            //   personal.AddKid(AffiliationName1);
            //   PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);


            document.Add(Nationality);
            //     PdfTextFormField Nationality1 =
            //PdfFormField.CreateText(pdf, new Rectangle(200, 580, 300, 20),
            //        "nationality", profileDetail.Nationality);
            //     personal.AddKid(Nationality1);
            //     PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);


            document.Add(ContactDetails);
            document.Add(newline);

            //            PdfTextFormField ContactDetails1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 500, 500, 40),
            //"ContactDetails", profileDetail.ContactDetails);
            //            personal.AddKid(ContactDetails1);
            //            PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);



            document.Add(newline);


            document.Add(ContactDetailsofTechRes);

            //            PdfTextFormField ContactDetailsofTechRes1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36,420, 500, 40),
            //"ContactDetailsofTechRes", profileDetail.ContactDetailsOfTechnicalResources);
            //            personal.AddKid(ContactDetailsofTechRes1);
            //            PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);



            document.Add(newline);
            document.Add(newline);
            document.Add(TechnologyDevelopment);
            document.Add(ProuctName);
            //            PdfTextFormField ProuctName1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 330 , 500, 30),
            //"technologyname", profileDetail.TechnologyName);
            //            personal.AddKid(ProuctName1);
            //            PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);

            document.Add(newline);
            document.Add(Synopsis);
            //            PdfTextFormField Synopsis1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 270, 425, 30),
            //"entity", profileDetail.Synopsis);
            //            personal.AddKid(Synopsis1);
            //            PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);



            document.Add(ProtypeAvailability);
            //            PdfTextFormField ProtypeAvailability1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.PrototypeAvailable.ToString());
            //            personal.AddKid(ProtypeAvailability1);
            //            PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);




            document.Add(PurposeTechnology);
            //    PdfTextFormField PurposeTechnology1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.PurposeOfTechnology);
            //    personal.AddKid(PurposeTechnology1);
            //    PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);
            document.Add(Targets);
            //      PdfTextFormField Targets1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.TechnologyTargets);
            //      personal.AddKid(Targets1);
            //      PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);


            document.Add(YearDeveloped);
            //           PdfTextFormField YearDeveloped1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.DevelopedYear);
            //           personal.AddKid(YearDeveloped1);
            //           PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);


            document.Add(Approval);
            //            PdfTextFormField Approval1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.ApprovalsRequired);
            //            personal.AddKid(Approval1);
            //            PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);

            document.Add(ApprovalOther);
            //            PdfTextFormField ApprovalOther1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.OtherApprovalsRequired);
            //            personal.AddKid(ApprovalOther1);
            //            PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);

            document.Add(PotentialBene);
            //            PdfTextFormField PotentialBene1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.PotentialBenefiaciaries);
            //            personal.AddKid(PotentialBene1);
            //            PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);

            document.Add(EndUserBenefit);
            //            PdfTextFormField EndUserBenefit1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.BenefitsOfTechnology);
            //            personal.AddKid(EndUserBenefit1);
            //            PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);

            document.Add(EndUserBenefit2);
            //            PdfTextFormField EndUserBenefit4 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.BenefitsOfTechnology2.ToString());
            //            personal.AddKid(EndUserBenefit4);
            //            PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);

            document.Add(TechnologyProCapacity);
            document.Add(ResearchCentre);
            //            PdfTextFormField ResearchCentre1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.ReserchCentreToDevelopTechnology);
            //            personal.AddKid(ResearchCentre1);
            //            PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);

            document.Add(AvailabilityFacility);
            //            PdfTextFormField AvailabilityFacility1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.FacilityAvailability);
            //            personal.AddKid(AvailabilityFacility1);
            //            PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);


            document.Add(RawMeterialAvailability);
            //            PdfTextFormField RawMeterialAvailability1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.RawMaterialAvailability);
            //            personal.AddKid(RawMeterialAvailability1);
            //            PdfAcroForm.GetAcroForm(pdf, true).AddField(personal);

            document.Add(EnablePilot);
            //            PdfTextFormField EnablePilot1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.ResourcesToEnablePilotPro);
            //            personal.AddKid(EnablePilot1);

            document.Add(ProductionCapacity);
            //            PdfTextFormField ProductionCapacity1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.ProductionCapacity);
            //            personal.AddKid(ProductionCapacity1);


            document.Add(ProductionCostOf);
            document.Add(ProductionCost);
            //            PdfTextFormField ProductionCost1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.ProductionCost);
            //            personal.AddKid(ProductionCost1);
            document.Add(OneUnit);
            //            PdfTextFormField OneUnit1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.OneUnitCost);
            //            personal.AddKid(OneUnit1);


            document.Add(SalesandMar);
            document.Add(Sold);
            //            PdfTextFormField Sold1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.TechnologySold.ToString());
            //            personal.AddKid(Sold1);


            document.Add(SalePrice);
            //            PdfTextFormField SalePrice1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.SalePrice);
            //            personal.AddKid(SalePrice1);

            document.Add(CompetingLocal);
            //            PdfTextFormField CompetingLocal1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.CompetingProductsSalesMarketing);
            //            personal.AddKid(CompetingLocal1);


            document.Add(UniqueSellingPoint);

            //            PdfTextFormField UniqueSellingPoint1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.UniqueSellingPointSalesMarketing);
            //            personal.AddKid(UniqueSellingPoint1);


            document.Add(Sustain);
            document.Add(HowGreen);
            //            PdfTextFormField HowGreen1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.HowGreen);
            //            personal.AddKid(HowGreen1);

            document.Add(Sustainable);
            //            PdfTextFormField Sustainable1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.SustainableProducts);
            //            personal.AddKid(Sustainable1);

            document.Add(CompeteingLocalSus);
            //            PdfTextFormField CompeteingLocalSus1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.CompetingProductsSustainability);
            //            personal.AddKid(CompeteingLocalSus1);


            document.Add(USPSus);
            //            PdfTextFormField USPSus1 =
            //PdfFormField.CreateText(pdf, new Rectangle(36, 600, 425, 30),
            //"entity", profileDetail.UniqueSellingPointSustainability);
            //            personal.AddKid(USPSus1);



            document.Add(Impact);
            //document.Add(LocalEconomy);
            //document.Add(NationalEconomy);
            document.Add(ServiceDelivery);
            document.Add(EmploymentGeneration);
            document.Add(Environment);
            document.Add(Society);
            //document.Add(impactassessment);
            //document.Add(estimatedImpact);
            document.Add(AnySupport);



            //document.Add(newline);
            //document.Add(Ranking);
            ////document.Add(GP);
            ////document.Add(newline);
            //document.Add(Rank);
            ////document.Add(newline);
            //document.Add(Unit);
            ////document.Add(newline);
            //document.Add(Headquater);
            ////document.Add(newline);
            //document.Add(Service);
            ////document.Add(newline);
            //document.Add(PracticeHistory);
            ////document.Add(newline);
            //document.Add(table);
            // Page numbers
            //int n = pdf.GetNumberOfPages();
            //for (int i = 1; i <= n; i++)
            //{
            //    document.ShowTextAligned(new Paragraph(String
            //       .Format("page" + i + " of " + n)),
            //       559, 806, i, TextAlignment.RIGHT,
            //       VerticalAlignment.TOP, 0);
            //}
            document.Flush();
            // Close document
            document.Close();
            return filename;
        }




        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(TechnologyProfile profile, HttpPostedFileBase[] file , string VideoEmbed)
        {
            if (profile.Name != null && profile.Name != "")
            {
                //if (file != null)
                //{
                //    news.Image = GernalFunction.SaveFile(file, Constants.PathNewsImages);
                //}
                //else
                //{
                //    news.Image = Constants.NoImage;
                //}



               


                if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
                {
                    profile.ID = Convert.ToInt32(Request.QueryString["ID"]);
                    //news.DateTime = DateTime.Now;
                    //new TechnologyProfileModel().Update(profile);


                    string filename = "ProfileReport_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_tt") + ".pdf";
                    //string file = "E:\\EGDownloads\\Media\\";
                    //string file = HttpContext.Current.Request.PhysicalApplicationPath + "\\pdf\\";

                    //string Profilefile = HttpContext.Request.Path + "\\pdf\\";
                    string Profilefile = Request.PhysicalApplicationPath + "pdf\\";
                    string file1 = System.IO.Path.Combine(Profilefile, filename);
                    var myFile = System.IO.File.Create(file1);
                    myFile.Close();



                    string FileName = Create_ProfilePDFFile(profile, Profilefile+filename);



                    new TechnologyProfileModel().Update(profile);




                    TempData["AlertTask"] = "Record Successfully Update";
                }
                else
                {
                    //news.DateTime = DateTime.Now;
                    new TechnologyProfileModel().Save(profile);
                    TempData["AlertTask"] = "Record Successfully Saved";
                }

                if (file != null && file.Length > 0 && profile.ID > 0)
                {
                    int count = 1;
                    foreach (var item in file)
                    {
                        if (item != null)
                        {
                            ProfileImage GP = new ProfileImage();
                            GP.TechnologyProfileID = profile.ID;
                            GP.Image = GernalFunction.SaveFile(item, Constants.PathProfileImages, count + "-" + Convert.ToString(DateTime.Now.Ticks) + item.FileName.Substring(item.FileName.IndexOf(".")));
                            db.ProfileImages.Add(GP);
                            db.SaveChanges();
                            //string ResizePicturePath = Constants.PathGalleryImages;
                            //string ResizePictureNewPath = Constants.PathGalleryImagesthumb;
                            //GernalFunction.ResizePicture3(198, 284, ResizePicturePath, GP.Image, ResizePictureNewPath, 0.5, item.InputStream);
                        }
                        count++;
                    }
                }



                if (VideoEmbed!=null &&  VideoEmbed != "" && profile.ID > 0 )
                {
             
                   
                    {
                     
                       
                            ProfileVideo GP = new ProfileVideo();
                            GP.TechnologyProfileID = profile.ID;
                            GP.VideoLink = VideoEmbed;
                        List<ProfileVideo> List = new List<ProfileVideo>();
                        List =  db.ProfileVideos.Where(x => x.TechnologyProfileID == profile.ID).ToList();
                        foreach (var item in List)
                        {
                            db.ProfileVideos.Remove(item);

                        }
                            db.ProfileVideos.Add(GP);
                            db.SaveChanges();
                
                        
                     
                    }
                }





                return RedirectToAction("Manage");
            }
            else
            {
                TempData["AlertTask"] = "Please Enter the Description";
                return View();
            }
        }

        public ActionResult Manage()
        {
            List<TechnologyProfile> lst = new TechnologyProfileModel().GetAllProfiles();
            return View(lst);
        }

        public void Delete(int ID)
        {
            new NewsModel().Delete(ID);
            TempData["AlertTask"] = "Record Successfully Deleted";
            Response.Redirect("Manage");
        }


        public bool DeleteProfileImage(int id)
        {
            var photoModel = db.ProfileImages.Where(x => x.ID == id).SingleOrDefault();

            string fullPath = Request.MapPath(Constants.PathProfileImages + photoModel.Image);
            System.IO.File.Delete(fullPath);
            db.ProfileImages.Remove(photoModel);
            db.SaveChanges();
            TempData["tempId"] = "1";
            return true;
        }

    }
}