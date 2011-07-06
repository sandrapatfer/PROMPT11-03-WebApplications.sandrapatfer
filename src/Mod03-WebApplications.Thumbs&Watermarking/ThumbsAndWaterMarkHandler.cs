// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThumbsAndWaterMarkHandler.cs" company="CCISEL">
//   Centro de Cálculo do Instituto Superior de Engenharia de Lisboa - CCISEL  2011
// </copyright>
// <summary>
//   This class implements an image thumbnails and watermarking handler. It receives the
//   following case named Parameters from the request connection string:
//   ImageId -&amp;gt; The image id.
//   Width   -&amp;gt; Optional. The width of the returned image.
//   Height  -&amp;gt; Optional. The height of the returned image.
//   ImageFormat  -&amp;gt; Optional. The mime subtype image format to return (the type is always image/).
//   E.g: jpeg returns a watermarked image in Jpeg format.
//   This handler uses a &lt;see cref="IThumbsAndWatermarkingConfig" /&gt; to obtain
//   output configuration data (watermark text, font, error image, etc), and
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mod03_WebApplications.ThumbsAndWatermarking {
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.Web;

    using Mod03_WebApplications.ThumbsAndWatermarking.Config;
    using Mod03_WebApplications.ThumbsAndWatermarking.ImageStore;

    /// <summary>
    /// This class implements an image thumbnails and watermarking handler. It receives the
    /// following case named Parameters from the request connection string:
    /// ImageId -> The image id.
    /// Width   -> Optional. The width of the returned image. 
    /// Height  -> Optional. The height of the returned image.
    /// ImageFormat  -> Optional. The mime subtype image format to return (the type is always image/). 
    ///            E.g: jpeg returns a watermarked image in Jpeg format.
    /// This handler uses a <see cref="IThumbsAndWatermarkingConfig"/> to obtain
    /// output configuration data (watermark text, font, error image, etc), and
    /// </summary>
    /// <remarks>This handler only supprts HTTP GET requests. If an invalid request is made,
    /// because of wrong HTTP method or incorrect parameters an error image 
    /// is returned watermarked by the error description.
    /// </remarks>
    public class ThumbsAndWaterMarkHandler : IHttpHandler
    {
        #region Private Fields

        /// <summary>
        /// The <see cref="IThumbsAndWatermarkingConfig"/> instance to configure the handler.
        /// </summary>
        private readonly IThumbsAndWatermarkingConfig _wmHandlerConfig = new ThumbsAndWatermarkingStaticConfig();

        /// <summary>
        /// The <see cref="ThumbsHandlerArguments"/> instance.
        /// </summary>
        //private readonly ThumbsHandlerArguments _wmHandlerArguments = new ThumbsHandlerArguments();
        private ThumbsHandlerArguments _wmHandlerArguments
        {
            get
            {
                if (HttpContext.Current.Items["_wmHandlerArguments"] == null)
                {
                    HttpContext.Current.Items.Add("_wmHandlerArguments", new ThumbsHandlerArguments());
                }
                return (ThumbsHandlerArguments)HttpContext.Current.Items["_wmHandlerArguments"];
            }
        }

        /// <summary>
        /// The <see cref="IImageStore"/> instance to get images.
        /// </summary>
        private readonly IImageStore _wmHandlerImageStore = new ImageStoreFileSystem();
        #endregion Private Fields

        #region Interface IHttpHandler
        /// <summary>
        /// Gets a value indicating whether the handler IsReusable.
        /// </summary>
        public bool IsReusable 
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the IImageStore.
        /// </summary>
        public IImageStore ImageStore 
        {
            get { return _wmHandlerImageStore; }
        }

        /// <summary>
        /// Gets the <see cref="IThumbsAndWatermarkingConfig"/>.
        /// </summary>
        public IThumbsAndWatermarkingConfig ThumbsAndWatermarkingConfig 
        {
            get { return _wmHandlerConfig; }
        }

        /// <summary>
        /// The handler <see cref="IHttpHandler.ProcessRequest"/>
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public void ProcessRequest(HttpContext context) 
        {
            int statusCode;
            if ((statusCode = EvaluateArguments(context)) != 200) {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = statusCode;
                return;
            }

            Image outImg;
            using (Image img = _wmHandlerImageStore.GetImage(_wmHandlerArguments.ImageId, context)) {
                outImg = img;
                if (_wmHandlerConfig.ThumbnailsEnabled) {
                    outImg = GenerateThumbnail(img);
                }

                if (_wmHandlerConfig.WatermarkEnabled) {
                    GenerateWatermark(outImg);
                }

                outImg.Save(context.Response.OutputStream, _wmHandlerArguments.ImageFormat);
                outImg.Dispose();
            }

            context.Response.ContentType = "image/" + _wmHandlerArguments.ImageFormat;
            //context.Response.CacheControl = "public";
            context.Response.Cache.SetCacheability(HttpCacheability.Server);
            context.Response.Cache.SetValidUntilExpires(true);
            context.Response.Cache.SetExpires(DateTime.Now.AddHours(2));
        }
        #endregion

        #region Internal Helper Methods

        /// <summary>
        /// Evaluates the arguments received in the connection string and fills <see cref="_wmHandlerArguments"/>. 
        /// It returns the HTTP <see cref="HttpResponse.StatusCode"/> to return to client.
        /// If everithing in the request is ok, 200 is returned. Returns 405 for an 
        /// invalid request method. Otherwhise returns 405, meaning that there are some
        /// invalid argument. When an error ocurrs, <see cref="ThumbsHandlerArguments.ErrorString"/> is set
        /// with the error description.
        /// </summary>
        /// <param name="ctx">The <see cref="HttpContext"/> to get the received Arguments.</param>
        /// <returns>It returns the HTTP <see cref="HttpResponse.StatusCode"/> to return to the client.
        /// If everithing in the request is ok, 200 is returned. Returns 405 for an 
        /// invalid request method. Otherwhise returns 405, meaning that there are some
        /// invalid parameters.
        /// </returns>
        private short EvaluateArguments(HttpContext ctx) {
            HttpRequest req = ctx.Request;

            // Check if the request method is GET
            if (req.RequestType != "GET") {
                _wmHandlerArguments.ErrorString = "Invalid HTTP request method.";
                return 405;
            }

            // Let's parse the ImageId argument
            string fieldValue = req.QueryString["ImageId"];
            if (String.IsNullOrEmpty(fieldValue)) {
                _wmHandlerArguments.ErrorString = "Invalid or missing 'ImageId'";
                return 400;
            }

            // Image Id is OK. Set the correnponding field.                    
            _wmHandlerArguments.ImageId = fieldValue;

            short retCode;

            // Let's parse the Width argument
            if ((retCode = ParseIntField("Width", _wmHandlerConfig.MaxWidth, out _wmHandlerArguments.Width)) != 200) {
                return retCode;
            }

            // Let's parse the Height argument
            if ((retCode = ParseIntField("Height", _wmHandlerConfig.MaxHeight, out _wmHandlerArguments.Height)) != 200) {
                return retCode;
            }

            // Let's parse the Format argument
            fieldValue = req.QueryString["Format"];
            if (String.IsNullOrEmpty(fieldValue)) {
                _wmHandlerArguments.ImageFormat = _wmHandlerConfig.ImageFormat;
            }
            else 
            {
                try 
                {
                    _wmHandlerArguments.ImageFormat = (ImageFormat)Enum.Parse(typeof(ImageFormat), fieldValue);
                } 
                catch (ArgumentException) 
                {
                    _wmHandlerArguments.ErrorString = "Invalid ImageFormat";
                    return 400;
                }
            }

            return 200;
        }

        /// <summary>
        /// Parses an the given QueryString <paramref name="fieldName"/> value as an integer.
        /// If the field does not exist or is not convertible to an integar, returns status code 400,
        /// otherwise returns 200. 
        /// </summary>
        /// <param name="fieldName">
        /// The field name.
        /// </param>
        /// <param name="maxOrDefaultValue">
        /// The max or default value.
        /// </param>
        /// <param name="field">
        /// The field.
        /// </param>
        /// <returns>
        /// </returns>
        private short ParseIntField(string fieldName, int maxOrDefaultValue, out int field) {
            HttpRequest req = HttpContext.Current.Request;
            field = -1;
            string fieldValue = req.QueryString[fieldName];
            if (String.IsNullOrEmpty(fieldValue)) {
                field = maxOrDefaultValue;
            }
            else {
                try 
                {
                    field = Math.Min(int.Parse(fieldValue), maxOrDefaultValue);
                }
                catch (FormatException) 
                {
                    _wmHandlerArguments.ErrorString = "Invalid " + fieldName;
                    return 400;
                }
                catch (OverflowException) 
                {
                    _wmHandlerArguments.ErrorString = "Invalid " + fieldName;
                    return 400;
                }
            }

            return 200;
        }

        /// <summary>
        /// Generates the thumbnail based on <see cref="_wmHandlerArguments.Width"/> and <see cref="_wmHandlerArguments.Height"/>
        /// but maitaining original image proportionality.
        /// </summary>
        /// <param name="img">The img to generate the thumbnail.</param>
        /// <returns>The generated thumbnail</returns>
        private Image GenerateThumbnail(Image img) 
        {
            int sourceWidth = img.Width;
            int sourceHeight = img.Height;

            int destWidth = _wmHandlerArguments.Width;
            int destHeight = _wmHandlerArguments.Height;

            float imageProportion = (float)img.Width / img.Height;
            if (imageProportion > 1) 
            {
                // Image is landscape. Width remains. Height must be changed proportionally
                destHeight = (int)(_wmHandlerArguments.Width / imageProportion);
            }
            else 
            {
                // Image is portrait. Height remains. Width must be changed proportionally
                destWidth = (int)(_wmHandlerArguments.Height * imageProportion);
            }

            Bitmap outBmp = new Bitmap(destWidth, destHeight, img.PixelFormat);
            outBmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (Graphics graphicsPhoto = Graphics.FromImage(outBmp)) 
            {
                graphicsPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsPhoto.DrawImage(
                                  img,
                                  new Rectangle(0, 0, destWidth, destHeight),
                                  new Rectangle(0, 0, sourceWidth, sourceHeight),
                                  GraphicsUnit.Pixel);
                img.Dispose();
                return outBmp;
            }
        }

        /// <summary>
        /// Generates the watermark based on the <see cref="IThumbsAndWatermarkingConfig"/>
        /// properties related to Watermarking.
        /// </summary>
        /// <param name="img">The img whwre the watermark is generated.</param>
        private void GenerateWatermark(Image img) {
            using (Graphics g = Graphics.FromImage(img)) {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                Font f = _wmHandlerConfig.GetFont(img.Width, img.Height);

                int textWidth = (int)g.MeasureString(_wmHandlerConfig.Text, f).Width;

                WatermarkPosition position = _wmHandlerConfig.WatermarkTextPosition;

                Point watermarkPosition = CalculatePosition(img, f, position, textWidth);

                g.DrawString(
                             _wmHandlerConfig.Text,
                             f,
                             new SolidBrush(_wmHandlerConfig.WatermarkColor),
                             watermarkPosition.X,
                             watermarkPosition.Y);

                // g.DrawString(this.GetHashCode().ToString(),
                //  new Font("Arial Black", fontSize, FontStyle.Bold, GraphicsUnit.Pixel),
                //  new SolidBrush(Color.FromArgb(150, Color.White)), xPos, yPos + fontSize);
            }

            return;
        }

        /// <summary>
        /// Calculates and returns the <see cref="Point"/> of the Watermark position.
        /// </summary>
        /// <param name="img">
        /// The img.
        /// </param>
        /// <param name="f">
        /// The <see cref="Font"/> to write the watermark.
        /// </param>
        /// <param name="position">
        /// The <see cref="WatermarkPosition"/> flags pretended position.
        /// </param>
        /// <param name="textWidth">
        /// The text width.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/> of the Watermark position.
        /// </returns>
        private static Point CalculatePosition(Image img, Font f, WatermarkPosition position, int textWidth)
        {
            Point watermarkPosition = new Point();

            // Calculate yPos
            if ((position & WatermarkPosition.TOP) != 0) {
                // Define yPos as 0
                watermarkPosition.Y = 0;
            }
            else if ((position & WatermarkPosition.BOTTOM) != 0) {
                // Define yPos as Image Height - Font Height
                watermarkPosition.Y = img.Height - f.Height;
            }
            else 
            {
                // Define yPos vertically Centered
                watermarkPosition.Y = (img.Height / 2) - (f.Height / 2);
            }

            // Calculate xPos
            if ((position & WatermarkPosition.LEFT) != 0) 
            {
                // Define xPos as 0
                watermarkPosition.X = 0;
            }
            else if ((position & WatermarkPosition.RIGHT) != 0) 
            {
                // Define xPos as Image Width - Text Width
                watermarkPosition.X = img.Width - textWidth;
            }
            else 
            {
                // Define xPos horizontally Centered
                watermarkPosition.X = (img.Width / 2) - (textWidth / 2);
            }

            return watermarkPosition;
        }

        #endregion Internal Helper Methods
        /// <summary>
        /// This is an internal class that evaluates the <see cref="ThumbsAndWaterMarkHandler"/> 
        /// connection string arguments related with the thumbnail generation and makes them available 
        /// strongly typed.
        /// </summary>
        private class ThumbsHandlerArguments {
            /// <summary>
            /// Gets or sets the image id.
            /// </summary>
            /// <value>The image id.</value>
            public string ImageId;

            /// <summary>
            /// Gets or sets the desired image width.
            /// </summary>
            /// <value>The width.</value>
            /// <remarks>
            /// The request query string argument corresponding to this value is optional.
            /// If none, invalid or a value greater than <see cref="ThumbsAndWatermarkingStaticConfig.MaxWidth"/>
            /// is received it is set with the <see cref="ThumbsAndWatermarkingStaticConfig.MaxWidth"/> value.
            /// </remarks>
            public int Width;

            /// <summary>
            /// Gets or sets the desired image height.
            /// </summary>
            /// <value>The height.</value>
            /// /// <remarks>
            /// The request query string argument corresponding to this value is optional.
            /// If none, invalid or a value greater than <see cref="ThumbsAndWatermarkingStaticConfig.MaxHeight"/>
            /// is received it is set with the <see cref="ThumbsAndWatermarkingStaticConfig.MaxHeight"/> value.
            /// </remarks>
            public int Height;

            /// <summary>
            /// Gets or sets the desired returned image format.
            /// </summary>
            /// <value>The image format.</value>
            public ImageFormat ImageFormat;

            /// <summary>
            /// Gets or sets the error string. This string is not null when there is an error
            /// accessing the handler because of invalid request method or arguments.
            /// </summary>
            /// <value>The error string.</value>
            public string ErrorString;
        }
    }
}