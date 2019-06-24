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

        private int StudentId;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        
        public InnerPromoFragment(IPromoCode cod,int studentId)
        {
            PromoCod = cod;
            StudentId = studentId;
        }

        private IPromoCode PromoCod;

        int MonthCount;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            if (!string.IsNullOrEmpty(PromoCod.PrommoCode))
            {
                Infotxt.Text = "პრომო კოდის მისაღებად მიმართეთ სკოლის ადმინისტრაციას";
                Infotxt.SetTextColor(Color.LightGreen);
            }


            NextButton.Click += async(s, e) =>
            {
                if (MonthCount > 0)
                {
                  await UserControl.Instance.ReNewPack(StudentId, MonthCount, MonthCount * 1, PromoCod.PrommoCode);
                    Intent intent = new Intent(this, typeof(ActivityPaymentCategory));
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "გთხოვთ შეიყვანეტ პრომოკოდი სწორად", ToastLength.Long).Show();
                }
            };

            Submit.Click += (s, e) =>
            {
                if (promoEdit.Text == PromoCod.PrommoCode && !string.IsNullOrEmpty(PromoCod.PrommoCode))
                {
                    promoEdit.SetBackgroundResource(Resource.Drawable.izruneback);

                    var DataAdapter = new ArrayAdapter<string>(this,
                  Android.Resource.Layout.SimpleSpinnerDropDownItem,
                 PromoCod.Prices.Select(i => $"{ i.months} თვე").ToList());

                    monthSpiner.Adapter = DataAdapter;
                    monthSpiner.ItemSelected += MonthSpiner_ItemSelected;
                }
                else
                {
                    promoEdit.SetBackgroundResource(Resource.Drawable.RebButtonBackground);
                }
            };


        }

        private void MonthSpiner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            MonthCount = PromoCod.Prices.ElementAt(e.Position).months;
        }
    }
}