using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Izrune.Adapters.RecyclerviewAdapters;
using Izrune.Attributes;
using Izrune.Fragments;
using Izrune.Helpers;
using IZrune.PCL;

namespace Izrune.Activitys
{
    [Activity(Label = "IZrune", Theme = "@style/AppTheme", MainLauncher = true)]
    class MainActivity : MPDCBaseActivity
    {
        protected override int LayoutResource { get; } = Resource.Layout.LayoutMainIncomePage;

        [MapControl(Resource.Id.nav_view)]
        NavigationView navigationView;

        [MapControl(Resource.Id.mainPageContainer)]
        FrameLayout MainContainer;

        [MapControl(Resource.Id.drawer_layout)]
        DrawerLayout drawer;

        [MapControl(Resource.Id.hamburgerBtnMainActivity)]
        FrameLayout Hamburger;

        [MapControl(Resource.Id.HeaderText)]
        TextView HeaderText;

        //[MapControl(Resource.Id.MenuContainer)]
        //LinearLayout MenuContainer;

        //[MapControl(Resource.Id.MenuRecyclerView)]
        //RecyclerView MenuRecycler;

        private List<MenuItemClass> MenuItems = new List<MenuItemClass>()
        {
            new MenuItemClass(){Image=Resource.Drawable.homeicon ,MenuTitle="შესვლა"},
            new MenuItemClass(){Image=Resource.Drawable.homeicon ,MenuTitle="სიახლე"},
            new MenuItemClass(){Image=Resource.Drawable.homeicon ,MenuTitle="გაიგეთ მეტი"},
            new MenuItemClass(){Image=Resource.Drawable.homeicon ,MenuTitle="კონტაქტი"},
        };

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCore.Instance.InitServices();


            ChangeFragmentPage(new LogInFragment(), MainContainer.Id);
            // navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            Hamburger.Click += Hamburger_Click;
            #region oldManeu
            //MenuRecyclerAdapter adapter = new MenuRecyclerAdapter(MenuItems);
            //adapter.OnItemClick += (s) =>
            //{
            //    switch (s)
            //    {
            //        case 0:
            //            {
            //                HeaderText.Text = "";
            //                ChangeFragmentPage(new LogInFragment(), MainContainer.Id);
            //                drawer.CloseDrawers();
            //                break;
            //            }
            //        case 1:
            //            {
            //                HeaderText.Text = "საგანმანათლებლო სიახლეები";
            //                ChangeFragmentPage(new NewsFragment(), MainContainer.Id);
            //                drawer.CloseDrawers();
            //                break;
            //            }
            //        case 2:
            //            {
            //                HeaderText.Text = "გაიგეთ მეტი";
            //                ChangeFragmentPage(new GetMoreInfoFragment(), MainContainer.Id);
            //                drawer.CloseDrawers();
            //                break;
            //            }
            //        case 3:
            //            {
            //                HeaderText.Text = "კონტაქტი";
            //                ChangeFragmentPage(new ContactFragment(), MainContainer.Id);
            //                drawer.CloseDrawers();
            //                break;
            //            }
            //    };
            //};
            //MenuRecycler.SetLayoutManager(new LinearLayoutManager(this));
            //MenuRecycler.SetAdapter(adapter);

            #endregion
        }
        private void Hamburger_Click(object sender, EventArgs e)
        {
           drawer.OpenDrawer(navigationView);
        }

        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.GroupId)
            {
                case Resource.Id.mainGroup:
                    {
                        HeaderText.Text = "";
                        ChangeFragmentPage(new LogInFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }
                case Resource.Id.LoginNews:
                    {
                        HeaderText.Text = "საგანმანათლებლო სიახლეები";
                        ChangeFragmentPage(new NewsFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }
                case Resource.Id.LoginGetInfo:
                    {
                        HeaderText.Text = "გაიგეთ მეტი";
                        ChangeFragmentPage(new GetMoreInfoFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }
                case Resource.Id.LoginContact:
                    {
                        HeaderText.Text = "კონტაქტი";
                        ChangeFragmentPage(new ContactFragment(), MainContainer.Id);
                        drawer.CloseDrawers();
                        break;
                    }

            }
        }


        public override void OnBackPressed()
        {
            base.OnBackPressed();
            //Intent intent = new Intent(this, typeof(LogInActivity));
            //StartActivity(intent);
            this.Finish();
        }


    }
}