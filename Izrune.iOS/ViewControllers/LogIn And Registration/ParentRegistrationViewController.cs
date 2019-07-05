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

        private bool AddMoreStudentClicked;
        private int CurrentIndex = 0;

        bool NextClicked = true;
        private List<IRegion> CityList;
        private IPrice SelectedPrice;
        //private int AddStudentIndex;
        private const int HeaderAndFooterHeight = 275;

        private IStudent MoreStudent;

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

            InitGestures();

            ChangeHeader(true);
        }


        private void InitViewControllers()
        {
            parentRegVc = Storyboard.InstantiateViewController(ParentRegiFirstViewController.StoryboardId) as ParentRegiFirstViewController;

            parent2RegVc = Storyboard.InstantiateViewController(ParentRegSecondViewController.StoryboardId) as ParentRegSecondViewController;

            studentRegVc1 = Storyboard.InstantiateViewController(StudentRegFirstViewController.StoryboardId) as StudentRegFirstViewController;

            studentRegVc2 = Storyboard.InstantiateViewController(StudentRegSecondViewController.StoryboardId) as StudentRegSecondViewController;
            studentRegVc2.SchoolSelected = (school) => 
            { 
                choosePacketVc.SchoolId = school.id;
                choosePacketVc.RefreshData?.Invoke();
                SelectedPrice = null;
                CurrentIndex = 3;
            };
            studentRegVc2.CitySelected = () => { 
                SelectedPrice = null; 
            };

            choosePacketVc = Storyboard.InstantiateViewController(PacketViewController.StoryboardId) as PacketViewController;
            choosePacketVc.IsFromMenu = false;

            choosePacketVc.PriceSelected = (price) => {

                SelectedPrice = price;
                CurrentIndex = 4;
                HideHeader(true);
                AddViewController(AddMoreStudentVc, studentRegVc2);
            };

            AddMoreStudentVc = Storyboard.InstantiateViewController(AddStudentViewController.StoryboardId) as AddStudentViewController;
            AddMoreStudentVc.AddMoreStudentClicked = () =>
            {
                //Add More Student

                AddMoreStident();
            };

            paymentViewController = Storyboard.InstantiateViewController(PaymentMethodViewController.StoryboardId) as PaymentMethodViewController;
            paymentViewController.GoToLogin = () => { 
                this.NavigationController.PopViewController(true); 
                };
        }

        private void AddMoreStident()
        {
            AddMoreStudentClicked = true;
            prewBtn.Enabled = false;
            ReseteViewControllers();
            CurrentIndex = 2;
            SelectedPrice = null;
            //AddStudentIndex = 0;
            AddViewController(studentRegVc1, AddMoreStudentVc);
            ChangeHeader(false);
            HideHeader(false);
        }

        private void InitGestures()
        {
            nextBtn.TouchUpInside += delegate
            {
                NextClicked = true;
                mainScrollView.SetContentOffset(CoreGraphics.CGPoint.Empty, true);
                GetCurrentPage(CurrentIndex);
                //CurrentIndex++;
                CheckIndex();

            };

            prewBtn.TouchUpInside += delegate {

                if(AddMoreStudentClicked)
                {
                    //TODO
                    prewBtn.Enabled = CurrentIndex <= 2;
                    NextClicked = false;
                    GetCurrentPage(CurrentIndex);
                    CheckIndex();
                }
                else
                {
                    NextClicked = false;
                    GetCurrentPage(CurrentIndex);
                    //CurrentIndex--;
                    CheckIndex();
                }

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
                //subViewsContentHeightConstraint.Constant = scrollView.ContentSize.Height;
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
            nextBtn.Enabled = CurrentIndex < 5;
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
                            var res = parentRegVc.IsFormFilled();

                            if (res)
                            {
                                AddViewController(parent2RegVc, parentRegVc);
                                CurrentIndex++;
                                parentRegVc.SendClicked?.Invoke();
                            }

                            else
                            {
                                ShowAlert();
                                CurrentIndex = 0;
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        if (NextClicked)
                        {
                            var res = parent2RegVc.IsFormFilled();

                            if(res)
                            {
                                AddViewController(studentRegVc1, parent2RegVc);
                                CurrentIndex++;
                                parent2RegVc.SendClicked?.Invoke();
                                ChangeHeader(false);
                            }

                            else
                            {
                                ShowAlert();
                                CurrentIndex = 1;
                            }

                        }
                        else
                        {
                            AddViewController(parentRegVc, parent2RegVc);
                            CurrentIndex--;
                            ChangeHeader(true);
                        }
                        break;
                    }
                case 2:
                    {
                        if (NextClicked)
                        {
                            var res = studentRegVc1.IsFormFilled();

                            if(res)
                            {
                                prewBtn.Enabled = true;
                                if (AddMoreStudentClicked)
                                {
                                    studentRegVc1.StudentSelected?.Invoke();
                                    MoreStudent = studentRegVc1.Student;
                                }
                                else
                                    studentRegVc1.SendClicked?.Invoke();

                                ChangeHeader(false);
                                AddViewController(studentRegVc2, studentRegVc1);
                                CurrentIndex++;
                            }

                            else
                            {
                                ShowAlert();
                                CurrentIndex = 2;
                            }

                        }
                        else
                        {
                            ChangeHeader(true);
                            AddViewController(parent2RegVc, studentRegVc1);
                            CurrentIndex--;
                        }
                        break;
                    }
                case 3:
                    {
                        if (NextClicked)
                        {

                            var res = studentRegVc2.IsFormFilled();

                            if(res)
                            {
                                if (AddMoreStudentClicked)
                                {
                                    studentRegVc2.StudentSelected?.Invoke();
                                    MoreStudent.RegionId = studentRegVc2.Student.RegionId;
                                    MoreStudent.Village = studentRegVc2.Student.Village;
                                    MoreStudent.SchoolId = studentRegVc2.Student.SchoolId;
                                    MoreStudent.Class = studentRegVc2.Student.Class;
                                }

                                studentRegVc2.SendClicked?.Invoke();

                                if (SelectedPrice == null && studentRegVc2.IsAllSelected)
                                {
                                    //AddViewController(choosePacketVc, studentRegVc2);

                                    this.NavigationController.PushViewController(choosePacketVc, false);
                                    //HideHeader(true);
                                }

                                else if (SelectedPrice == null && !studentRegVc2.IsAllSelected)
                                {
                                    CurrentIndex = 2;
                                    ShowAlert();
                                }

                                else
                                {
                                    AddViewController(AddMoreStudentVc, studentRegVc2);
                                    CurrentIndex++;
                                }
                            }

                            else
                            {
                                ShowAlert();
                                CurrentIndex = 3;
                            }
                        }
                        else
                        {
                            ChangeHeader(false);
                            AddViewController(studentRegVc1, studentRegVc2);
                            CurrentIndex--;
                        }
                        break;
                    }
                case 4:
                    {
                        if (NextClicked)
                        {
                            HideHeader(true);
                            AddMoreStudentVc?.SendClicked?.Invoke();
                            AddMoreStudentVc.DataSent = (ipay) => {
                                paymentViewController.PayInfo = ipay;
                                this.NavigationController.PushViewController(paymentViewController, true);
                            };
                        }
                        else
                        {
                            ChangeHeader(false);
                            HideHeader(false);
                            AddViewController(studentRegVc2, AddMoreStudentVc);
                            CurrentIndex--;
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

        private void SetContentHeight(nfloat scrollviewContentHeight)
        {
            //subViewsContentHeightConstraint.Constant = scrollviewContentHeight;


            UIEdgeInsets safeAreaSize = new UIEdgeInsets();

            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                safeAreaSize = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets;
            }

            var currentContentHeight = this.View.Frame.Height - (HeaderAndFooterHeight + safeAreaSize.Top + safeAreaSize.Bottom);
            var diffrenceWithContents = currentContentHeight - scrollviewContentHeight;

            if(diffrenceWithContents >= 0)
            {
                subViewsContentHeightConstraint.Constant = currentContentHeight;

            }

            else
            {
                subViewsContentHeightConstraint.Constant = scrollviewContentHeight;

            }

            //if (CurrentIndex >= 4)
                //subViewsContentHeightConstraint.Constant += 120;

            View.LayoutIfNeeded();
            //var diff = this.View.Frame.Height - (scrollviewConstentHeight + HeaderAndFooterHeight);

            //if (diff >= 155)
            //{
            
            //    //var size = (safeAreaSize > 0 ? safeAreaSize : 25);
            //    subViewsContentHeightConstraint.Constant = scrollviewConstentHeight + diff - 155 + safeAreaSize.Bottom;

            //    View.LayoutIfNeeded();
            //}


            viewForPager.Frame = new CoreGraphics.CGRect(viewForPager.Frame.X, viewForPager.Frame.Y,
                viewForPager.Frame.Width, subViewsContentHeightConstraint.Constant);

            View.LayoutIfNeeded();
        }

        private void ReseteViewControllers()
        {
            studentRegVc1 = Storyboard.InstantiateViewController(StudentRegFirstViewController.StoryboardId) as StudentRegFirstViewController;
            studentRegVc2 = Storyboard.InstantiateViewController(StudentRegSecondViewController.StoryboardId) as StudentRegSecondViewController;
            studentRegVc2.CityList = CityList;

            studentRegVc2.SchoolSelected = (school) =>
            {
                choosePacketVc.SchoolId = school.id;
                choosePacketVc.RefreshData?.Invoke();
                SelectedPrice = null;
                CurrentIndex = 3;
            };

            studentRegVc2.CitySelected = () => {
                SelectedPrice = null;
            };

            choosePacketVc = Storyboard.InstantiateViewController(PacketViewController.StoryboardId) as PacketViewController;
            choosePacketVc.IsFromMenu = false;

            choosePacketVc.PriceSelected = (price) => {

                MoreStudent.PackageMonthCount = price.MonthCount;
                SelectedPrice = price;
                CurrentIndex = 4;
                HideHeader(true);
                AddViewController(AddMoreStudentVc, studentRegVc2);
            };

            AddMoreStudentVc = Storyboard.InstantiateViewController(AddStudentViewController.StoryboardId) as AddStudentViewController;

            AddMoreStudentVc.AddMoreStudentClicked = () =>
            {
                AddMoreStident();
            };
        }

        private void ChangeHeader(bool isParent)
        {
            headerTitleLbl.Text = isParent ? "მშობლის (ან სხვა მზრუნველის) რეგისტრაცია" : "დაარეგისტრირეთ მოსწავლე";
            headerImageView.Image = isParent ? UIImage.FromBundle("1 – 6.png") : UIImage.FromBundle("Group 248.png");
        }

        private void HideHeader(bool hide)
        {
            headerStackView.Hidden = hide;
            headerHeightConstant.Constant = hide ? 0 : 120;
        }
    }
}