using Microsoft.Ajax.Utilities;
using Silkways.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Silkways.Models.SilkwaysFunction
{
    public class GernalFunction
    {
      
        public static string ImageCroping(string crop, string Filepath, bool thumbnail, out string HostedFilePath,out string FileNamed)
        {
            string HostFilePath = "";
            string CropFileName = "";
            if (!String.IsNullOrEmpty(crop))
            {
                var fileName = "";
                var imageStr64 = crop;
                var imgCode = imageStr64.Split(',');

                var bytes = Convert.FromBase64String(imgCode[1]);

                using (var stream = new MemoryStream(bytes))
                {
                    using (var img = System.Drawing.Image.FromStream(stream))
                    {
                        var filename = "";
                        var fileext = "";
                        if (imgCode.Contains("png"))
                        {
                            filename = "Image" + DateTime.UtcNow;
                            fileext = ".png";
                        }
                        else
                        {
                            filename = "Image" + DateTime.UtcNow;
                            fileext = ".jpg";
                        }

                        string fileNameNew = filename;
                        fileNameNew = RemoveSpecialCharacters(fileNameNew);


                        var path = HttpContext.Current.Server.MapPath(Filepath);

                        var ext = Path.GetExtension(filename);

                        var i = 0;
                        while (File.Exists(path + fileNameNew + fileext))
                        {
                            i++;
                            fileNameNew = Path.GetFileNameWithoutExtension(filename);
                            fileNameNew += i;
                        }


                        fileName = fileNameNew + fileext;
                        var filePath = HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + Filepath + fileName);
                        string filePathSaved =  Filepath + fileName;
                        File.WriteAllBytes(filePath, bytes);
                        HostFilePath = filePathSaved;
                        CropFileName = fileName;

                        if (thumbnail == true)
                        {
                            string ThumbImgName = fileName.Replace("Image", "Thumb");
                            Resize_Image_Thumb(filePath, fileName, 300, 300, ThumbImgName, "");
                        }
                        
                    }

                }
                HostedFilePath = HostFilePath;
                FileNamed = CropFileName;
                return fileName;
            }
            else
            {
                HostedFilePath = HostFilePath;
                FileNamed = CropFileName;
                return null;
            }
            
        }

        public static string BuildBootstrapPagination(decimal numberOfPages, string url, int pageNo, int NumberofRecordsToBeShown)
        {

            string html = string.Empty;
            html = Pagination(Convert.ToInt32(Math.Ceiling(numberOfPages / NumberofRecordsToBeShown)), pageNo, url);
            return html;

        }

        public static string Pagination(int numberOfPages, int pageNo, string url)
        {
            string html = string.Empty;
            pageNo = pageNo + 1;
            int numericLinksCount = 9;
            int noOfPages = numberOfPages;
            int n = noOfPages;
            if (noOfPages > 9)
            {
                n = 11;
            }

            int series = 0;
            series = pageNo / 9;
            if (pageNo < 10 || pageNo % 9 == 0)
            {
                series = pageNo / 10;
            }


            for (int i = 1; i <= n; i++)
            {
                int page = i + (9 * series);

                if (page > noOfPages)
                {
                    break;
                }

                if (page > 9 && i == 1)
                {
                    int less = page - 1;
                    html += @"<li><a href='/" + url + "?pageno=" + less + "'>Previous</a></li>";
                }

                if (pageNo == page)
                {
                    html += @"<li class='active'><a href='/" + url + "?pageno=" + page + "'>" + page + "</a></li>";
                }
                else
                {
                    if (i == 10)
                    {
                        html += @"<li><a href='/" + url + "?pageno=" + page + "'> Next</a></li>";
                    }

                    if (i < 10)
                    {
                        html += @"<li><a href='/" + url + "?pageno=" + page + "'>" + page + "</a></li>";
                    }
                }
            }
            return html;



        }
        public static bool CreateFTPDirectory(string directory)
        {
            FtpWebRequest requestDir = null;
            try
            {
                //create the directory
                directory = "ftp://184.154.206.133/"+ directory;
                requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(directory));
                requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                requestDir.Credentials = new NetworkCredential("ftplahoreweb", "aslkfj@asdlfkj#kK");
                requestDir.UseBinary = true;
                requestDir.Proxy = null;
                requestDir.KeepAlive = false;
                requestDir.UsePassive = true;
       
                FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                ftpStream.Dispose();
                response.Close();
                response.Dispose();
                return true;
            }
            catch
            {
                if (requestDir != null)
                {
                    requestDir.Abort();
                }

                return false;


            }
        }
        //public static bool CreateFTPDirectory(string directory)
        //{
        //    FtpWebRequest requestDir = null;
        //    try
        //    {
        //        //create the directory
        //        directory = "ftp://184.154.206.133/" + directory;
        //        requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(directory));
        //        requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
        //        requestDir.Credentials = new NetworkCredential("ftplahoreweb", "aslkfj@asdlfkj#kK");
        //        requestDir.UseBinary = true;
        //        requestDir.Proxy = null;
        //        requestDir.KeepAlive = false;
        //        requestDir.UsePassive = true;
        //        FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
        //        Stream ftpStream = response.GetResponseStream();

        //        ftpStream.Close();
        //        ftpStream.Dispose();
        //        response.Close();
        //        response.Dispose();
        //        return true;
        //    }
        //    catch
        //    {
        //        if (requestDir != null)
        //        {
        //            requestDir.Abort();
        //        }

        //        return false;


        //    }
        //}

        public static void Resize_Image_Thumb(string filePath, string srcImgName, int newWidth, int newHeight, string ThumbImgName, string nPath)
        {
            //Get Destinatino Path of image
            string newImgName = null;
            string newImagePath = null;
            string imgName = null;
            string imgExt = null;

            string[] NameExtArray = srcImgName.Split(new char[] { '.' });
            imgName = NameExtArray[0];
            imgExt = NameExtArray[1];
            newImgName = ThumbImgName;
            newImagePath = filePath.Replace(srcImgName, ThumbImgName);

            Image imgPhoto = Image.FromFile(filePath);

            int sourceWidth = 0;
            int sourceHeight = 0;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;
            double nPercent = 0;
            double nPercentW = 0;
            double nPercentH = 0;
            sourceWidth = imgPhoto.Width;
            sourceHeight = imgPhoto.Height;
            sourceX = 0;
            sourceY = 0;
            destX = 0;
            destY = 0;

            nPercent = 0;
            nPercentW = 0;
            nPercentH = 0;

            nPercentW = (Convert.ToDouble(newWidth) / Convert.ToDouble(sourceWidth));
            nPercentH = (Convert.ToDouble(newHeight) / Convert.ToDouble(sourceHeight));

            if ((nPercentH < nPercentW))
            {
                nPercent = nPercentW;
                destY = Convert.ToInt32(((newHeight - (sourceHeight * nPercent)) / 2));
            }
            else
            {
                nPercent = nPercentH;
                destX = Convert.ToInt32(((newWidth - (sourceWidth * nPercent)) / 2));
            }

            int destWidth = 0;
            int destHeight = 0;
            destWidth = Convert.ToInt32((sourceWidth * nPercent));
            destHeight = Convert.ToInt32((sourceHeight * nPercent));


            Bitmap bmPhoto = null;
            bmPhoto = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            Graphics grPhoto = null;
            grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            grPhoto.Dispose();
            bmPhoto.Save(nPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            

            bmPhoto.Dispose();
            imgPhoto.Dispose();
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
                        var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                        var s = HttpContext.Current.Server.MapPath(SavePath + newfilename);
                        newbmp.Save(s, image.RawFormat);
                        newbmp.Dispose();

                        try
                        {
                            SetFTP( HttpContext.Current.Server.MapPath(SavePath + filename) , SavePath, filename);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }
        public void SetCookie(User user)
        {
            HttpCookie AdminCookie = new HttpCookie("AdminCookies");
            AdminCookie.Values["UserName"] = user.UserName.ToString();
            AdminCookie.Values["Password"] = user.Password.ToString();
            AdminCookie.Values["ID"] = user.ID.ToString();
            HttpContext.Current.Response.SetCookie(AdminCookie);
        }
        public static int GetLoginUserID()
        {
            int ID = 0;
            HttpCookie cookie = HttpContext.Current.Request.Cookies["AdminCookies"];
            if (cookie != null)
            {
                ID = Convert.ToInt32(cookie.Values["ID"]);
            }
            return ID;
        }
        public void CheckAdminLogin()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["AdminCookies"];
            if (cookie == null || String.IsNullOrEmpty(Convert.ToString(cookie)))
            {
                HttpContext.Current.Response.Redirect("/User/Login");
            }
            else if (cookie.Values["Password"] == "-")
            {
                string url = "/User/LockScreen?ID=" + cookie.Values["ID"].ToString();
                HttpContext.Current.Response.Redirect(url);

            }

        }

        public SelectList CategoryList()
        {
            var CategoryList = new SelectList(new CategoryModel().GetAll().Select(x => new { x.ID, x.CategoryName }).OrderBy(x => x.CategoryName).ToList(), "ID", "CategoryName");
            return CategoryList;
        }

        //public SelectList ProductList()
        //{
        //    var productlst= new SelectList(new ProductModel().GetProducts().Select(x=>new{x.ID,x}) )
        //}

        public void EmailHostDetail(string toEmail, string fromEmail, string subject, string bodyHtml, string fileName)
        {
            //File Path
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Resources\\Files\\";

            //email sending code will appear here.
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();

            message.To.Add(new MailAddress(toEmail));

            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = bodyHtml;

            if (!String.IsNullOrEmpty(fileName))
            {

                //DOING ATTACHMENT:
                Attachment data =
                    new Attachment(filePath + fileName,
                        MediaTypeNames.Application.Octet);
                // Add time stamp information for the file.
                ContentDisposition disposition = data.ContentDisposition;
                // Add the file attachment to this e-mail message.
                message.Attachments.Add(data);
            }


            string hostname = "smtp.sendgrid.net";// "smtp.gmail.com";
            client.Host = hostname;
            client.Port = 587;
            client.EnableSsl = true;
            string username = "alipk3";
            string password = "Emailsend94";
            message.From = new MailAddress(fromEmail, "NBK");
            var basicCredentials = new System.Net.NetworkCredential(username, password);
            client.Credentials = basicCredentials;
            client.Send(message);
        }
        //public static void SendEmailSMPT(string ToEmail, string Body, string Subject)
        //{
        //    MailMessage message = new MailMessage();
        //    SmtpClient client = new SmtpClient();
        //    message.To.Add(new MailAddress(ToEmail));
        //    message.Subject = Subject;
        //    message.IsBodyHtml = true;
        //    message.Body = Body;
        //    message.From = new MailAddress(ConfigurationManager.AppSettings.Get("MailingAddress").ToString().Trim(), "StudyAbroad.pk");
        //    //string hostname = ConfigurationManager.AppSettings.Get("MailServerName").ToString().Trim();
        //    //client.Host = hostname;
        //    client.Host = "184.107.73.104";
        //    string username = ConfigurationManager.AppSettings.Get("MailingAddress").ToString().Trim();
        //    string password = "Pakistan@123"; //ConfigurationManager.AppSettings.Get("Password").ToString().Trim();
        //    //message.From = new MailAddress(username);
        //    System.Net.NetworkCredential basicCredentials = new System.Net.NetworkCredential(username, password);
        //    client.Credentials = basicCredentials;
        //    client.Port = 25;// 26;
        //    client.Send(message);
        //}
        public static void SendEmailSMPT(string ToEmail, string Body, string Subject)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            message.To.Add(new MailAddress(ToEmail));
            message.Subject = Subject;
            message.IsBodyHtml = true;
            message.Body = Body;
            //message.From = new MailAddress(ConfigurationManager.AppSettings.Get("MailingAddress").ToString().Trim(), "StudyAbroad.pk");
            message.From = new MailAddress("noreply@chapter2.pk", "Chapter2.pk");
            //string hostname = "184.107.73.104";
            client.Host = "91.205.175.115"; //"69.175.87.74";//;
            string username = "noreply@chapter2.pk";
            string password = "#aaqq0tw0ZtY4bnD";
            //message.From = new MailAddress(username);
            System.Net.NetworkCredential basicCredentials = new System.Net.NetworkCredential(username, password);
            client.Credentials = basicCredentials;
            client.Port = 25;//25//465;//26//587;
            client.Send(message);
        }

        public static async void SendEmailSMPTAsync(string ToEmail, string Body, string Subject)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            message.To.Add(new MailAddress(ToEmail));
            message.Subject = Subject;
            message.IsBodyHtml = true;
            message.Body = Body;
            //message.From = new MailAddress(ConfigurationManager.AppSettings.Get("MailingAddress").ToString().Trim(), "StudyAbroad.pk");
            message.From = new MailAddress("noreply@chapter2.pk", "Chapter2.pk");
            //string hostname = "184.107.73.104";
            client.Host = "91.205.175.115"; //"69.175.87.74";//;
            string username = "noreply@chapter2.pk";
            string password = "#aaqq0tw0ZtY4bnD";
            //message.From = new MailAddress(username);
            System.Net.NetworkCredential basicCredentials = new System.Net.NetworkCredential(username, password);
            client.Credentials = basicCredentials;
            client.Port = 25;//25//465;//26//587;
            await client.SendMailAsync(message);
        }
        public static string GetHmtlContentsFromDirectory(string FileName)
        {
            string Result = "";
            string FilePath = HttpContext.Current.Server.MapPath(FileName);
            System.IO.StreamReader MyFile = new System.IO.StreamReader(FilePath);
            Result = MyFile.ReadToEnd();
            MyFile.Close();
            MyFile.Dispose();
            return Result;
        }

        public static string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex("(?:[^a-zA-Z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }
        public static string GenerateUrl(string Url)
        {
            string UrlPeplaceSpecialWords = Regex.Replace(Url, @"&quot;|['"",&?%\.!()@$^_+=*:#/\\-]", " ").Trim();
            string RemoveMutipleSpaces = Regex.Replace(UrlPeplaceSpecialWords, @"\s+", " ");
            string ReplaceDashes = RemoveMutipleSpaces.Replace(" ", "-");
            string DuplicateDashesRemove = ReplaceDashes.Replace("--", "-");
            return DuplicateDashesRemove.ToLower();
        }
        public static string SaveFile(HttpPostedFileBase ImageFile, string Path, string FileName = "")
        {
            FileName = string.IsNullOrEmpty(FileName) ? Convert.ToString(DateTime.Now.Ticks) + ImageFile.FileName.Substring(ImageFile.FileName.IndexOf(".")) : FileName;
            string ImageServerPath = HttpContext.Current.Server.MapPath(Path + "/" + FileName);
            ImageFile.SaveAs(ImageServerPath);
            //try
            //{
            //    SetFTP(ImageServerPath, Path, FileName);
            //}
            //catch (Exception)
            //{

                
            //}

            return FileName;
        }
        public static void SetFTP(string path, string uploadpath, string FileName)
        {
            try
            {
                FTPBE be = new FTPBE();
                be.HostFilePath = path;
                be.TransferFilePath = uploadpath + FileName;
                be.FileName = FileName;
                bool ISDone = Silkways.Models.SilkwaysFunction.GernalFunction.FtpUploads.FtpUploadUsingFileUpload(be);
                //if (System.IO.File.Exists(path) == true)
                //{
                //    System.IO.File.Delete(path);
                //}
            }
            catch (Exception ex)
            {
            }
            
        }
        
        public class FTPBE
        {

            public string HostFilePath { get; set; }

            public string TransferFilePath { get; set; }

            public string FileName { get; set; }
            public bool DeleteFileAfterTransfer { get; set; }
            public FTPBE()
            {


            }
        }

        public static class FtpUploads
        {
            #region BasicSettings+SSL
            private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            }
            public static FtpWebRequest BasicSettings(FtpWebRequest ftp)
            {
                ftp.UseBinary = true;
                ftp.Proxy = null;
                ftp.KeepAlive = false;
                ftp.UsePassive = true;
                ftp.EnableSsl =true;
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);
                return ftp;
            }
            #endregion

            public static bool FtpUploadUsingFileUpload(FTPBE o)
            {
                FtpWebRequest ftp = null;
                //string url = HttpContext.Current.Server.MapPath(o.HostFilePath );

                try
                {
                    FtpInformationOthers FTP = new GernalFunction().FTPInformation;
                    string url = o.HostFilePath;
                    string domainname = FTP.Path + o.TransferFilePath;
                   

                    ftp = (FtpWebRequest)FtpWebRequest.Create(domainname);
                    ftp.Credentials = new NetworkCredential(FTP.ftpName, FTP.ftppassword);
                    ftp = BasicSettings(ftp);
                    ftp.EnableSsl = false;
                    ftp.Method = WebRequestMethods.Ftp.UploadFile;
                    FileInfo fileInfo = new FileInfo(url);
                    FileStream fileStream = fileInfo.OpenRead();

                    int bufferLength = 2048;
                    byte[] buffer = new byte[bufferLength];


                    Stream uploadStream = ftp.GetRequestStream();
                    int contentLength = fileStream.Read(buffer, 0, bufferLength);
                    try
                    {
                        while (contentLength != 0)
                        {
                            uploadStream.Write(buffer, 0, contentLength);
                            contentLength = fileStream.Read(buffer, 0, bufferLength);
                        }

                        uploadStream.Close();
                        fileStream.Close();

                        uploadStream.Dispose();
                        fileStream.Dispose();
                    }
                    catch
                    {
                        uploadStream.Dispose();
                        fileStream.Dispose();

                    }

                    ftp = null;
                    #region DelFile
                    try
                    {

                        if (o.DeleteFileAfterTransfer == true)
                        {
                            System.IO.File.Delete(HttpContext.Current.Server.MapPath(o.HostFilePath + o.FileName));
                        }
                    }
                    catch
                    {


                    }
                    #endregion


                    return true;
                }
                catch (WebException e)
                {
                    String status = ((FtpWebResponse)e.Response).StatusDescription; 
                    if (ftp!=null)
                    {
                        ftp.Abort();
                    }

                    return false;

                }
            }
            
        }
        FtpInformationOthers ftpInformation;
        public FtpInformationOthers FTPInformation
        {
            get {
                if (ftpInformation!=null)
                {
                    return ftpInformation;
                }
                else
                {
                    using (var ftpdb = new GaintShoeBoxEntities())
                    {
                        SystemSetting FTP = ftpdb.SystemSettings.FirstOrDefault();
                        if (FTP != null && !string.IsNullOrEmpty(FTP.FTPIP) && !string.IsNullOrEmpty(FTP.FtpName) && !string.IsNullOrEmpty(FTP.FtpPassword) && !string.IsNullOrEmpty(FTP.FTPPath))
                        {
                            FtpInformationOthers FP = new FtpInformationOthers();
                            FP.FTPIP = FTP.FTPIP;
                            FP.ftpName = FTP.FtpName;
                            FP.ftppassword = FTP.FtpPassword;
                            FP.Path = FTP.FTPPath;
                            ftpInformation = FP;
                        }
                    }
                    return ftpInformation;
                }
            }
        }

        public class FtpInformationOthers
        {
            public string FTPIP { get; set; }
            public string ftpName { get; set; }
            public string ftppassword { get; set; }
            public string Path { get; set; }
        }
    }
}