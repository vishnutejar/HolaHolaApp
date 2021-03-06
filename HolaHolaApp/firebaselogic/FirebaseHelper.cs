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
                    FcmToken=item.Object.FcmToken,
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
                .PostAsync(users);
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
            var loginuserPhNum = Preferences.Get(Constants.UsersPhoneNumber,"0");
            var selectedUserPhNum = phonenumber;

            try
            {
                var userlist = (await firebase
                .Child(Constants.Chats)
                .OnceAsync<MessageCenter>()).Where(
                    user1=>user1.Object.ReceiverPhonumber.Equals(selectedUserPhNum)
                    &&
                    user1.Object.SenderPhoneNumber.Equals(loginuserPhNum)
                    ||
                    user1.Object.ReceiverPhonumber.Equals(loginuserPhNum)
                    &&
                    user1.Object.SenderPhoneNumber.Equals(selectedUserPhNum)

                    ).Select(item =>
                new MessageCenter
                {
                    SenderPhoneNumber = item.Object.SenderPhoneNumber,
                    Messages = item.Object.Messages,
                    MsgDate = item.Object.MsgDate,
                    ReceiverPhonumber = item.Object.ReceiverPhonumber,
                   
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
