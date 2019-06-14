using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Izrune.Activitys;
using Izrune.Adapters.ViewPagerAdapter;
using Izrune.Attributes;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Helpers;
using MpdcContainer = ServiceContainer.ServiceContainer;
namespace Izrune.Fragments
{
    class ServiceFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.layoutServices;


        [MapControl(Resource.Id.backgroundAnimationView)]
        View AnimatedView;

        [MapControl(Resource.Id.MainPager)]
        ViewPager pager;

        [MapControl(Resource.Id.IndividualButton)]
        LinearLayout IndividualButton;

        [MapControl(Resource.Id.PromoButton)]
        LinearLayout PromoButton;

        [MapControl(Resource.Id.PromoTxt)]
        TextView PromoText;

        [MapControl(Resource.Id.IndividualTxt)]
        TextView Individual;

        [MapControl(Resource.Id.BodyContent)]
        LinearLayout Body;

        public int CurrentId { get; set; }


        private List<MPDCBaseFragment> FragmentList = new List<MPDCBaseFragment>();
       

        float Density;
        private bool CurrentFragmnet = true;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           
        }

        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);



            var Result =MpdcContainer.Instance.Get<IUserServices>().GetPromoCodeAsync(CurrentId);
            var Individualserv = MpdcContainer.Instance.Get<IUserServices>().GetPromoCodeAsync(0);

            await Task.WhenAll(Result, Individualserv);

            var Individual = new IndividualServiceFragmentcs(Individualserv.Result.Prices.ToList());
            var Promo = new PromoFragment(Result.Result);
            FragmentList.Add(Individual);
            FragmentList.Add(Promo);
            Density = Resources.DisplayMetrics.Density;
            PromoButton.Click += PromoButton_Click;
            IndividualButton.Click += IndividualButton_Click;


            var adapter = new ServiceViewPagerAdapter(ChildFragmentManager, FragmentList);
            pager.Adapter = adapter;
            pager.PageSelected += Pager_PageSelected;


        }

        private void Pager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            if (e.Position == 1)
            {
                PromoButton_Click(null, null);
            }
            else
            {
                IndividualButton_Click(null, null);
            }
        }

        private void IndividualButton_Click(object sender, EventArgs e)
        {
            if (CurrentFragmnet)
                return;


            ObjectAnimator animation = ObjectAnimator.OfFloat(AnimatedView, "x", IndividualButton.GetX());
            animation.SetDuration(200);
            animation.Start();
            pager.PageSelected -= Pager_PageSelected;
            pager.SetCurrentItem(0, true);
            pager.PageSelected += Pager_PageSelected;
            Individual.SetTextColor(Android.Graphics.Color.Rgb(255, 255, 255));
            PromoText.SetTextColor(Android.Graphics.Color.Rgb(106, 106, 106));
            CurrentFragmnet = true;
        }

        private void PromoButton_Click(object sender, EventArgs e)
        {
            if (!CurrentFragmnet)
                return;


            ObjectAnimator animation = ObjectAnimator.OfFloat(AnimatedView, "x", PromoButton.GetX() + Density);
            animation.SetDuration(200);
            animation.Start();
            pager.PageSelected -= Pager_PageSelected;
            pager.SetCurrentItem(1, true);
            pager.PageSelected += Pager_PageSelected;
            Individual.SetTextColor(Android.Graphics.Color.Rgb(106, 106, 106));
            PromoText.SetTextColor(Android.Graphics.Color.Rgb(255, 255, 255));
            CurrentFragmnet = false;
        }
    }
}