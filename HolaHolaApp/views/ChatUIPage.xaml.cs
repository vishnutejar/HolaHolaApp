using HolaHolaApp.models;
using HolaHolaApp.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HolaHolaApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatUIPage : ContentPage
    {
        public ChatUIPage()
        {
            InitializeComponent();
        }
        public ChatUIPage(Users users)
        {
            InitializeComponent();
            var vm = new ChatUIPageViewModel(users);
            this.BindingContext = vm;
        }
    }
}