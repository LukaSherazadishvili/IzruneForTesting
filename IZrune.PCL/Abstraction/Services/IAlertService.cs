using System;
using System.Collections.Generic;
using System.Text;

namespace IZrune.PCL.Abstraction.Services
{
    public interface IAlertService
    {
        void ShowAlerDialog(string Title,string Message);
    }
}
