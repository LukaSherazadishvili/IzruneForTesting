﻿using System;
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
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Helpers;

namespace Izrune.Fragments
{
    class PromoFragment : MPDCBaseFragment
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

        [MapControl(Resource.Id.PromoConteiner)]
        FrameLayout PromoContainer;

        [MapControl(Resource.Id.MonthConteiner)]
        FrameLayout MonthContainer;

        [MapControl(Resource.Id.BotBackButton)]
        LinearLayout BotBackButton;

        [MapControl(Resource.Id.PromoSection)]
        TextView PromoResult;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public PromoFragment(IPromoCode cod)
        {
            PromoCod = cod;
        }

        private IPromoCode PromoCod;

        int MonthCount;

        int Price;

        bool IsSucces = false;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            if (!string.IsNullOrEmpty(PromoCod.PrommoCode))
            {
                Infotxt.Text = "პრომო კოდის მისაღებად მიმართეთ სკოლის ადმინისტრაციას";
                Infotxt.SetTextColor(Color.LightGreen);
                PromoContainer.Visibility = ViewStates.Visible;
                MonthContainer.Visibility = ViewStates.Visible;
            }
            else
            {

                PromoContainer.Visibility = ViewStates.Invisible;
                MonthContainer.Visibility = ViewStates.Invisible;


            }


            var DataAdapter = new ArrayAdapter<string>(this,
                Android.Resource.Layout.SimpleSpinnerDropDownItem,
               PromoCod.Prices.Select(i => $"{ i.Period}").ToList());

            monthSpiner.Adapter = DataAdapter;
            monthSpiner.ItemSelected += MonthSpiner_ItemSelected;



            NextButton.Click += (s, e) =>
            {
                CloseKeyboard();
                if (IsSucces)
                {
                    UserControl.Instance.SetPromoPack(MonthCount, Price, PromoCod.PrommoCode);
                    Intent intent = new Intent(this, typeof(RullesActivity));
                    StartActivity(intent);
                }
                else
                {
                 //   Toast.MakeText(this, "გთხოვთ შეიყვანოთ პრომო კოდი სწორად", ToastLength.Long).Show();
                }
            };

            Submit.Click += (s, e) =>
            {
                CloseKeyboard();
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

            BotBackButton.Click += (s, e) =>
            {
                (Activity as NextRegistrationStudentActivity).OnBackPressed();
            };
            
        }

        private void MonthSpiner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (PromoCod.PrommoCode != "")
            {
                var Result = PromoCod.Prices.ElementAt(e.Position).EndDate?.Subtract(PromoCod.Prices.ElementAt(e.Position).StartDate.Value);

                PromoResult.Text = $"{PromoCod.Prices.ElementAt(e.Position).Period}-{PromoCod.Prices.ElementAt(e.Position).price}₾";
                Price= PromoCod.Prices.ElementAt(e.Position).price.Value;

                MonthCount = PromoCod.Prices.ElementAt(e.Position).MonthCount.Value; /*MonthDifference(PromoCod.Prices.ElementAt(e.Position).EndDate.Value, PromoCod.Prices.ElementAt(e.Position).StartDate.Value)+1*/;
            }
        }


        private   int MonthDifference( DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }
    }
}