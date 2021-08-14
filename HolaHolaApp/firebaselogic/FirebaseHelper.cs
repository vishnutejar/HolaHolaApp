using Firebase.Database;
using Firebase.Database.Query;
using HolaHolaApp.apputils;
using HolaHolaApp.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HolaHolaApp.firebaselogic
{
    public class FirebaseHelper
    {
        public static FirebaseClient firebase = new FirebaseClient(Constants.FireBaseURL);

        //Read All    
        public static async Task<List<Users>> GetAllUser()
        {
            try
            {
                var userlist = (await firebase
                .Child(Constants.Users)
                .OnceAsync<Users>()).Select(item =>
                new Users
                {
                    PhoneNumber = item.Object.PhoneNumber,
                    Password = item.Object.Password,
                    email = item.Object.email,
                    username = item.Object.username
                }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Read     
        public static async Task<Users> GetUser(string phnumber)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebase
                .Child(Constants.Users)
                .OnceAsync<Users>();
                return allUsers.Where(a => a.PhoneNumber == phnumber).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        //Inser a user    
        public static async Task<bool> AddUser(Users users)
        {
            try
            {


                await firebase
                .Child(Constants.Users)
                .PostAsync(new Users()
                {
                    PhoneNumber = users.PhoneNumber,
                    Password = users.Password,
                    email = users.email,
                    username = users.username
                });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        public static async Task<bool> UpdateUser(string Phnumber, string password, Users users)
        {
            try
            {


                var toUpdateUser = (await firebase
                .Child(Constants.Users)
                .OnceAsync<Users>()).Where(a => a.Object.PhoneNumber == Phnumber).FirstOrDefault();
                await firebase
                .Child(Constants.Users)
                .Child(toUpdateUser.Key)
                .PutAsync(new Users()
                {
                    email = users.email,
                    Password = users.Password,
                    username = users.username
                });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        //Delete User
        public static async Task<bool> DeleteUser(string phnum)
        {
            try
            {


                var toDeletePerson = (await firebase
                .Child(Constants.Users)
                .OnceAsync<Users>()).Where(a => a.Object.PhoneNumber == phnum).FirstOrDefault();
                await firebase.Child(Constants.Users).Child(toDeletePerson.Key).DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //insertChatData
        public static async Task<bool> AddChatData(MessageCenter messageCenter)
        {
            try
            {

                await firebase
                .Child(Constants.Chats)
                .PostAsync(messageCenter);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        public static async Task<List<MessageCenter>> GetSelectedUserChats(string phonenumber)
        {
            try
            {
                var loginusermobilenumber = Preferences.Get(Constants.UsersPhoneNumber, "0");
                var userlist = (await firebase
                .Child(Constants.Chats)
                .OnceAsync<MessageCenter>()).Where(user => user.Object.ReceiverPhonumber.Equals(phonenumber)
                &&user.Object.SenderPhoneNumber.Equals(loginusermobilenumber)).Select(item =>
                  new MessageCenter
                  {
                      Messages = item.Object.Messages,
                      SenderPhoneNumber = item.Object.SenderPhoneNumber,
                      ReceiverPhonumber= item.Object.ReceiverPhonumber,
                      MsgDate = item.Object.MsgDate
                  }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }



    }
}
