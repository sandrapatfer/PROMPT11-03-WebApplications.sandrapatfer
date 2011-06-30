// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IImageStore.cs" company="CCISEL">
//   Centro de Cálculo do Instituto Superior de Engenharia de Lisboa - CCISEL  2011
// </copyright>
// <summary>
//   This interface represents the image store.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Mod03_WebApplications.ThumbsAndWatermarking.ImageStore
{
    using System.Drawing;
    using System.Web;

    /// <summary>
    /// This interface represents the image store. 
    /// </summary>
    public interface IImageStore {
        /// <summary>
        /// Gets the image corresponding to the given <paramref name="imageId"/>.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <param name="ctx">The current <see cref="HttpContext"/>.</param>
        /// <returns></returns>
        Image GetImage(string imageId, HttpContext ctx);
    }
}