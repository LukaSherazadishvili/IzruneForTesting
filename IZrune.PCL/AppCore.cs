﻿using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Implementation.Services;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using  MpdcContainer= ServiceContainer.ServiceContainer;
namespace IZrune.PCL
{
  public  class AppCore
    {
        private static AppCore _instance;
        public static AppCore Instance => _instance ?? (_instance = new AppCore());

        public string CurrentUserToken = "";

        public bool IsOnline { get { return CrossConnectivity.Current.IsConnected; } }

        public IAlertService Alertdialog { get; set; }

        public void InitServices()
        {
           // Alertdialog = dialog;
            MpdcContainer.Instance.Register<ILoginServices, LoginServices>(new LoginServices());
            MpdcContainer.Instance.Register<IQuezServices, QuezServices>(new QuezServices());
            MpdcContainer.Instance.Register<IStatisticServices, StatisticServices>(new StatisticServices());
            MpdcContainer.Instance.Register<IUserServices, UserServices>(new UserServices());
            MpdcContainer.Instance.Register<IRegistrationServices, RegistrationServices>(new RegistrationServices());
            MpdcContainer.Instance.Register<INewsService, NewsService>(new NewsService());
            MpdcContainer.Instance.Register<IPaymentService, PaymentService>(new PaymentService());
        }





    }
}
