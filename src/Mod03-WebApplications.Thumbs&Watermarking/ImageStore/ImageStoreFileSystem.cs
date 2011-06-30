// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageStoreFileSystem.cs" company="CCISEL">
//   Centro de Cálculo do Instituto Superior de Engenharia de Lisboa - PROMPT 2011
// </copyright>
// <summary>
//   Defines the ImageStoreFileSystem type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Mod03_WebApplications.ThumbsAndWatermarking.ImageStore
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// An implementation of <see cref="IImageStore"/> that reads images from the filesystem.
    /// </summary>
    public class ImageStoreFileSystem : IImageStore
    {
        /// <summary>
        /// Gets the base directory of the Image Store Location.
        /// </summary>
        private static String ImageStoreLocation
        {
            get
            {
                return "~/images/";
            }
        }

        /// <summary>
        /// Gets the image corresponding to the given <paramref name="imageId"/>.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <param name="ctx">The current <see cref="HttpContext"/>.</param>
        /// <returns></returns>
        public Image GetImage(string imageId, HttpContext ctx)
        {
            string filesDir = ctx.Server.MapPath(ImageStoreLocation);
            string filename = (from file in Directory.GetFiles(filesDir)
                               where file.Contains(imageId)
                               select file).First();
            return Image.FromFile(filename);
        }
    }
}