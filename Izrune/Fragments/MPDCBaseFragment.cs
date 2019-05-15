using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Android.Support.V4.App;
using Android.Animation;
using Izrune.Helpers;
using Izrune.Attributes;

namespace Izrune.Fragments
{
   abstract class MPDCBaseFragment: Android.Support.V4.App.Fragment
    {
        protected abstract int LayoutResource { get; }

        ObjectAnimator scale1;
        ObjectAnimator scale2;

        private List<int> LoadingImageList = new List<int>()
        {
            
        };

        private ImageView image { get; set; }

        protected virtual FrameLayout MainFrame { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(LayoutResource, container, false);
            MapControl(view);
            return view;
        }

        protected void Startloading(bool transparent = false)
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
                FrameLayout loadingFrame = new FrameLayout(this)
                {
                    LayoutParameters = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.MatchParent)
                };
                loadingFrame.Clickable = true;
                loadingFrame.SetBackgroundColor((transparent) ? Android.Graphics.Color.Argb(0, 0, 0, 0) : Android.Graphics.Color.Rgb(0, 0, 0));
                image = new ImageView(this)
                {
                    LayoutParameters = new FrameLayout.LayoutParams((int)System.Math.Round(80 * this.Resources.DisplayMetrics.Density), (int)System.Math.Round(80 * this.Resources.DisplayMetrics.Density), GravityFlags.Center)
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
                MainFrame.AddView(loadingFrame);
                scale1.Start();
            }
            catch (Exception)
            { }
        }

        private void Scale1_AnimationEnd(object sender, EventArgs e)
        {
            try
            {

                scale2.Start();
            }
            catch (Exception) { }
        }

        private void Scale2_AnimationEnd(object sender, EventArgs e)
        {
            try
            {
                if (HoroscopeIndex == 11)
                {
                    HoroscopeIndex = -1;
                }
                ++HoroscopeIndex;
                image.SetBackgroundResource(LoadingImageList.ElementAt(HoroscopeIndex));

                scale1.Start();
            }
            catch (Exception) { }
        }

        private int HoroscopeIndex = 0;

        public void StopLoading(bool ClearChildren = false)
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
                if ((ClearChildren) ? true : MainFrame.ChildCount > 1)
                    MainFrame.RemoveViewAt(MainFrame.ChildCount - 1);
            }
            catch (Exception) { }
        }

        protected void CloseKeyboard()
        {
            View view = Activity.CurrentFocus;
            if (view != null)
            {
                InputMethodManager imm = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(view.WindowToken, 0);
                view.ClearFocus();
            }
        }

        private void MapControl(View rootView)
        {
            Type type = GetType();
            type.GetRuntimeFields().Where(o => o.IsDefined(typeof(MapControlAttribute))).ForEach(f => {
                var attr = f.GetCustomAttribute<MapControlAttribute>();
                View theView = rootView.FindViewById(attr.Resource);
                f.SetValue(this, theView);
            });

            type.GetRuntimeProperties().Where(o => o.CanWrite && o.IsDefined(typeof(MapControlAttribute))).ForEach(p => {
                var attr = p.GetCustomAttribute<MapControlAttribute>();
                View theView = rootView.FindViewById(attr.Resource);
                p.SetValue(this, theView);
            });
        }

        public void ChangeFragmentPage(Fragments.MPDCBaseFragment fragment, int layoutId, bool clearStack = true, bool clearPrevious = false, bool FadeInFadeOut = false)
        {
            Android.Support.V4.App.Fragment frag = null;
            if (clearStack)
            {
                int stackCount = ChildFragmentManager.BackStackEntryCount;
                for (int i = stackCount; i > 0; i--)
                {
                    ChildFragmentManager.PopBackStackImmediate();
                }
                ChildFragmentManager.Fragments.Clear();
            }
            else if (clearPrevious)
            {
                ChildFragmentManager.PopBackStackImmediate();
            }
            if (ChildFragmentManager.BackStackEntryCount > 0)
            {
                frag = ChildFragmentManager.Fragments.Last();
            }

            var transaction = ChildFragmentManager
                 .BeginTransaction()
                .Add(layoutId, fragment)
                .AddToBackStack(null);
           
            transaction.CommitAllowingStateLoss();
        }

        public static implicit operator Android.App.Activity(MPDCBaseFragment fragment)
        {
            return fragment.Activity;
        }

        public static implicit operator Context(MPDCBaseFragment fragment)
        {
            return fragment.Context;
        }

    }
}