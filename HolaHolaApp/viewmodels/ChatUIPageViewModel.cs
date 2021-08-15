using HolaHolaApp.apputils;
using HolaHolaApp.firebaselogic;
using HolaHolaApp.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HolaHolaApp.viewmodels
{
    public class ChatUIPageViewModel : BaseViewModel
    {
        string loginUserPhoneNumber = Preferences.Get(Constants.UsersPhoneNumber, "0");

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
       public ICommand InitCommand { get; set; }
        public ChatUIPageViewModel(Users users)
        {
            this.users = users;
            UserName = users.username;
            LstCurrentUsersMSG = new ObservableCollection<MessageCenter>();

            SendCommand = new Command(SendUserMessage);
            InitCommand = new Command(InitCommandChatUiData);
            InitCommand.Execute(null);

        }

        private async void InitCommandChatUiData(object obj)
        {
            LstCurrentUsersMSG.Clear();
            var messages =  await FirebaseHelper.GetSelectedUserChats(users.PhoneNumber);

            foreach (var msgs in messages)
            {
                LstCurrentUsersMSG.Add(msgs);
            }
        }

        private async void SendUserMessage(object obj)
        {

            if (string.IsNullOrEmpty(EnteredMgs))
            {

                UserMessage("Enter Your Message");
            }
            else {

                var msgdata = new MessageCenter
                {
                    Messages = EnteredMgs,
                    SenderPhoneNumber = loginUserPhoneNumber,
                    ReceiverPhonumber = users.PhoneNumber,
                   
                };
                var items = await FirebaseHelper.AddChatData(msgdata);
                if (items)
                {
                    LstCurrentUsersMSG.Add(msgdata);
                }
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
