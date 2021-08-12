using HolaHolaApp.firebaselogic;
using HolaHolaApp.models;
using HolaHolaApp.views;
using System.Windows.Input;
using Xamarin.Forms;

namespace HolaHolaApp.viewmodels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }

        string phonenumber, password;
        public string PhoneNumber
        {
            get
            {

                return phonenumber;
            }
            set
            {

                phonenumber = value;
                PropertyChangedEvent("PhoneNumber");
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

        public LoginPageViewModel()
        {
            LoginCommand = new Command(ValidateUser);
            SignUpCommand = new Command(GotoSignUp);
        }

        private void GotoSignUp(object obj)
        {
            Navigation(new SignUpPage());
        }

        private async void ValidateUser(object obj)
        {
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                UserMessage("Enter PhoneNumber");
            }
            else if (string.IsNullOrEmpty(Password))
            {

                UserMessage("Enter Password");
            }
            else {

                var users = await FirebaseHelper.GetUser(PhoneNumber);

                if (users != null)
                {
                    if (users.PhoneNumber.Equals(PhoneNumber) && users.Password.Equals(Password))
                    {

                        UserMessage("welcome to Holahola APp");
                    }
                    else
                    {

                        UserMessage("Check your Phone Number and Password");

                    }
                }
                else {
                    UserMessage("User Not Found Kindly signup!!");
                    Navigation(new SignUpPage());

                }
            }

        }
    }
}
