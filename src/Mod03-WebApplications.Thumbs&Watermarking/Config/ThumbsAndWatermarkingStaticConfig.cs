// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThumbsAndWatermarkingStaticConfig.cs" company="CCISEL">
//   Centro de Cálculo do Instituto Superior de Engenharia de Lisboa - PROMPT 2011
// </copyright>
// <summary>
//   Defines the ThumbsAndWatermarkingStaticConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mod03_WebApplications.ThumbsAndWatermarking.Config
{
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>
    /// One sttaic implemebtation of <see cref="IThumbsAndWatermarkingConfig"/>.
    /// </summary>
    public class ThumbsAndWatermarkingStaticConfig : IThumbsAndWatermarkingConfig
    {
        /// <summary>
        /// Gets the watermark text. If the returned value is null or an Empty
        /// String, means that the handler should not put any watermarking text.
        /// </summary>
        /// <value>The watermark text.</value>
        public string Text
        {
            get { return "SLB"; }
        }

        /// <summary>
        /// Gets the watermark text position.
        /// </summary>
        /// <value>The watermark text position.</value>
        public WatermarkPosition WatermarkTextPosition
        {
            get
            {
                return WatermarkPosition.CENTER;
            } 
        }

        /// <summary>
        /// Gets the color of the watermark.
        /// </summary>
        /// <value>The color of the watermark.</value>
        public Color WatermarkColor
        {
            get { return Color.FromArgb(100, Color.Red); }
        }

        /// <summary>
        /// Gets the response image format.
        /// </summary>
        /// <value>The response image format.</value>
        public ImageFormat ImageFormat
        {
            get { return ImageFormat.Jpeg; }
        }

        /// <summary>
        /// Gets the invalid request image. 
        /// This is the image to return when an invalid request is made to the handler.
        /// </summary>
        /// <value>The invalid request image.</value>
        public Image InvalidRequestImage
        {
            get { throw new System.NotImplementedException(); }
        }

        /// <summary>
        /// Gets the maximum image width.
        /// </summary>
        /// <value>>The maximum image width.</value>
        public int MaxWidth
        {
            get { return 800; }
        }

        /// <summary>
        /// Gets the maximum image height.
        /// </summary>
        /// <value>>The maximum image height.</value>
        public int MaxHeight
        {
            get { return 600; }
        }

        /// <summary>
        /// Gets a value indicating whether thumnails is enabled.
        /// </summary>
        /// <value><c>true</c> if thumnails is enabled; otherwise, <c>false</c>.</value>
        public bool ThumbnailsEnabled
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether watermarking is enabled.
        /// </summary>
        /// <value><c>true</c> if watermarking is enabled; otherwise, <c>false</c>.</value>
        public bool WatermarkEnabled
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the font for the specified <paramref name="width"/> and <paramref name="height"/>.
        /// </summary>
        /// <param name="width">The image width.</param>
        /// <param name="height">The image height.</param>
        /// <returns>The font for the specified <paramref name="width"/> and <paramref name="height"/> 
        /// </returns>
        public Font GetFont(int width, int height)
        {
            return new Font("Arial Black", (int)(width / 8), FontStyle.Bold, GraphicsUnit.Pixel);
        }
    }
}