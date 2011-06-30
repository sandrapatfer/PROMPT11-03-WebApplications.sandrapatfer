// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WatermarkPosition.cs" company="CCISEL">
//   Centro de Cálculo do Instituto Superior de Engenharia de Lisboa - PROMPT 2011
// </copyright>
// <summary>
//   Specifies the position of the watermark text. With contant flags several combinations
//   can be made, eg: TOP | LEFT; TOP| RIGHT, etc. There are invalid combinations, where
//   values from the same axis are given in a flagged constant. In this case the behavior
//   is undefined: TOP|BOTTOM.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Mod03_WebApplications.ThumbsAndWatermarking.Config
{
    using System;

    /// <summary>
    /// Specifies the position of the watermark text. With contant flags several combinations
    /// can be made, eg: TOP | LEFT; TOP| RIGHT, etc. There are invalid combinations, where 
    /// values from the same axis are given in a flagged constant. In this case the behavior 
    /// is undefined: TOP|BOTTOM.
    /// </summary>
    [Flags]
    public enum WatermarkPosition
    {
        /// <summary>
        /// The watermark position is at the top of the image.
        /// </summary>
        TOP = 1,

        /// <summary>
        /// The watermark position is bottom of the image.
        /// </summary>
        BOTTOM = 2,

        /// <summary>
        /// The watermark position is left of the image.
        /// </summary>
        LEFT = 4,

        /// <summary>
        /// The watermark position is right of the image.
        /// </summary>
        RIGHT = 8,

        /// <summary>
        /// The watermark position is center of the image.
        /// </summary>
        CENTER = 16
    }
}