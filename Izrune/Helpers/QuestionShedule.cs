using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Izrune.Helpers
{
    public class QuestionShedule
    {
        public int Position { get; set; }
        public bool IsCurrent { get; set; }
        public bool AlreadeBe { get; set; }
    }

}