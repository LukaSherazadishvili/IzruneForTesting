using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CoreGraphics;
using Foundation;
using UIKit;

namespace MPDC.iOS.Utils
{
    public static class ExtentionHelper
    {

        public static float GetWidthByText(this string text, UIFont font)
        {
            Foundation.NSString nsString = new Foundation.NSString(text);
            UIStringAttributes attribs = new UIStringAttributes { Font = font };
            var size = nsString.GetSizeUsingAttributes(attribs);

            return (float)size.Width;
        }

        public static void ShowConnectionAlert(this UIViewController uIViewController)
        {
            var alert = UIAlertController.Create("შეცდომა", "შეამოწმეთ ინტერნეტთან კავშირი", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("დახურვა", UIAlertActionStyle.Default, null));
            uIViewController.PresentViewController(alert, true, null);
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
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

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static float GetStringHeight(this string text, float width, float margins, int fontSize)
        {
            var nsText = new NSString(text);
            var constarinedRect = new CGSize(width - margins, nfloat.MaxValue);

            var rect = nsText.GetBoundingRect(constarinedRect, NSStringDrawingOptions.UsesLineFragmentOrigin, new UIStringAttributes { Font = AppFontOfSize(fontSize) }, null);

            var height = Math.Ceiling(rect.Height);

            return (float)height;
        }

        public static NSDate DateTimeToNSDate(this DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
                date = DateTime.SpecifyKind(date, DateTimeKind.Local);
            return (NSDate)date;
        }

        public static DateTime NSDateToDateTime(this NSDate date)
        {
            return ((DateTime)date).ToLocalTime();
        }

        public static bool IsEmtyOrNull(this string text)
        {
            return (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text));
        }

    }
}
