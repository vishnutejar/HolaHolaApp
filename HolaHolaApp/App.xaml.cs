using HolaHolaApp.apputils;
using HolaHolaApp.views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HolaHolaApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var navigationpage = Preferences.Get(Constants.IsLogin, false);
            if (navigationpage)
            {
                MainPage = new NavigationPage(new HomePage());

            }
            else 
            {
                MainPage = new NavigationPage(new LoginPage());

            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
