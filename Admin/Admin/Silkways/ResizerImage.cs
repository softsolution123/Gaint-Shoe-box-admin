using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace Silkways
{
    public class ResizerImage
    {

        public static void ResizePicture(int height, int width, string path, string filename, string SavePath, double scaleFactor, Stream sourcePath)
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
                        var thumbGraph = Graphics.FromImage(newbmp);
                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph.DrawImage(image, imageRectangle);
                        var s = HttpContext.Current.Server.MapPath(SavePath + newfilename);
                        newbmp.Save(s, image.RawFormat);
                        newbmp.Dispose();
                        //thumbnailImg.Save(targetPath, image.RawFormat);
                    }


                }
            }
        }
    }
}