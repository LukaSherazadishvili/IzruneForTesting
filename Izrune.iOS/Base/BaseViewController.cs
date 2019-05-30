using System;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using Izrune.iOS.Utils;
using MpdcViewExtentions;
using Plugin.Connectivity;
using UIKit;

namespace MPDCiOSPages.ViewControllers
{
    public partial class BaseViewController : UIViewController
    {
        public BaseViewController() : base("BaseViewController", null)
        {
        }

        public BaseViewController(string nibname,NSBundle bundle) : base(nibname, bundle)
        {
        }

        public BaseViewController(IntPtr handle) : base(handle)
        {
        }

        //protected string PlaceHolder = "logo.png";

        private UIView _loadingView;

        private UIActivityIndicatorView loadingIndicator;

        protected UIColor BarAccentColor { get; set; }

        protected UIColor ViewBackgroundColor { get; set; } = UIColor.White;
        #region Virtuals
        protected virtual bool ShouldInitBackgroundColor { get;} = true;
        protected virtual bool ShouldDrawImageInNavBar { get;  } = true;
        protected virtual bool ShouldMakeBarTransparent { get;  } = true;


        protected virtual string BarButtonFontName { get;  }


        protected virtual UIColor NavBarColor { get; }
        protected virtual UIImage NavBarImage { get; }
        protected virtual CoreGraphics.CGSize LoadingSize { get => new CoreGraphics.CGSize(100, 150); }
        protected virtual CoreGraphics.CGPoint LoadingCenter { get => this.View.Center; }

      


        protected virtual UIView RootViewForLoading { get => this.View; }
        #endregion


        UIViewController _emptyNoInternetVc;


        bool _isDataLoading = false;

        object _lockerdataLoading = new object();
        protected bool IsDataLoading
        {
            get
            {
                lock (_lockerdataLoading)
                {
                    return _isDataLoading;
                }
            }
             set
            {

                lock (_lockerdataLoading)
                {
                    _isDataLoading = value;

                }
            }
        }

        //LoadingViewOptions LoadingViewOptions = new LoadingViewOptions()
        //{
        //    X = 0,
        //    Y = 0,
        //    Width = 100.0f,
        //    Height = 150.0f,
        //    CornerRadius = 15.0f,
        //    ClipsToBounds = true,
        //    BackgroundColor = UIColor.Clear,
        //    LoadingImage = null

        //};
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitNavBarStyleToUi();
            InitColors();

            //View.BackgroundColor = ViewBackgroundColor;
        }

        protected void AddMenuItemToBar(UIImage barButtonImage,UIColor imageColor, Action action, bool isRight = true, string title = "", string fontName="")
        {

            if (UIScreen.MainScreen.Bounds.Width < 350)
                title = string.Empty;


            this.NavigationItem.InitBarButtonToNav(barButtonImage, imageColor, action, isRight, title, fontName);


        }


        private Action _connectedAction;

        protected void ShowLoading(CGPoint point = default(CGPoint))
        {

            if (IsDataLoading)
                return;

            _loadingView = createLoadingView(point);


            var alert = UIAlertController.Create("", "", UIAlertControllerStyle.Alert);

            loadingIndicator = new UIActivityIndicatorView(new CGRect(0, 0, 30, 30));
            loadingIndicator.Center = this.View.Center;
            loadingIndicator.HidesWhenStopped = true;
            loadingIndicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray;
            loadingIndicator.Color = AppColors.Tint;

            //alert.View.AddSubview(loadingIndicator);
            loadingIndicator.StartAnimating();

            this.RootViewForLoading.AddSubview(loadingIndicator);
            //IsDataLoading = true;

            //Task.Run(async () =>
            //{

            //    while (IsDataLoading)
            //    {

            //        InvokeOnMainThread(() => loadingIndicator.StartAnimating());
            //        //await Task.Delay(1400);
            //    }


            //    //InvokeOnMainThread(() => _loadingView?.RemoveFromSuperview());
            //});

        }

        protected void EndLoading()
        {

            IsDataLoading = false;
            loadingIndicator.StopAnimating();
            //InvokeOnMainThread(() => _loadingView?.RemoveFromSuperview());

        }

        protected UIView createLoadingView(CGPoint point=default(CGPoint))
        {

            UIView view = new UIView
            {
                Bounds = new CoreGraphics.CGRect(0,0, LoadingSize.Width, LoadingSize.Height)
            };

            view.Layer.CornerRadius = 15;
            view.ClipsToBounds = true;

            if (point == default(CGPoint))
                view.Center = LoadingCenter;
            else
                view.Frame = new CGRect(point.X, point.Y, LoadingSize.Width, LoadingSize.Height);

            view.BackgroundColor = UIColor.Clear;//ThemeColors.ViewBackgroundColor.RgbToUIColor();
            var image = new UIImageView
            {
                Image = UIImage.FromBundle("4.png"),
                ContentMode = UIViewContentMode.ScaleAspectFit,
                Frame = new CoreGraphics.CGRect(0, 0, view.Bounds.Width, view.Bounds.Height - 10)
            };


            view.AddSubview(image);

            view.AddSubview(image);
            return view;

        }

    

        protected void RemoveItemsFrombar()
        {

            this.NavigationItem.RightBarButtonItems = null;
            this.NavigationItem.LeftBarButtonItems = null;

        }

        protected void InitColors()
        {

            if (ShouldInitBackgroundColor)
                this.View.BackgroundColor = ViewBackgroundColor;//TODO gasatania
        }

        protected void InitNavBarStyleToUi()
        {

            if (ShouldMakeBarTransparent)
                this.NavigationController?.NavigationBar?.InitTransparencyToNavBar();

           

            if (NavBarColor != default(UIColor))
                this.NavigationController?.NavigationBar?.InitNavigationBarColorWithNoShadow(NavBarColor);

            if (ShouldDrawImageInNavBar)
                this.NavigationItem?.InitLogoToNav(NavBarImage ?? UIImage.FromBundle("title.png"));

        }



        #region NOINTERNET PAgE

        protected bool IsInternetAvaiable(){
            return CrossConnectivity.Current.IsConnected;
        }

        UIViewController getNoInternetViewController()
        {

            var emptyVc = Storyboard.InstantiateViewController("NoInternetStoryBoardId");

            return emptyVc;
        }

        protected void ShowNoInternetPage(Action Connected){

            _connectedAction = Connected;
            CrossConnectivity.Current.ConnectivityChanged -= Current_ConnectivityChanged;
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
            _emptyNoInternetVc = getNoInternetViewController();
            _emptyNoInternetVc.View.Tag = EMPTYTAG;
            addChildvc(_emptyNoInternetVc);

        }
        protected void HideNoInternetPage(){
            _emptyNoInternetVc?.RemoveFromParentViewController();
            removeEmptyView();
        }

        const int EMPTYTAG = 666;

         void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            //if (_emptyNoInternetVc != null)
            //{
            //    var currIntPage = _emptyNoInternetVc as NoInternetViewController;
            //    currIntPage.SetNoInternetState(e.IsConnected);
            //}


            //if (e.IsConnected)
            //{
            //    _connectedAction?.Invoke();
            //    HideNoInternetPage();
            //}
        }

        void addChildvc(UIViewController vc)
        {

            AddChildViewController(vc);
            vc.View.Bounds = this.View.Bounds;
            this.View.AddSubview(vc.View);
            vc.DidMoveToParentViewController(this);

        }

        void removeEmptyView()
        {

            //for (int i = 0; i < this.View.Subviews.Length; i++)
            //{
            //    var currView = this.View.Subviews[i];
            //    if (currView.Tag == EmptyDataController.TagNumber)
            //    {
            //        currView.Hidden = true;

            //        currView.RemoveFromSuperview();
            //    }
            //}

        }
        #endregion
    }

}

