using System;
using System.Text.RegularExpressions;
using Cavea.PCL.Utils;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Cavea.iOS.Utils
{
    public static class ExtentionHelper
    {
        public static UIColor RgbToUIColor(this Rgb rgb)
        {
            return UIColor.FromRGB((byte)rgb.Red, (byte)rgb.Green, (byte)rgb.Blue);
        }

        public static float GetWidthByText(this string text, UIFont font)
        {
            Foundation.NSString nsString = new Foundation.NSString(text);
            UIStringAttributes attribs = new UIStringAttributes { Font = font };
            var size = nsString.GetSizeUsingAttributes(attribs);

            return (float)size.Width;
        }

        public static CGSize  GetSizeByText(this string text, UIFont font)
        {
            Foundation.NSString nsString = new Foundation.NSString(text);
            UIStringAttributes attribs = new UIStringAttributes { Font = font };
            var size = nsString.GetSizeUsingAttributes(attribs);

            return size;
        }

        public static UIFont AppFontOfSize(nfloat size) {

            return UIFont.FromName("BPG Mrgvlovani Caps 2010", size);
        }

        public static UIFont AppFontOfSizeRegular(nfloat size)
        {

            return UIFont.FromName("BPG Arial", size);
        }

    }
}
