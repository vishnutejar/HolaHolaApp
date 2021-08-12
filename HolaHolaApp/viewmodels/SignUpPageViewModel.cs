using HolaHolaApp.firebaselogic;
using HolaHolaApp.models;
using System.Windows.Input;
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
                var userdata = new Users
                {
                    PhoneNumber = Phonenumber,
                    email = Email,
                    Password = Password,
                    username = UserName
                };
                bool IsUserAdded= await FirebaseHelper.AddUser(userdata);
                if (IsUserAdded)
                {
                    UserMessage("Thanks for signup in HolaHola App,");

                }
                else {
                    UserMessage("Check your entered infomration");
                }

            }
    }
}
}
