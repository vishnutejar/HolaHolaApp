using HolaHolaApp.apputils;
using HolaHolaApp.firebaselogic;
using HolaHolaApp.models;
using HolaHolaApp.views;
using Plugin.FirebasePushNotification;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HolaHolaApp.viewmodels
{
    public class SignUpPageViewModel : BaseViewModel
    {
        string username, phonenumber, email, password, confirmpassword;
        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                PropertyChangedEvent("UserName");
            }
        }
        public string Phonenumber
        {
            get
            {
                return phonenumber;
            }
            set
            {
                phonenumber = value;
                PropertyChangedEvent("Phonenumber");
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                PropertyChangedEvent("Email");
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                PropertyChangedEvent("Password");
            }
        }
        public string ConfirmPassword
        {
            get
            {
                return confirmpassword;
            }
            set
            {
                confirmpassword = value;
                PropertyChangedEvent("ConfirmPassword");
            }
        }

        public ICommand SignUpCommand { get; set; }
        public SignUpPageViewModel()
        {
            SignUpCommand = new Command(ValidateSignupData);
        }

        private async void ValidateSignupData(object obj)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                UserMessage("Enter UserName");
            }
            else if (string.IsNullOrEmpty(Phonenumber))
            {

                UserMessage("Enter PhoneNumber");
            }
            else if (string.IsNullOrEmpty(Email))
            {

                UserMessage("Enter Email");
            }
            else if (string.IsNullOrEmpty(Password))
            {

                UserMessage("Enter Password");
            }
            else if (string.IsNullOrEmpty(ConfirmPassword))
            {

                UserMessage("Enter ConfirmPassword");
            }
            else if (!Password.Equals(ConfirmPassword))
            {

                UserMessage("Enter Password and ConfirmPassword same");
            }
            else
            {
                //CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
                //{
                //    System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
                //    App.Current.Properties["Fcmtocken"] = p.Token ?? "";
                //    App.Current.SavePropertiesAsync();

                //    Preferences.Set(Constants.Fcmtocken, p.Token);
                //};

                Preferences.Set(Constants.Fcmtocken, CrossFirebasePushNotification.Current.Token);

                var userdata = new Users
                {
                    PhoneNumber = Phonenumber,
                    email = Email,
                    Password = Password,
                    username = UserName,
                    FcmToken= CrossFirebasePushNotification.Current.Token
                };
                bool IsUserAdded= await FirebaseHelper.AddUser(userdata);
                if (IsUserAdded)
                {
                    UserMessage("Thanks for signup in HolaHola App,");
                    Preferences.Set(Constants.IsLogin, true);
                    Preferences.Set(Constants.UsersPhoneNumber, userdata.PhoneNumber);
                   
                    Navigation(new HomePage());

                }
                else {
                    UserMessage("Check your entered infomration");
                    Preferences.Set(Constants.IsLogin, false);

                }

            }
    }
}
}
