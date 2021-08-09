using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using Silkways.Models;

namespace Silkways
{
    public static class CommonFunction
    {
       
        public static string SaveFile(HttpPostedFileBase ImageFile, string Path, string FileName = "")
        {
            FileName = string.IsNullOrEmpty(FileName) ? Convert.ToString(DateTime.Now.Ticks) + ImageFile.FileName.Substring(ImageFile.FileName.IndexOf(".")) : FileName;
            string ImageServerPath = HttpContext.Current.Server.MapPath(Path + "/" + FileName);
            ImageFile.SaveAs(ImageServerPath);
            return FileName;
        }
        public static void ResizePicture3(int height, int width, string path, string filename, string SavePath, double scaleFactor, Stream sourcePath)
        {
            Size newsize = new Size();
            newsize.Width = width;
            newsize.Height = height;

            using (Bitmap newbmp = new Bitmap(newsize.Width, newsize.Height), oldbmp = Bitmap.FromFile(HttpContext.Current.Server.MapPath(path + filename)) as Bitmap)
            {
                using (Graphics newgraphics = Graphics.FromImage(newbmp))
                {
                    using (var image = Image.FromStream(sourcePath))
                    {
                        newgraphics.DrawImage(oldbmp, 0, 0, newsize.Width, newsize.Height);
                        newgraphics.Save();
                        string newfilename = filename;
                        var newWidth = (int)(image.Width * scaleFactor);
                        var newHeight = (int)(image.Height * scaleFactor);
                        // var thumbnailImg = new Bitmap(newWidth, newHeight);
                        //var thumbGraph = Graphics.FromImage(newbmp);
                        //thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        //thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        //thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        //var thumbGraph = Graphics.FromImage(newbmp);
                        //newgraphics.CompositingQuality = CompositingQuality.HighQuality;
                        //newgraphics.SmoothingMode = SmoothingMode.HighQuality;
                        //newgraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                        //thumbGraph.DrawImage(image, imageRectangle);
                        var s = HttpContext.Current.Server.MapPath(SavePath + newfilename);
                        newbmp.Save(s, image.RawFormat);
                        newbmp.Dispose();
                        //thumbnailImg.Save(targetPath, image.RawFormat);
                    }


                }
            }
        }
        public static string ContactInquiryBody(string name, string email, string phone, string services, string message)
        {
            string html = @"
<!DOCTYPE html>
<html>
<head>
    <meta name='viewport' content='width=device-width'>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
    <title>Simple Transactional Email</title>
    <link href='https://fonts.googleapis.com/css?family=Lato:300,400,700,900' rel='stylesheet'>
    <style type='text/css'>
            /* -------------------------------------
            INLINED WITH https://putsmail.com/inliner
        ------------------------------------- */
            /* -------------------------------------
            RESPONSIVE AND MOBILE FRIENDLY STYLES
        ------------------------------------- */
            @media only screen and (max-width: 620px) {
                table[class=body] h1 {
                    font-size: 28px !important;
                    margin-bottom: 10px !important;
                }

                table[class=body] p,
                table[class=body] ul,
                table[class=body] ol,
                table[class=body] td,
                table[class=body] span,
                table[class=body] a {
                    font-size: 16px !important;
                }

                table[class=body] .wrapper,
                table[class=body] .article {
                    padding: 10px !important;
                }

                table[class=body] .content {
                    padding: 0 !important;
                }

                table[class=body] .container {
                    padding: 0 !important;
                    width: 100% !important;
                }

                table[class=body] .main {
                    border-left-width: 0 !important;
                    border-radius: 0 !important;
                    border-right-width: 0 !important;
                }

                table[class=body] .btn table {
                    width: 100% !important;
                }

                table[class=body] .btn a {
                    width: 100% !important;
                }

                table[class=body] .img-responsive {
                    height: auto !important;
                    max-width: 100% !important;
                    width: auto !important;
                }
            }
            /* -------------------------------------
            PRESERVE THESE STYLES IN THE HEAD
        ------------------------------------- */
            @media all {
                .ExternalClass {
                    width: 100%;
                }

                    .ExternalClass,
                    .ExternalClass p,
                    .ExternalClass span,
                    .ExternalClass font,
                    .ExternalClass td,
                    .ExternalClass div {
                        line-height: 100%;
                    }

                .apple-link a {
                    color: inherit !important;
                    font-family: inherit !important;
                    font-size: inherit !important;
                    font-weight: inherit !important;
                    line-height: inherit !important;
                    text-decoration: none !important;
                }

                .btn-primary table td:hover {
                    background-color: #c12400 !important;
                }

                .btn-primary a:hover {
                    background-color: #c12400 !important;
                    border-color: #c12400 !important;
                }
            }

            @media only screen and (max-width:479px) {
                div.milestone {
                    float: left;
                    width: 200px;
                }

                    div.milestone b {
                        font-size: 95px !important;
                    }

                div.cupimage {
                    margin-right: 10px !important;
                    width: 100px !important;
                }

                div.milestone p {
                    margin-top: -35px !important;
                }

                .content-block-footer {
                    padding: 0px 15px 35px !important;
                }
            }
    </style>
</head>
<body class='' style='background-color:#fff;font-family:arial;-webkit-font-smoothing:antialiased;font-size:14px;line-height:1.4;margin:0;padding:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;'>
    <table border='0' cellpadding='0' cellspacing='0' class='body' style='border-collapse:separate;mso-table-lspace:0pt;mso-table-rspace:0pt;background-color:#fff;width:100%;'>
        <tr>
            <td style='font-family:arial;font-size:14px;vertical-align:top;'>&nbsp;</td>
            <td class='container' style='font-family:arial;font-size:14px;vertical-align:top;display:block;max-width:580px;padding:10px;width:580px;Margin:0 auto !important;'>
                <div class='content' style='box-sizing:border-box;display:block;Margin:0 auto;max-width:580px;border:1px #bbb1b1 solid;'>
                    <!-- START CENTERED WHITE CONTAINER -->
                    <div style='float:left;width:100%;border-radius:0px;background:#f2f2f2;padding:10px 0px;text-align:center;'><a style='display:inline-block;' href='http://www.smartproducts.com.pk/'><img style='float:none;display:inline-block;    width: 140px;' src='http://www.smartproducts.com.pk/assets/images/mt-logo.png' alt='' /></a></div>
                    <table class='main' style='border-collapse:separate;mso-table-lspace:0pt;mso-table-rspace:0pt;background:#fff;border-radius:0px;width:100%;'>
                        <!-- START MAIN CONTENT AREA -->
                        <tr>
                            <td class='wrapper' style='font-family:arial;font-size:14px;vertical-align:top;box-sizing:border-box;padding:20px 45px;'>
                                <table border='0' cellpadding='0' cellspacing='0' style='border-collapse:separate;mso-table-lspace:0pt;mso-table-rspace:0pt;width:100%;'>

                                    <tr>
                                        <td style='padding:15px 0px;'>
                                            <div style='float:left;width:100%;font-size:14px;color:#606060;font-family:arial'>Dear " + name + @",</div>
                                            <div style='float:left;width:100%;font-family:arial;'>
                                                <div style='display:block;margin-top:14px;font-size:14px;color:#606060;text-decoration:none;'>
<table border='0' cellpadding='5' cellspacing='0' style='width:100%;'>
<tr>
<td><strong>Name</strong></td>
<td>" + name + @"</td>
</tr>
<tr>
<td><strong>Email</strong></td>
<td>" + email + @"</td>
</tr>
<tr>
<td><strong>Phone</strong></td>
<td>" + phone + @"</td>
</tr>
<tr>
<td><strong>Services</strong></td>
<td>" + services + @"</td>
</tr>
<tr>
<td><strong>Message</strong></td>
<td>" + message + @"</td>
</tr>

</table>
                                                    
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <!-- END MAIN CONTENT AREA -->
                    </table>
                    <!-- START FOOTER -->
                    <div class='footer' style='clear:both;width:100%;'>
                        <table border='0' cellpadding='0' cellspacing='0' style='border-collapse:separate;mso-table-lspace:0pt;mso-table-rspace:0pt;width:100%;'>
                            <tr>
                                <td class='content-block content-block-footer' style='padding:0px 45px 35px;'>
                                    <span class='apple-link' style='border-top:1px #fb9351 solid;color:#606060;font-size:14px;text-align:left;display:block;padding-top:35px;'>
                                        This email is auto generated and does not need a reply from you.
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- END FOOTER -->
                    <!-- END CENTERED WHITE CONTAINER -->
                </div>
            </td>
            <td style='font-family:arial;font-size:14px;vertical-align:top;'>&nbsp;</td>
        </tr>
    </table>
</body>
</html>";
            return html;
        }
        public static bool EmailSent(string EmailBody, string Subject, List<string> ToEmail = null, List<string> CCEmail = null, List<string> BCCEmail = null)
        {
            bool Status = false;
            SystemSetting SS = new SystemSetting();
            using (var esdb = new GaintShoeBoxEntities())
            {
                SS = esdb.SystemSettings.FirstOrDefault();
            }
            if (SS != null
                && SS.ID > 0
                && !string.IsNullOrEmpty(SS.SenderEmail)
                && !string.IsNullOrEmpty(SS.Email)
                && !string.IsNullOrEmpty(SS.SenderPassword)
                && !string.IsNullOrEmpty(SS.SMTPHost))
            {
                string From = SS.SenderEmail;
                string Password = SS.SenderPassword;
                string Host = SS.SMTPHost;
                int Port = Convert.ToInt32(SS.Port);
                try
                {
                    var message = new MailMessage();
                    //message.To.Add(ToEmail);
                    if (ToEmail != null)
                    {
                        foreach (var to in ToEmail)
                        {
                            message.To.Add(new MailAddress(to));
                        }
                    }
                    if (CCEmail != null)
                    {
                        foreach (var CC in CCEmail)
                        {
                            message.CC.Add(new MailAddress(CC));
                        }
                    }
                    if (BCCEmail != null)
                    {
                        foreach (var BCC in BCCEmail)
                        {
                            message.Bcc.Add(new MailAddress(BCC));
                        }
                    }
                    message.From = new MailAddress(From, SS.Email);  // replace with valid value
                    message.Subject = Subject;
                    message.Body = EmailBody;
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.SubjectEncoding = System.Text.Encoding.Default;
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = From,
                            Password = Password 
                        };
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = credential;
                        smtp.Host = Host;
                        smtp.Port = Port;

                        smtp.Send(message);
                        smtp.EnableSsl = SS.IsSSL;
                        Status = true;
                    }
                }
                catch (Exception ex)
                {
                    Status = false;
                    throw ex;
                }
            }
            else
            {
                Status = false;
            }


            return Status;
        }
    }
}