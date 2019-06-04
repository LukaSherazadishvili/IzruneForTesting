using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using FFImageLoading;
using Foundation;
using UIKit;

namespace MpdcViewExtentions
{
    public static class ViewExtensions
    {
        public static void ApplyGradient(this UIView view, UIColor[] colors)
               => view.ApplyGradient(colors, null);


        #region Colors
        private static void ApplyGradient(this UIView view, UIColor[] colors, NSNumber[] locations)
        {
            for (int i = 0; i < view.Layer?.Sublayers?.Length; i++)
            {
                var currLayer = view.Layer.Sublayers[i];

                if (currLayer is CAGradientLayer)
                    currLayer.RemoveFromSuperLayer();
            }

            var gradient = new CAGradientLayer
            {
                Frame = new CoreGraphics.CGRect(0, 0, view.Bounds.Width, view.Bounds.Height),
                Colors = colors.Select(o => o.CGColor).Cast<CoreGraphics.CGColor>().ToArray(),
                Locations = locations,

            };

            gradient.StartPoint = new CoreGraphics.CGPoint(0.0, 0.5);
            gradient.EndPoint = new CoreGraphics.CGPoint(1.0, 0.5);

            view.Layer.InsertSublayer(gradient, 0);
        }

        public static void ApplyVerticalGradient(this UIView view, UIColor[] colors,NSNumber[] locations=null)
        {
            for (int i = 0; i < view.Layer?.Sublayers?.Length; i++)
            {
                var currLayer = view.Layer.Sublayers[i];

                if (currLayer is CAGradientLayer)
                    currLayer.RemoveFromSuperLayer();
            }


            var gradient = createVerticalGradient(view, colors, locations);


            view.Layer.InsertSublayer(gradient, 0);
        }

        public static void ApplyVerticalGradient(this UIView view, UIColor[] colors,out CAGradientLayer gradientLayer, NSNumber[] locations = null)
        {
            for (int i = 0; i < view.Layer?.Sublayers?.Length; i++)
            {
                var currLayer = view.Layer.Sublayers[i];

                if (currLayer is CAGradientLayer)
                    currLayer.RemoveFromSuperLayer();
            }


            var gradient = createVerticalGradient(view, colors, locations);

            gradientLayer = gradient;

            view.Layer.InsertSublayer(gradient, 0);
        }


        public static CAGradientLayer createVerticalGradient( UIView view, UIColor[] colors, NSNumber[] locations = null)
        {
           
            var gradient = new CAGradientLayer
            {
                Frame = new CoreGraphics.CGRect(0, 0, view.Frame.Width, view.Frame.Height),
                Colors = colors.Select(o => o.CGColor).Cast<CoreGraphics.CGColor>().ToArray(),
                Locations = locations ?? new NSNumber[] { 0, 1 }

            };


            return gradient;
        }


        public static UIColor FromHexString(this UIColor color, string hexValue, float alpha = 1.0f)
        {
            var colorString = hexValue.Replace("#", "");
            if (alpha > 1.0f)
            {
                alpha = 1.0f;
            }
            else if (alpha < 0.0f)
            {
                alpha = 0.0f;
            }

            float red, green, blue;

            switch (colorString.Length)
            {
                case 3: // #RGB
                    {
                        red = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(0, 1)), 16) / 255f;
                        green = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(1, 1)), 16) / 255f;
                        blue = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(2, 1)), 16) / 255f;
                        return UIColor.FromRGBA(red, green, blue, alpha);
                    }
                case 6: // #RRGGBB
                    {
                        red = Convert.ToInt32(colorString.Substring(0, 2), 16) / 255f;
                        green = Convert.ToInt32(colorString.Substring(2, 2), 16) / 255f;
                        blue = Convert.ToInt32(colorString.Substring(4, 2), 16) / 255f;
                        return UIColor.FromRGBA(red, green, blue, alpha);
                    }

                default:
                    throw new ArgumentOutOfRangeException(string.Format("Invalid color value {0} is invalid. It should be a hex value of the form #RBG, #RRGGBB", hexValue));

            }
        }


        public static UIImage GetImageWithColor(this UIImage image, UIColor color)
        {
            UIGraphics.BeginImageContextWithOptions(image.Size, false, image.CurrentScale);
            CGContext context = UIGraphics.GetCurrentContext();
            context.TranslateCTM(0, image.Size.Height);
            context.ScaleCTM(1.0f, -1.0f);
            context.SetBlendMode(CGBlendMode.Normal);
            var rect = new CoreGraphics.CGRect(0, 0, image.Size.Width, image.Size.Height);
            context.ClipToMask(rect, image.CGImage);
            color.SetFill();
            context.FillRect(rect);
            UIImage newImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return newImage;
        }

        #endregion

        #region NavigationBar



        public static void InitLogoToNav(this UINavigationItem navigationItem, UIImage image)
        {
            //var logo = UIImage.FromBundle(image);


            var containerVIew = new UIView(new CoreGraphics.CGRect(0, 0, 146, 34));
            containerVIew.ClipsToBounds = true;
            var titleImageView = new UIImageView(new CoreGraphics.CGRect(0, 0, 146, 34));
            //titleImageView.BackgroundColor = UIColor.Red;

            titleImageView.ClipsToBounds = true;
            titleImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            //titleImageView.BackgroundColor = UIColor.Red;
            titleImageView.Image = image;
            //containerVIew.BackgroundColor = UIColor.Yellow;

            containerVIew.AddSubview(titleImageView);

            navigationItem.TitleView = containerVIew;

        }

        public static void InitBarButtonToNav(this UINavigationItem navigationItem, UIImage barButtonImage, UIColor ImageColor, Action action, bool isRight = true, string title = "", string fontName = "")
        {
            string BarButtonFontName = fontName;
            if (navigationItem == null)
            {

                Debug.WriteLine("******* WARNING NO NavigationItem to add barbutton *******");
                return;

            }

            if (barButtonImage == null)
                return;



            if (string.IsNullOrEmpty(title))
            {

                var barButtonItem = new UIBarButtonItem();
                barButtonItem.Image = barButtonImage.GetImageWithColor(ImageColor);
                barButtonItem.Clicked += delegate
                {

                    action?.Invoke();
                };
                barButtonItem.TintColor = ImageColor;

                if (isRight)
                    navigationItem.RightBarButtonItem = barButtonItem;
                else
                    navigationItem.LeftBarButtonItem = barButtonItem;

                return;
            }


            var button = new UIButton(UIButtonType.System);

            if (!string.IsNullOrEmpty(title))
            {
                button.SetTitle(title, UIControlState.Normal);

                if (button.TitleLabel != null && !string.IsNullOrEmpty(BarButtonFontName))
                    button.TitleLabel.Font = UIFont.FromName(BarButtonFontName, 13);
            }


            button.SetImage(barButtonImage.GetImageWithColor(ImageColor), UIControlState.Normal);

            button.SemanticContentAttribute = UISemanticContentAttribute.ForceRightToLeft;

            button.SizeToFit();
            button.TintColor = ImageColor;
            button.TouchUpInside += delegate
            {

                action?.Invoke();
            };

            var barbuttonItem = new UIBarButtonItem(button);

            barbuttonItem.TintColor = ImageColor;


            if (isRight)
                navigationItem.RightBarButtonItem = barbuttonItem;
            else
                navigationItem.LeftBarButtonItem = barbuttonItem;
        }

        public static void InitTransparencyToNavBar(this UINavigationBar navigationBar)
        {

            navigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            navigationBar.ShadowImage = new UIImage();
            navigationBar.Translucent = true;
        }

        public static void InitNavigationBarColorWithNoShadow(this UINavigationBar navigationBar, UIColor navBarColor)
        {

            //navigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            navigationBar.BarTintColor = navBarColor;

            navigationBar.ShadowImage = new UIImage();
            navigationBar.Translucent = false;
        }

        #endregion

        #region ViewControllers

        public static UINavigationController CreateWithNavigationController(this UIViewController controller)
        {

            UINavigationController navigationController = new UINavigationController();
            //navigationController.NavigationBar.TintColor = AppTheme.Instance.SelectedTheme.NavBarIconColor;
            navigationController.PushViewController(controller, false);

            return navigationController;

        }

        public static void AddShadowToView(this UIView view, float ShadowRadius, float cornerRadius, float shadowOpacity, UIColor color)
        {

            view.Layer.MasksToBounds = false;
            view.Layer.CornerRadius = cornerRadius;
            view.Layer.ShadowColor = color.CGColor;
            view.Layer.ShadowOpacity = shadowOpacity;
            view.Layer.ShadowOffset = new CGSize(0,0);
            view.Layer.ShadowRadius = ShadowRadius;
        }

        public static void ToCardView(this UIView mainView, float cornerRadius = 3.0f, float shadowRadius = 3.0f, float shadowOpacity = 0.2f, UIColor shadowColor = default(UIColor))
        {
           // UIBezierPath shadowPath = UIBezierPath.FromRect(mainView.Bounds);
            mainView.Layer.MasksToBounds = false;
            mainView.Layer.CornerRadius = cornerRadius;
            mainView.Layer.ShadowColor = shadowColor.CGColor;
            mainView.Layer.ShadowOffset = new CGSize(2, 2);
            mainView.Layer.ShadowOpacity = shadowOpacity;
            mainView.Layer.ShadowRadius = shadowRadius;
            //mainView.Layer.ShadowPath = shadowPath.CGPath;
        }

        public static void ToCardViewWithOffset(this UIView mainView, float cornerRadius = 3.0f, float shadowRadius = 3.0f, float shadowOpacity = 0.1f, float rightOffset = 2.0f, float botomOffset = 2.0f)
        {
            mainView.Layer.CornerRadius = cornerRadius;
            mainView.Layer.ShadowColor = UIColor.Black.CGColor;
            mainView.Layer.ShadowOpacity = shadowOpacity;
            mainView.Layer.ShadowRadius = shadowRadius;
            mainView.Layer.ShadowOffset = new CoreGraphics.CGSize(rightOffset, botomOffset);
            mainView.Layer.MasksToBounds = false;
        }

        public static UINavigationController CreateWithNavigationControllerWithMenu(this UIViewController controller, Action menuAction, UIImage image, UIColor imageColor, bool isRight = true, string title = "", string fontName = "")
        {

            var navigationController = controller.CreateWithNavigationController();

            navigationController.NavigationBar.TopItem.InitBarButtonToNav(image, imageColor, menuAction, isRight, title, fontName);

            return navigationController;


        }

        public static void ShareUrl(this UIViewController vc, string url)
        {

            var text = NSObject.FromObject(url);

            var item = new[] { text };

            var activity = new UIActivityViewController(item, null);

            var idiom = UIDevice.CurrentDevice.UserInterfaceIdiom;

            if (idiom == UIUserInterfaceIdiom.Pad)
            {
                var window = UIApplication.SharedApplication.KeyWindow;

                activity.PopoverPresentationController.SourceView = vc.NavigationItem.TitleView;
            }

            vc.PresentViewController(activity, true, null);
        }

        public static void AddAsChildViewController(this UIViewController vc, UIViewController parentViewController)
        {
            parentViewController.AddChildViewController(vc);

            vc.View.Bounds = parentViewController.View.Bounds;

            parentViewController.View.AddSubview(vc.View);

            vc.DidMoveToParentViewController(parentViewController);
        }

        public static void AddVcInView(this UIViewController vc, UIView view, UIViewController child)
        {
            vc.AddChildViewController(child);

            child.View.Frame = new CoreGraphics.CGRect(0, 0, view.Frame.Width, view.Frame.Height);

            view.AddSubview(child.View);

            child.DidMoveToParentViewController(vc);
        }

        public static void AddAsChildViewController(this UIViewController vc, UIViewController parentViewController, UIView viewWhereToAdd)
        {
            parentViewController.AddChildViewController(vc);

            vc.View.Frame = new CGRect(0, 0, viewWhereToAdd.Frame.Width, viewWhereToAdd.Frame.Height);

            viewWhereToAdd.AddSubview(vc.View);

            vc.DidMoveToParentViewController(parentViewController);
        }
        #endregion

        #region Animations

        public static void AddShimmer(this UIView view,UIColor shimerColor = default(UIColor))
        {
            CAGradientLayer gradientLayer = new CAGradientLayer();
            gradientLayer.Colors = new CoreGraphics.CGColor[] { UIColor.Clear.CGColor, UIColor.White.ColorWithAlpha(0.4f).CGColor, UIColor.Clear.CGColor };
            //gradientLayer.Locations = new Foundation.NSNumber[] {0.0,0.5f,1.0f };
            gradientLayer.StartPoint = new CGPoint(0.7f, 1.0f);
            gradientLayer.EndPoint = new CGPoint(0.0f, 0.8f);
            gradientLayer.Frame = view.Bounds; ;

            //var angle =45 * Math.PI / 180;
            //gradientLayer.Transform = CATransform3D.MakeRotation((System.nfloat)angle, 0, 0, 1);
            //gradientLayer.Opacity = 0.5f;
            view.Layer.Mask = gradientLayer;

            if (shimerColor != default(UIColor))
            {
                view.BackgroundColor = shimerColor;
            }

            view.Layer.RemoveAllAnimations();

            var animation = CABasicAnimation.FromKeyPath("transform.translation.x");
            animation.Duration = 1.0;
            animation.From = new NSNumber(-view.Frame.Width);
            animation.To = new NSNumber(view.Frame.Width * 2);

            animation.RepeatCount = float.MaxValue;

            gradientLayer.AddAnimation(animation, "shimmering");

        }

        public static void Fade(this UIView view, bool isIn, double duration = 0.3, Action onFinished = null)
        {
            var minAlpha = (nfloat)0.0f;
            var maxAlpha = (nfloat)1.0f;

            view.Alpha = isIn ? minAlpha : maxAlpha;
            view.Transform = CGAffineTransform.MakeIdentity();
            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    view.Alpha = isIn ? maxAlpha : minAlpha;
                },
                onFinished
            );
        }

        public static void Scale(this UIView view, bool isIn, double duration = 0.3, Action onFinished = null)
        {
            var minAlpha = (nfloat)0.0f;
            var maxAlpha = (nfloat)1.0f;
            var minTransform = CGAffineTransform.MakeScale((nfloat)0.1, (nfloat)0.1);
            var maxTransform = CGAffineTransform.MakeScale((nfloat)1, (nfloat)1);

            view.Alpha = isIn ? minAlpha : maxAlpha;
            view.Transform = isIn ? minTransform : maxTransform;
            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    view.Alpha = isIn ? maxAlpha : minAlpha;
                    view.Transform = isIn ? maxTransform : minTransform;
                },
                onFinished
            );
        }

        public static void Zoom(this UIView view, bool isIn, double duration = 0.3, Action onFinished = null)
        {
            var minAlpha = (nfloat)0.4f;
            var maxAlpha = (nfloat)1.0f;
            var minTransform = CGAffineTransform.MakeScale((nfloat)1.3, (nfloat)1.3);
            var maxTransform = CGAffineTransform.MakeScale((nfloat)1, (nfloat)1);

            view.Alpha = isIn ? minAlpha : maxAlpha;
            view.Transform = isIn ? minTransform : maxTransform;
            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    view.Alpha = isIn ? maxAlpha : minAlpha;
                    view.Transform = isIn ? maxTransform : minTransform;
                },
                onFinished
            );
        }

        public static void ZoomWithMaxFactor(this UIView view, bool isIn, double duration = 0.3, float maxScaleFactor = 1.3f, float minScaleFactor = 1.0f, Action onFinished = null)
        {
            var minAlpha = (nfloat)0.4f;
            var maxAlpha = (nfloat)1.0f;
            var minTransform = CGAffineTransform.MakeScale((nfloat)maxScaleFactor, (nfloat)maxScaleFactor);
            var maxTransform = CGAffineTransform.MakeScale((nfloat)minScaleFactor, (nfloat)minScaleFactor);

            view.Alpha = isIn ? minAlpha : maxAlpha;
            view.Transform = isIn ? minTransform : maxTransform;
            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    view.Alpha = isIn ? maxAlpha : minAlpha;
                    view.Transform = isIn ? maxTransform : minTransform;
                },
                onFinished
            );
        }

        #endregion

        #region ImageView / Image

        public static void InitImageFromWeb(this UIImageView imageView, string url, string miniPlaceholder=null, string maxPlaceholder=null, bool isMini = false, bool shouldFillOnlyContent = false)
        {


            if (imageView == null) return;


            if (shouldFillOnlyContent)
                imageView.ContentMode = UIViewContentMode.ScaleAspectFit;

            string placeholder = isMini ? miniPlaceholder : maxPlaceholder;
            ImageService
                  .Instance

                .LoadUrl(url)
                .Success(() => {
                    if (shouldFillOnlyContent)
                        imageView.ContentMode = UIViewContentMode.ScaleAspectFill;
                })
                .ErrorPlaceholder(placeholder)
                .LoadingPlaceholder(placeholder)
                .Error((Exception obj) => {

                    string urll = url;
                    Console.WriteLine(obj);
                })
                  .Into(imageView);
        }

        public static void InitImageFromWeb(this UIImageView imageView, string url, bool isMini = false, bool shouldFillOnlyContent = false)
        {


            //if (imageView == null) return;


            //if (shouldFillOnlyContent)
            //    imageView.ContentMode = UIViewContentMode.ScaleAspectFit;

            //var currTheme = AppTheme.Instance.SelectedTheme;

            //string placeholder = isMini ? currTheme.LoadingPlaceHolderSmallPath : currTheme.LoadingPlaceHolderWidePath;
            //ImageService
                //  .Instance

                //.LoadUrl(url)
                //.Success(() => {
                //    if (shouldFillOnlyContent)
                //        imageView.ContentMode = UIViewContentMode.ScaleAspectFill;
                //})
                //.ErrorPlaceholder(placeholder)

                //.LoadingPlaceholder(placeholder)
                //.Error((Exception obj) => {

                //    string urll = url;
                //    Console.WriteLine(obj);
                //})
                  //.Into(imageView);
        }

        public static void InitImageFromWeb(this UIImageView imageView, string url, Action finishedSuccess,Action FinishedFail)
        {
            //if (imageView == null) return;

            //var currTheme = AppTheme.Instance.SelectedTheme;

           
            //ImageService
                //  .Instance
                  
                //.LoadUrl(url)

                //.Success(() => {
                //    finishedSuccess?.Invoke();
                //})
               

               
                //.Error((Exception obj) => {

                //    FinishedFail?.Invoke();
                //    string urll = url;
                //    Console.WriteLine(obj);
                //})
                  //.Into(imageView);
        }

        public static void HideShareImageView(this UIImageView imageView, string shareUrl)
        {
            if (string.IsNullOrEmpty(shareUrl) || string.IsNullOrWhiteSpace(shareUrl))
                imageView.Hidden = true;
            else
                imageView.Hidden = false;
        }

        public static UIImage MaxResizeImage(this UIImage sourceImage, float maxWidth, float maxHeight)
        {
            var sourceSize = sourceImage.Size;
            var maxResizeFactor = Math.Max(maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);
            if (maxResizeFactor > 1) return sourceImage;
            var width = maxResizeFactor * sourceSize.Width;
            var height = maxResizeFactor * sourceSize.Height;
            UIGraphics.BeginImageContext(new SizeF((float)width, (float)height));
            sourceImage.Draw(new RectangleF(0, 0, (float)width, (float)height));
            var resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }
        #endregion

        #region PlainViews

        public static UIActivityIndicatorView CreateIndicatorView(this UIView View)
        {
            var activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray)
            {
                Alpha = 1.0f,
                Color = UIColor.White
            };


            View.AddSubview(activityIndicator);
            activityIndicator.Center = new CoreGraphics.CGPoint(UIScreen.MainScreen.Bounds.Size.Width / 2, UIScreen.MainScreen.Bounds.Size.Height / 2);

            return activityIndicator;
        }

        public static void InitShadowToView(this UIView shadowView)
        {
            shadowView.Layer.MasksToBounds = false;
            shadowView.Layer.ShadowOffset = new CoreGraphics.CGSize(5, 5);//shadow kordinatebi
            shadowView.Layer.ShadowOpacity = 0.16f;
            shadowView.Layer.ShadowRadius = 3.5f;//spread
            shadowView.Layer.ShadowColor = UIColor.Black.CGColor;


        }

        public static void InitLightShadowToView(this UIView shadowView)
        {


            shadowView.Layer.MasksToBounds = false;
            shadowView.Layer.ShadowOffset = new CoreGraphics.CGSize(3, 3);//shadow kordinatebi
            shadowView.Layer.ShadowOpacity = 0.06f;
            shadowView.Layer.ShadowRadius = 3.5f;//spread
            shadowView.Layer.ShadowColor = UIColor.Black.CGColor;


        }
        #endregion

        #region Constraint
        public static void SetMultiplier(this NSLayoutConstraint constraint, float multiplier)
        {

            NSLayoutConstraint.DeactivateConstraints(new NSLayoutConstraint[] { constraint });

            var newConstraint = NSLayoutConstraint.Create(constraint.FirstItem, constraint.FirstAttribute,
                                                          constraint.Relation, constraint.SecondItem,
                                                          constraint.SecondAttribute,
                                                          multiplier,
                                                          constraint.Constant);
            newConstraint.Priority = constraint.Priority;
            newConstraint.ShouldBeArchived = constraint.ShouldBeArchived;
            newConstraint.SetIdentifier(constraint.GetIdentifier());
            NSLayoutConstraint.ActivateConstraints(new NSLayoutConstraint[] { newConstraint });
        }

        #endregion

        #region WebViewFont
        public static void InitWebViewContentWithStyle(this UIWebView webView, string html, UIColor color, UIColor bgColor)
        {

            var calibriFont = UIFont.FromName("Arial", 14);
            var htmlContent = string.Format(html);

            color.GetRGBA(out nfloat red, out nfloat green, out nfloat blue, out nfloat alpha);
            bgColor.GetRGBA(out nfloat redBg, out nfloat greenBg, out nfloat bluebg, out nfloat alphaBg);


            var r = (int)(red * 255) > 0 ? (int)(red * 255) : 0;
            var g = (int)(green * 255) > 0 ? (int)(green * 255) : 0;
            var b = (int)(blue * 255) > 0 ? (int)(blue * 255) : 0;

            var rb = (int)(redBg * 255) > 0 ? (int)(redBg * 255) : 0;
            var gb = (int)(greenBg * 255) > 0 ? (int)(greenBg * 255) : 0;
            var bb = (int)(bluebg * 255) > 0 ? (int)(bluebg * 255) : 0;



            string output = string.Format("<html><head><style>* iframe{{width: 100%; height: auto; margin-top : 5px; margin-bottom : 5px;}} {{ margin:0; padding:0;}} body {{ background-color: rgb({5},{6},{7}); padding: 15px 0 0 0;}}  p{{hyphens: auto;-webkit-hyphens: auto;-moz-hyphens: auto;-ms-hyphens: auto;word-break: break-word;word-break: break-all;-ms-word-break: break-all;overflow-wrap: break-word;word-wrap: break-word;font-family:{1};color:rgb({2},{3},{4})}} span {{font-family:{1};color:rgb({2},{3},{4})}}  li {{font-family:{1};color:rgb({2},{3},{4})}}</style></head><body>{0}</body></html>", htmlContent, calibriFont.FamilyName,
                                          r, g, b, rb, gb, bb);

            webView.LoadHtmlString(output, null);
        }

        #endregion

        #region CollectionView

        public static void InitCollectionViewSettings(this UICollectionView collectionView, UINib nib, NSString identifier, UICollectionViewScrollDirection direction, bool isPagingEnabled = false)
        {
            collectionView.RegisterNibForCell(nib, identifier);

            var Layout = new UICollectionViewFlowLayout
            {
                ScrollDirection = direction
            };

            collectionView.CollectionViewLayout = Layout;

            collectionView.ShowsHorizontalScrollIndicator = false;

            collectionView.PagingEnabled = isPagingEnabled;

        }
        #endregion

        #region Label

        public static bool IsLabelTruncated(this UILabel label, UIFont font)
        {
            var labelText = label.Text;

            var labelTextSize = (NSObject.FromObject(labelText) as NSString)
                .GetBoundingRect(new CGSize(nfloat.MaxValue, label.Frame.Height),
                                 NSStringDrawingOptions.UsesLineFragmentOrigin, attributes: new UIStringAttributes(new NSDictionary(labelText, NSUnderlineStyle.Single)),
                                 context: null).Size;

            var result = labelTextSize.Width > label.Bounds.Size.Width;

            return result;
        }

        public static void InitHTML(this UILabel label, string HTMLText)
        {

            if (string.IsNullOrEmpty(HTMLText))
            {
                label.Text = string.Empty;
                return;
            }

            var attr = new NSAttributedStringDocumentAttributes();
            var nsError = new NSError();
            attr.DocumentType = NSDocumentType.HTML;

            var myHtmlData = NSData.FromString(HTMLText, NSStringEncoding.Unicode);
            label.AttributedText = new NSAttributedString(myHtmlData, attr, ref nsError);

        }

        public static void LoadHtmlString(this UILabel label, string html)
        {

        }
        #endregion

        #region Bottom Tabbar

        public static void SetSelectionColors(this UITabBar tabBar, UIColor selectionColor, UIColor unselectedColor)
        {
            tabBar.TintColor = selectionColor;
            tabBar.UnselectedItemTintColor = unselectedColor;
            foreach (var item in tabBar.Items)
            {


                UITextAttributes txtAttributes = new UITextAttributes
                {
                    //Font = AppFont.Current.GetPrimaryFontBySize(9)
                };
                item.SetTitleTextAttributes(txtAttributes, UIControlState.Normal);
                item.SelectedImage = item.SelectedImage.GetImageWithColor(selectionColor);
                item.Image = item.SelectedImage.GetImageWithColor(unselectedColor);
            }
        }

        #endregion

        #region UITextField

        public static void MakeRoundedTextField(this UITextField textField, float cornerRadius, UIColor backgroundColor = default(UIColor), float textLeading = 5)
        {
            textField.Layer.MasksToBounds = true;
            textField.Layer.CornerRadius = cornerRadius;
            textField.Layer.BackgroundColor = backgroundColor.CGColor;
            textField.Layer.SublayerTransform = CATransform3D.MakeTranslation(textLeading, 0, 0);
        }

        public static void AddBorderToTextField(this UITextField textField, UIColor borderColor = null)
        {
            textField.Layer.MasksToBounds = true;
            textField.Layer.BorderWidth = 2;
            textField.Layer.BorderColor = borderColor.CGColor;
        }

        public static void AddBorderAndBackgroundToTextField(this UITextField textField, UIColor backgroundColor = null, UIColor borderColor = null)
        {
            textField.Layer.MasksToBounds = true;
            textField.Layer.BorderWidth = 2;
            textField.Layer.BorderColor = borderColor.CGColor;
            textField.Layer.BackgroundColor = backgroundColor.CGColor;
        }

        #endregion

    }

}



