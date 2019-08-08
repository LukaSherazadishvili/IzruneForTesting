using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Izrune.Activitys;
using Izrune.Attributes;
using Izrune.Helpers;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Helpers;

namespace Izrune.Fragments
{
    class InnerPromoFragment : MPDCBaseFragment
    {
        protected override int LayoutResource { get; } = Resource.Layout.PromoLayout;

        [MapControl(Resource.Id.PromoEditText)]
        EditText promoEdit;

        [MapControl(Resource.Id.SubmitButton)]
        LinearLayout Submit;

        [MapControl(Resource.Id.Informationtxt)]
        TextView Infotxt;

        [MapControl(Resource.Id.MonthSpiner)]
        Spinner monthSpiner;

        [MapControl(Resource.Id.NextButton)]
        LinearLayout NextButton;

        [MapControl(Resource.Id.BotBackButton)]
        LinearLayout BotbackButton;

        [MapControl(Resource.Id.PromoSection)]
        TextView PromoResult;

        [MapControl(Resource.Id.PromoConteiner)]
        FrameLayout PromoContainer;

        [MapControl(Resource.Id.MonthConteiner)]
        FrameLayout MonthContainer;


        private int StudentId;


        bool IsSucces = false;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        
        public InnerPromoFragment(IPromoCode cod,int studentId)
        {
            PromoCod = cod;
            StudentId = studentId;
        }

        private static IPromoCode PromoCod;

        int MonthCount;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            BotbackButton.Visibility = ViewStates.Gone;

            if (!string.IsNullOrEmpty(PromoCod.PrommoCode))
            {
                Infotxt.Text = "პრომო კოდის მისაღებად მიმართეთ სკოლის ადმინისტრაციას";
                Infotxt.SetTextColor(Color.LightGreen);
                PromoContainer.Visibility = ViewStates.Visible;
                MonthContainer.Visibility = ViewStates.Visible;
                PromoResult.Visibility = ViewStates.Visible;
            }
            else
            {
                PromoResult.Visibility = ViewStates.Invisible;
                PromoContainer.Visibility = ViewStates.Invisible;
                MonthContainer.Visibility = ViewStates.Invisible;
            }

            NextButton.Click += async(s, e) =>
            {
                if (IsSucces)
                {
                    IzruneHellper.Instance.CurrentStudentAmount = MonthCount;

                  await UserControl.Instance.ReNewPack(UserControl.Instance.CurrentStudent);
                    Intent intent = new Intent(this, typeof(ActivityPaymentCategory));
                    intent.PutExtra("Inner", "sddsd");
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "გთხოვთ შეიყვანეტ პრომოკოდი სწორად", ToastLength.Long).Show();
                }
            };

            var DataAdapter = new ArrayAdapter<string>(this,
                  Android.Resource.Layout.SimpleSpinnerDropDownItem,
                 PromoCod.Prices.Select(i => $"{i.Period}").ToList());


            monthSpiner.Adapter = DataAdapter;
            monthSpiner.ItemSelected += MonthSpiner_ItemSelected;


            Submit.Click += (s, e) =>
            {
                if (promoEdit.Text == PromoCod.PrommoCode && !string.IsNullOrEmpty(PromoCod.PrommoCode))
                {
                    promoEdit.SetBackgroundResource(Resource.Drawable.izruneback);
                    IsSucces = true;
                }
                else
                {
                    promoEdit.SetBackgroundResource(Resource.Drawable.RedPromoCodebutton);
                    IsSucces = false;
                }
            };

            
        }

        private void MonthSpiner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var Result = PromoCod.Prices.ElementAt(e.Position).EndDate?.Subtract(PromoCod.Prices.ElementAt(e.Position).StartDate.Value);

            PromoResult.Text = $"{PromoCod.Prices.ElementAt(e.Position).Period}-{PromoCod.Prices.ElementAt(e.Position).price}₾";

            MonthCount = MonthDifference(PromoCod.Prices.ElementAt(e.Position).EndDate.Value, PromoCod.Prices.ElementAt(e.Position).StartDate.Value);
        }

        private int MonthDifference(DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }
    }
}