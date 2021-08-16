using HolaHolaApp.apputils;
using HolaHolaApp.firebaselogic;
using HolaHolaApp.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
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



                try
                {
                    var FCMTockenValue = users.FcmToken;
                    FCMBody body = new FCMBody();
                    FCMNotification notification = new FCMNotification();
                    notification.title = users.username;
                    notification.body = msgdata.Messages;
                    FCMData data = new FCMData();
                    data.key1 = users.username;
                    data.key2 = msgdata.ReceiverPhonumber;
                    data.key3 = msgdata.SenderPhoneNumber;
                    data.key4 = msgdata.MsgDate;
                    data.key4 = msgdata.Messages;
                    body.to =  FCMTockenValue;
                    body.notification = notification;
                    body.data = data;
                    var isSuccessCall = SendNotification(body).Result;
                    if (isSuccessCall)
                    {
                        //  App.C DisplayAlert("Alart", "Notifications Send Successfully", "Ok");
                    }
                    else
                    {
                        // DisplayAlert("Alart", "Notifications Send Failed", "Ok");
                    }
                }
                catch (Exception ex)
                {
                }
            }

        }
        public async Task<bool> SendNotification(FCMBody fcmBody)
        {
            try
            {
                var httpContent = JsonConvert.SerializeObject(fcmBody);
                var client = new HttpClient();
                var authorization = string.Format("key={0}", "AAAAWs9XPD4:APA91bFJYlBxztZolo7_OVlit8cX4lofnyvniO8JsM6Ix0fE30eqSzPW9NHsAweychu_cDXxHkwbwj-yotOOwPkWLpfFD_zaCx1DHeZR3_IFqgvtabZ2QuH-NWxzJIwXgCnqhTAG4bBw");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
                var stringContent = new StringContent(httpContent);
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                string uri = "https://fcm.googleapis.com/fcm/send";
                var response = await client.PostAsync(uri, stringContent).ConfigureAwait(false);
                var result = response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (TaskCanceledException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
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
