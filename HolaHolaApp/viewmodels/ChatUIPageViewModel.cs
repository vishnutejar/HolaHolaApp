using HolaHolaApp.models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace HolaHolaApp.viewmodels
{
    public class ChatUIPageViewModel : BaseViewModel
    {
        public string _EnteredMgs;
        public string  EnteredMgs
        {
            get
            {
                return _EnteredMgs;
            }
            set
            {
                _EnteredMgs = value;
                PropertyChangedEvent("EnteredMgs");
            }
        }
        private Users users;

        public ObservableCollection<MessageCenter> _LstCurrentUsersMSG { get; set; }

      public  ObservableCollection<MessageCenter> LstCurrentUsersMSG
        {
            get
            {
                return _LstCurrentUsersMSG;
            }
            set
            {
                _LstCurrentUsersMSG = value;
                PropertyChangedEvent("LstCurrentUsersMSG");
            }
        }

       public ICommand SendCommand { get; set; }
        public ChatUIPageViewModel(Users users)
        {
            this.users = users;
            UserName = users.username;
            LstCurrentUsersMSG = new ObservableCollection<MessageCenter>();

            SendCommand = new Command(SendUserMessage);
        }

        private void SendUserMessage(object obj)
        {

            if (string.IsNullOrEmpty(EnteredMgs))
            {

                UserMessage("Enter Your Message");
            }
            else {

                var msgdata = new MessageCenter
                {
                    Messages = EnteredMgs,
                    MsgDate = DateTime.Now,
                };
                LstCurrentUsersMSG.Add(msgdata);
                EnteredMgs = string.Empty;
            }
          
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
