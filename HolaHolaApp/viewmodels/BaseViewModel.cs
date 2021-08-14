using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace HolaHolaApp.viewmodels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void PropertyChangedEvent(string Propertyname) {

            var handler = PropertyChanged;
            if (handler != null) {

                handler(this, new PropertyChangedEventArgs(Propertyname));
            }
        }

        public void UserMessage(string message)
        {

            App.Current.MainPage.DisplayAlert("", message, "OK");
        }

        public void Navigation(Page page) {
            App.Current.MainPage.Navigation.PushAsync(page);
        }
        public void NavigationBack()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
        public async void NavigationRoot()
        {
           // App.Current.MainPage.Navigation.PopToRootAsync();
            await App.Current.MainPage.Navigation.PopToRootAsync();

        }
    }
}
