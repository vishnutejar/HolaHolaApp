using HolaHolaApp.apputils;
using HolaHolaApp.firebaselogic;
using HolaHolaApp.models;
using HolaHolaApp.views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HolaHolaApp.viewmodels
{
    public class HomePageViewModel : BaseViewModel
    {

        public Users _selectionItemvalue { get; set; }
        public Users selectionItemvalue
        {
            get
            {

                return _selectionItemvalue;
            }
            set
            {

                _selectionItemvalue = value;
                PropertyChangedEvent("selectionItemvalue");
            }
        }

        ObservableCollection<Users> _LstOfusers;
        public ObservableCollection<Users> LstOfusers
        {
            get
            {

                return _LstOfusers;
            }
            set
            {

                _LstOfusers = value;
                PropertyChangedEvent("LstOfusers");
            }

        }
        ICommand InitHomePage { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public HomePageViewModel()
        {
            InitHomePage = new Command(InitUserData);
            SelectionChangedCommand = new Command(ItemSelectionChanged);
            InitHomePage.Execute(null);

        }

        private void ItemSelectionChanged(object obj)
        {

            Navigation(new ChatUIPage(selectionItemvalue));
        }

        private async void InitUserData(object obj)
        {
            var loginUserPhoneNumber = Preferences.Get(Constants.UsersPhoneNumber, "0");

            var _lstOfUser = await FirebaseHelper.GetAllUser();

            var lstofOtherUser = _lstOfUser.Where(predicate: u => u.PhoneNumber != loginUserPhoneNumber).ToList();

            LstOfusers = new ObservableCollection<Users>(lstofOtherUser);

        }
    }
}
