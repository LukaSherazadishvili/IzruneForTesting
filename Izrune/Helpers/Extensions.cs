using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using FFImageLoading.Views;

namespace Izrune.Helpers
{
  public static  class Extensions
    {
        static ObjectAnimator scale1;
        static ObjectAnimator scale2;
        private static int HoroscopeIndex=0;
        private static ImageView image;
        private static List<int> MyHoroscopeList = new List<int>()
        {
           
        };


        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }

        public static void LoadImage(this ImageViewAsync imageView,string url ,bool smallPlaceholder = true)
        {

            imageView.SetImageResource(Android.Resource.Color.Transparent);

            ImageService.Instance
                .LoadUrl(url)
                .LoadingPlaceholder((!smallPlaceholder) ? "placeholderLarge" : "placeholderMinix", FFImageLoading.Work.ImageSource.CompiledResource)
                .ErrorPlaceholder((!smallPlaceholder) ? "placeholderLarge" : "placeholderMinix", FFImageLoading.Work.ImageSource.CompiledResource)
                .WithCache(FFImageLoading.Cache.CacheType.Disk)
                .DownSample(imageView.Width, imageView.Height, false)
                .DownSampleMode(FFImageLoading.Work.InterpolationMode.High)
                .CacheKey(url)
                .IntoAsync(imageView);
        }



        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
                action(item);

            return enumerable;
        }


        public static void Startloading(this FrameLayout view, Context context, bool transparent = false)
        {
            try
            {
                try
                {
                    scale1.AnimationEnd -= Scale1_AnimationEnd;
                    scale2.AnimationEnd -= Scale2_AnimationEnd;
                    scale1.End();
                    scale2.End();
                }
                catch (Exception) { }
                FrameLayout loadingFrame = new FrameLayout(context)
                {
                    LayoutParameters = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.MatchParent)
                };
                loadingFrame.Clickable = true;
                loadingFrame.SetBackgroundColor((transparent) ? Android.Graphics.Color.Argb(0, 0, 0, 0) : Android.Graphics.Color.Rgb(0, 0, 0));
                image = new ImageView(context)
                {
                    LayoutParameters = new FrameLayout.LayoutParams((int)System.Math.Round(80 * context.Resources.DisplayMetrics.Density), (int)System.Math.Round(80 * context.Resources.DisplayMetrics.Density), GravityFlags.Center)
                };
               // image.SetImageResource(MyHoroscopeList.ElementAt(HoroscopeIndex));
                scale1 = ObjectAnimator.OfPropertyValuesHolder(image,
                    PropertyValuesHolder.OfFloat("scaleX", 1.3f),
                    PropertyValuesHolder.OfFloat("scaleY", 1.3f),
                    PropertyValuesHolder.OfFloat("Alpha", 0.3f));
                scale1.SetDuration(800);
                scale2 = ObjectAnimator.OfPropertyValuesHolder(image,
                    PropertyValuesHolder.OfFloat("scaleX", 1.0f),
                    PropertyValuesHolder.OfFloat("scaleY", 1.0f),
                    PropertyValuesHolder.OfFloat("Alpha", 1.0f));
                scale2.SetDuration(800);
                scale2.AnimationEnd += Scale2_AnimationEnd;
                scale1.AnimationEnd += Scale1_AnimationEnd;
                loadingFrame.AddView(image);
                view.AddView(loadingFrame);
                scale1.Start();
            }
            catch (Exception)
            { }
        }

        private static void Scale1_AnimationEnd(object sender, EventArgs e)
        {
            try
            {
                
                scale2.Start();
            }
            catch (Exception) { }
        }

        private static void Scale2_AnimationEnd(object sender, EventArgs e)
        {
            try
            {
                if (HoroscopeIndex == 11)
                {
                    HoroscopeIndex = -1;
                }
                ++HoroscopeIndex;
                image.SetBackgroundResource(MyHoroscopeList.ElementAt(HoroscopeIndex));

                scale1.Start();
            }
            catch (Exception) { }
        }
        public static void StopLoading(this FrameLayout view, bool ClearChildren = false)
        {
            try
            {
                try
                {
                    scale1.AnimationEnd -= Scale1_AnimationEnd;
                    scale2.AnimationEnd -= Scale2_AnimationEnd;
                    scale1.End();
                    scale2.End();
                    scale1 = null;
                    scale2 = null;
                }
                catch (Exception) { }
                if ((ClearChildren) ? true : view.ChildCount > 1)
                    view.RemoveViewAt(view.ChildCount - 1);
            }
            catch (Exception) { }
        }
    }
}