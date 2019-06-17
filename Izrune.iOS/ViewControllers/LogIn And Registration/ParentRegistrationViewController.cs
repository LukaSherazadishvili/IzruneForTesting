// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using System.Threading.Tasks;
using Foundation;
using Izrune.iOS.Utils;
using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using MPDCiOSPages.ViewControllers;
using MpdcViewExtentions;
using UIKit;

namespace Izrune.iOS
{
    public partial class ParentRegistrationViewController : BaseViewController
    {
        public ParentRegistrationViewController (IntPtr handle) : base (handle)
        {
        }

        public static readonly NSString StoryboardId = new NSString("ParentRegistrationStoryboardId");

        //UIViewController[] RegistrationPages;

        #region Fields
        private ParentRegiFirstViewController parentRegVc;
        private ParentRegSecondViewController parent2RegVc;

        private StudentRegFirstViewController studentRegVc1;
        private StudentRegSecondViewController studentRegVc2;

        private PacketViewController choosePacketVc;
        private AddStudentViewController AddMoreStudentVc;

        private PaymentMethodViewController paymentViewController;

        
        private int CurrentIndex = 0;


        bool NextClicked = true;
        private List<IRegion> CityList;
        private IPrice SelectedPrice;

        private const int HeaderAndFooterHeight = 275;

        #endregion

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitViewControllers();

            InitUI();

            this.NavigationItem.BackBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);

            await LoadDataAsync();
            View.LayoutIfNeeded();

            this.AddVcInViewWithoutFrame(viewForPager, parentRegVc);
            var scrollView = parentRegVc.View.OfType<UIScrollView>().FirstOrDefault();
            scrollView.LayoutIfNeeded();

            SetContentHeight(scrollView.ContentSize.Height);

            //scrollView.BackgroundColor = UIColor.Red;
            subViewsContentHeightConstraint.Constant =scrollView.ContentSize.Height;// parentRegVc.View.Frame.Height;


            View.LayoutIfNeeded();

            var diff = this.View.Frame.Height - (scrollView.ContentSize.Height + HeaderAndFooterHeight);//(View.Subviews.OfType<UIScrollView>().FirstOrDefault().ContentSize.Height );

            if (diff > 0)
            {

                float safeAreaSize = default(float);

                if(UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {
                    safeAreaSize = (float)UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Bottom;
                }

                subViewsContentHeightConstraint.Constant = scrollView.ContentSize.Height + (diff) -130;
                View.LayoutIfNeeded();
            }



            InitGestures();

            ChangeHeader(true);
        }



        private void InitViewControllers()
        {
            parentRegVc = Storyboard.InstantiateViewController(ParentRegiFirstViewController.StoryboardId) as ParentRegiFirstViewController;

            parent2RegVc = Storyboard.InstantiateViewController(ParentRegSecondViewController.StoryboardId) as ParentRegSecondViewController;

            studentRegVc1 = Storyboard.InstantiateViewController(StudentRegFirstViewController.StoryboardId) as StudentRegFirstViewController;

            studentRegVc2 = Storyboard.InstantiateViewController(StudentRegSecondViewController.StoryboardId) as StudentRegSecondViewController;
            studentRegVc2.SchoolSelected = (school) => { choosePacketVc.SchoolId = school.id; };


            choosePacketVc = Storyboard.InstantiateViewController(PacketViewController.StoryboardId) as PacketViewController;
            choosePacketVc.PriceSelected = (price) => SelectedPrice = price;

            AddMoreStudentVc = Storyboard.InstantiateViewController(AddStudentViewController.StoryboardId) as AddStudentViewController;
            AddMoreStudentVc.AddMoreStudentClicked = () =>
            {
                studentRegVc1 = Storyboard.InstantiateViewController(StudentRegFirstViewController.StoryboardId) as StudentRegFirstViewController;

                CurrentIndex = 2;
                AddViewController(studentRegVc1, AddMoreStudentVc);
            };

            paymentViewController = Storyboard.InstantiateViewController(PaymentMethodViewController.StoryboardId) as PaymentMethodViewController;
        }

        private void InitGestures()
        {
            nextBtn.TouchUpInside += delegate
            {
                NextClicked = true;
                GetCurrentPage(CurrentIndex);
                CurrentIndex++;
                CheckIndex();
            };

            prewBtn.TouchUpInside += delegate {
                NextClicked = false;
                GetCurrentPage(CurrentIndex);
                CurrentIndex--;
                CheckIndex();
            };
        }

        private void AddViewController(UIViewController newVc, UIViewController oldVc)
        {
            oldVc.WillMoveToParentViewController(this);
            oldVc.View.RemoveFromSuperview();
            oldVc.RemoveFromParentViewController();


            var scrollView = newVc.View.OfType<UIScrollView>().FirstOrDefault();
            if(scrollView != null)
            {
                scrollView.LayoutIfNeeded();
                subViewsContentHeightConstraint.Constant = scrollView.ContentSize.Height;
            }

            this.AddVcInViewWithoutFrame(viewForPager, newVc);

            SetContentHeight(scrollView.ContentSize.Height);
        }

        private void InitUI()
        {
            nextBtn.Layer.CornerRadius = 25;
            prewBtn.Layer.CornerRadius = 25;

            nextBtn.AddShadowToView(5, 25, 0.8f, AppColors.TitleColor);
        }

        private void CheckIndex()
        {
            nextBtn.Enabled = CurrentIndex < 6;
            prewBtn.Enabled = CurrentIndex > 0;
        }

        private void GetCurrentPage(int pageIndex)
        {
            switch (pageIndex)
            {
                case 0:
                    {
                        if (NextClicked)
                        {
                            AddViewController(parent2RegVc, parentRegVc);
                            parentRegVc.SendClicked?.Invoke();
                        }
                        break;
                    }
                case 1:
                    {
                        if (NextClicked)
                        {
                            AddViewController(studentRegVc1, parent2RegVc);
                            parent2RegVc.SendClicked?.Invoke();
                            ChangeHeader(false);
                        }
                        else
                        {
                            AddViewController(parentRegVc, parent2RegVc);
                            ChangeHeader(true);
                        }
                        break;
                    }
                case 2:
                    {
                        if (NextClicked)
                        {
                            AddViewController(studentRegVc2, studentRegVc1);
                            studentRegVc1.SendClicked?.Invoke();
                            ChangeHeader(false);
                        }
                        else
                        {
                            AddViewController(parent2RegVc, studentRegVc1);
                            ChangeHeader(true);
                        }
                        break;
                    }
                case 3:
                    {
                        if (NextClicked)
                        {
                            studentRegVc2.SendClicked?.Invoke();
                            if (studentRegVc2.IsAllSelected)
                            {
                                //AddViewController(choosePacketVc, studentRegVc2);

                                this.NavigationController.PushViewController(choosePacketVc, false);
                                //HideHeader(true);
                            }
                            else
                            {
                                CurrentIndex--;
                                ShowAlert();
                            }
                        }
                        else
                        {
                            AddViewController(studentRegVc1, studentRegVc2);
                            ChangeHeader(false);
                        }
                        break;
                    }
                case 4:
                    {
                        if (NextClicked)
                        {
                            AddViewController(AddMoreStudentVc, choosePacketVc);
                            choosePacketVc.SendClicked?.Invoke();
                            HideHeader(true);
                        }
                        else
                        {
                            AddViewController(studentRegVc2, choosePacketVc);
                            HideHeader(false);
                        }
                            
                        break;
                    }
                case 5:
                    {
                        if (NextClicked)
                        {
                            AddMoreStudentVc?.SendClicked?.Invoke();

                            AddMoreStudentVc.DataSent = (ipay) => {
                                paymentViewController.PaymentUrl = ipay.CurrentUserPayURl;
                                AddViewController(paymentViewController, AddMoreStudentVc);
                            };

                        }
                        else
                        {
                            AddViewController(studentRegVc2, AddMoreStudentVc);
                            //this.NavigationController.PushViewController(choosePacketVc, false);
                        }

                        break;
                    }
                default:
                    break;
            }
        }

        private void ShowAlert()
        {
            var alertVc = UIAlertController.Create("ყურადღება!", "აუცილებელია *-ით აღნიშნული ველების შევსება", UIAlertControllerStyle.Alert);
            alertVc.AddAction(UIAlertAction.Create("დახურვა", UIAlertActionStyle.Default, null));
            this.PresentViewController(alertVc, true, null);
        }

        private void ChangeHeader(bool isParent)
        {
            headerTitleLbl.Text = isParent? "მშობლის (ან სხვა მზრუნველის) რეგისტრაცია" : "დაარეგისტრირეთ მოსწავლე";
            headerImageView.Image = isParent ? UIImage.FromBundle("1 – 6.png") : UIImage.FromBundle("Group 248.png");
        }

        private void HideHeader(bool hide)
        {
            headerStackView.Hidden = hide;
            headerHeightConstant.Constant = hide ? 0 : 120;
        }

        private async Task LoadDataAsync()
        {
            ShowLoading();
            HideHeaderAndFooter(true);
            var service = ServiceContainer.ServiceContainer.Instance.Get<IRegistrationServices>();

            try
            {
                CityList = (await service.GetRegionsAsync())?.OrderBy(x => x.title)?.ToList();
                parentRegVc.CityList = CityList;
                studentRegVc2.CityList = CityList;
                HideHeaderAndFooter(false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            finally
            {
                EndLoading();
            }

        }

        private void HideHeaderAndFooter(bool hide)
        {
            nextBtn.Hidden = hide;
            footerView.Hidden = hide;
            headerView.Hidden = hide;
        }

        private void SetContentHeight(nfloat contentHeight)
        {
            subViewsContentHeightConstraint.Constant = contentHeight;


            View.LayoutIfNeeded();
            var diff = this.View.Frame.Height - (contentHeight + HeaderAndFooterHeight);

            if (diff >= 155)
            {

                float safeAreaSize = default(float);

                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {
                    safeAreaSize = (float)UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Bottom;
                }

                //var size = (safeAreaSize > 0 ? safeAreaSize : 25);
                subViewsContentHeightConstraint.Constant = contentHeight + diff - 155;
                View.LayoutIfNeeded();
            }

            viewForPager.Frame = new CoreGraphics.CGRect(viewForPager.Frame.X, viewForPager.Frame.Y,
                viewForPager.Frame.Width, subViewsContentHeightConstraint.Constant);

            View.LayoutIfNeeded();
        }

    }
}