using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Izrune.Attributes;
using Izrune.Fragments.DialogFrag;
using Izrune.Helpers;
using IZrune.PCL;

namespace Izrune.Activitys
{
    abstract class MPDCBaseActivity : AppCompatActivity
    {
        ObjectAnimator scale1;
        ObjectAnimator scale2;

        private List<int> LoadingImageList = new List<int>()
        {
           
        };

        private ImageView image { get; set; }

        protected abstract int LayoutResource { get; }

        protected virtual FrameLayout MainFrame { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(LayoutResource);
            MapControls();
            AppInfo.Instance.CurrentContext = this;

            AlertService serv = new AlertService()
            {
                AlertEVent = (title, text) =>
                {
                    var transcation = FragmentManager.BeginTransaction();
                    warningDialogFragment dialog = new warningDialogFragment(title, text, true);
                    dialog.Show(transcation, "Image Dialog");
                },
                SacssesAler = (title, text) =>
                {
                    var transcation = FragmentManager.BeginTransaction();
                    warningDialogFragment dialog = new warningDialogFragment(title, text, false);
                    dialog.Show(transcation, "Image Dialog");
                }

            };
            AppCore.Instance.Alertdialog = serv;
            var currentView = FindViewById(Android.Resource.Id.Content);
            currentView.Focusable = true;
            currentView.FocusableInTouchMode = true;
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
                loadingFrame.SetBackgroundColor((transparent) ? Android.Graphics.Color.Argb(0, 0, 0, 0) : Android.Graphics.Color.Rgb(255, 255, 255));
                image = new ImageView(this)
                {
                    LayoutParameters = new FrameLayout.LayoutParams((int)System.Math.Round(80 * this.Resources.DisplayMetrics.Density), (int)System.Math.Round(80 * this.Resources.DisplayMetrics.Density), GravityFlags.Center)
                };
                image.SetImageResource(Resource.Drawable.logo);
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

        protected override void OnResume()
        {
            base.OnResume();
            AppInfo.Instance.CurrentContext = this;
        }

        private void MapControls()
        {
            Type type = GetType();
            type.GetRuntimeFields().Where(o => o.IsDefined(typeof(MapControlAttribute))).ForEach(f => {
                var attr = f.GetCustomAttribute<MapControlAttribute>();
                View theView = FindViewById(attr.Resource);
                f.SetValue(this, theView);
            });

            type.GetRuntimeProperties().Where(o => o.CanWrite && o.IsDefined(typeof(MapControlAttribute))).ForEach(p => {
                var attr = p.GetCustomAttribute<MapControlAttribute>();
                View theView = FindViewById(attr.Resource);
                p.SetValue(this, theView);
            });
        }

        protected void CloseKeyboard()
        {
            View view = this.CurrentFocus;
            if (view != null)
            {
                InputMethodManager imm = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(view.WindowToken, 0);
                view.ClearFocus();
            }
        }

        public void ChangeFragmentPage(Fragments.MPDCBaseFragment fragment, int layoutId)
        {
            Android.Support.V4.App.Fragment frag = null;
            int stackCount = SupportFragmentManager.BackStackEntryCount;
            for (int i = stackCount; i >= 0; i--)
            {
                SupportFragmentManager.PopBackStackImmediate();
            }

            SupportFragmentManager.Fragments.Clear();

            if (SupportFragmentManager.BackStackEntryCount > 0)
            {
                frag = SupportFragmentManager.Fragments.Last();
            }


            var transaction = SupportFragmentManager
                 .BeginTransaction()
                .Replace(layoutId, fragment)
                .AddToBackStack(null);

            if (frag != null)
                transaction = transaction.Hide(frag);

            transaction.Commit();
        }

    }
}