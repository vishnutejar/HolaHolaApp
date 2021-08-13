using HolaHolaApp.models;

namespace HolaHolaApp.viewmodels
{
    public class ChatUIPageViewModel : BaseViewModel
    {
        private Users users;

        public ChatUIPageViewModel(Users users)
        {
            this.users = users;
            UserName = users.username;
        }

        public string _usrname { get; set; }
        public string UserName
        {

            get
            {
                return _usrname;
            }
            set
            {
                _usrname = value;
                PropertyChangedEvent("UserName");
            }
        }
    }
}
