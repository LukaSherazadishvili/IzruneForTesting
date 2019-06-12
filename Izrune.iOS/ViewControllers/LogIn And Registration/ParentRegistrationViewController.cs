// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
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

        private ParentRegiFirstViewController parentRegVc;
        private ParentRegSecondViewController parent2RegVc;

        private StudentRegFirstViewController studentRegVc1;
        private StudentRegSecondViewController studentRegVc2;

        private PacketViewController choosePacketVc;
        private int CurrentIndex = 0;


        bool NextClicked = true;
        private List<IRegion> CityList;

        /*
         * 
         * UserControl RegisterParent(student)Part1-2
         * 
         * IregisterService RegisterInfo for dropdown
         * 
         * User Control GetPromoCode
        */

        
        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitViewControllers();

            InitUI();

            await LoadDataAsync();

            this.AddVcInView(viewForPager, parentRegVc);

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
            choosePacketVc.HideFooter = true;
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

            this.AddVcInView(viewForPager, newVc);
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
                            AddViewController(choosePacketVc, studentRegVc2);
                            studentRegVc2.SendClicked?.Invoke();
                            HideHeader(true);
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
                            AddViewController(parent2RegVc, parentRegVc);
                        else
                        {
                            AddViewController(parentRegVc, parent2RegVc);
                            HideHeader(false);
                        }
                            
                        break;
                    }
                case 5:
                    {
                        if (NextClicked)
                            AddViewController(parent2RegVc, parentRegVc);
                        else
                            AddViewController(parentRegVc, parent2RegVc);
                        break;
                    }
                default:
                    break;
            }
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
            var service = ServiceContainer.ServiceContainer.Instance.Get<IRegistrationServices>();

            try
            {
                CityList = (await service.GetRegionsAsync())?.OrderBy(x => x.title)?.ToList();
                parentRegVc.CityList = CityList;
                studentRegVc2.CityList = CityList;
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
    }
}