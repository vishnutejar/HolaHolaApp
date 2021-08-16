using HolaHolaApp.apputils;
using HolaHolaApp.interfaces;
using HolaHolaApp.views;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HolaHolaApp
{
    public partial class App : Application
    {
        INotificationManager notificationManager;
        int notificationNumber = 0;

        public App()
        {
            InitializeComponent();
            notificationManager = DependencyService.Get<INotificationManager>();
            //notificationManager.NotificationReceived += (sender, eventArgs) =>
            //{
            //    var evtData = (NotificationEventArgs)eventArgs;
            //    ShowNotification(evtData.Title, evtData.Message);
            //};

            var navigationpage = Preferences.Get(Constants.IsLogin, false);
            if (navigationpage)
            {
                MainPage = new NavigationPage(new HomePage());

            }
            else 
            {
                MainPage = new NavigationPage(new LoginPage());

            }


            
            // Push message received event
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                notificationNumber++;
                string title = $"Local Notification #{notificationNumber}";
                string message = $"You have now received {notificationNumber} notifications!";
                List<string> messge=new List<string>();
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                    messge.Add(data.Value.ToString());
                }
                notificationManager.SendNotification(messge[0], messge[1]);
                System.Diagnostics.Debug.WriteLine("Received");

            };
            //Push message received event
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

            };
        }
      public  void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
               // stackLayout.Children.Add(msg);
            });
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
