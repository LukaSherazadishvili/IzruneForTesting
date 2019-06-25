using System;
using UIKit;

namespace Izrune.iOS.Utils
{
    public static class AppColors
    {

        public static UIColor Succesful => UIColor.FromRGB(73, 181, 65);

        public static UIColor LightGreen => UIColor.FromRGBA(152, 216, 147, 255);

        public static UIColor Tint => UIColor.FromRGB(203, 135, 214);

        public static UIColor ErrorTitle => UIColor.FromRGB(231, 76, 60);

        public static UIColor LightRed => UIColor.FromRGBA(255, 160, 151, 255);

        public static UIColor TextFieldBackground => UIColor.FromRGB(243, 243, 243);

        public static UIColor PlaceHolder => UIColor.FromRGB(184, 184, 184);

        public static UIColor TextFieldTextColor => UIColor.FromRGB(106, 106, 106);

        public static UIColor TitleColor => UIColor.FromRGB(174, 113, 183);

        public static UIColor GreenShadow => UIColor.FromRGBA(73, 181, 65, 0);

        public static UIColor RedShadow => UIColor.FromRGBA(231, 76, 60, 153);

        public static UIColor UnselectedColor => UIColor.FromRGB(106,106,106);

        public static UIColor[] PurpleGradient => new UIColor[] { UIColor.FromRGB(153, 38, 173), TitleColor };

        public static UIColor[] YellowGradient => new UIColor[] { UIColor.FromRGB(253, 169, 43), UIColor.FromRGB(253, 200, 44) };

        public static UIColor GreenBg => UIColor.FromRGBA(73, 181, 65, 127);

        public static UIColor RedBg => UIColor.FromRGBA(231, 76, 60, 127);


    }
}
