using IZrune.PCL.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.extensions
{
   public static class Extensions
    {
        public static string ConverEnumToInt(this QuezCategory Categor )
        {
            var Result = Convert.ToInt32(Categor);

            return Result.ToString();
        }
    }
}
