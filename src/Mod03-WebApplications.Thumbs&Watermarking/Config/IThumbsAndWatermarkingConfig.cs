// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IThumbsAndWatermarkingConfig.cs" company="CCISEL">
//   Centro de Cálculo do Instituto Superior de Engenharia de Lisboa - PROMPT 2011
// </copyright>
// <summary>
//   Defines the IThumbsAndWatermarkingConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mod03_WebApplications.ThumbsAndWatermarking.Config
{
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>
    /// Configuration interface for <see cref="ThumbsAndWaterMarkHandler"/>.
    /// </summary>
    public interface IThumbsAndWatermarkingConfig {
        /// <summary>
        /// Gets the watermark text. If the returned value is null or an Empty
        /// String, means that the handler should not put any watermarking text.
        /// </summary>
        /// <value>The watermark text.</value>
        string Text { get; }

        /// <summary>
        /// Gets the watermark text position.
        /// </summary>
        /// <value>The watermark text position.</value>
        WatermarkPosition WatermarkTextPosition { get; }

        /// <summary>
        /// Gets the color of the watermark.
        /// </summary>
        /// <value>The color of the watermark.</value>
        Color WatermarkColor { get; }

        /// <summary>
        /// Gets the response image format.
        /// </summary>
        /// <value>The response image format.</value>
        ImageFormat ImageFormat { get; }

        /// <summary>
        /// Gets the invalid request image. 
        /// This is the image to return when an invalid request is made to the handler.
        /// </summary>
        /// <value>The invalid request image.</value>
        Image InvalidRequestImage { get; }

        /// <summary>
        /// Gets the maximum image width.
        /// </summary>
        /// <value>>The maximum image width.</value>
        int MaxWidth { get; }

        /// <summary>
        /// Gets the maximum image height.
        /// </summary>
        /// <value>>The maximum image height.</value>
        int MaxHeight { get; }

        /// <summary>
        /// Gets a value indicating whether thumnails is enabled.
        /// </summary>
        /// <value><c>true</c> if thumnails is enabled; otherwise, <c>false</c>.</value>
        bool ThumbnailsEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether watermarking is enabled.
        /// </summary>
        /// <value><c>true</c> if watermarking is enabled; otherwise, <c>false</c>.</value>
        bool WatermarkEnabled { get; }

        /// <summary>
        /// Gets the font for the specified <paramref name="width"/> and <paramref name="height"/>.
        /// </summary>
        /// <param name="width">The image width.</param>
        /// <param name="height">The image height.</param>
        /// <returns>The font for the specified <paramref name="width"/> and <paramref name="height"/> 
        /// </returns>
        Font GetFont(int width, int height);
    }
}