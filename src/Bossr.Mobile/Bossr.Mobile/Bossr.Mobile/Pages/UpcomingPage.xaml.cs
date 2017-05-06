using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bossr.Mobile.Models;
using Bossr.Mobile.ViewModels;
using Xamarin.Forms;

namespace Bossr.Mobile.Pages
{
    public partial class UpcomingPage : ContentPage
    {
        private UpcomingPageViewModel UpcomingPageViewModel => (UpcomingPageViewModel)BindingContext;

        public UpcomingPage(World selectedWorld)
        {
            InitializeComponent();

            Icon = Device.OnPlatform("clock.png", "", ""); // Todo: -> XAML

            BindingContext = new UpcomingPageViewModel();
            UpcomingPageViewModel.SelectedWorld = selectedWorld;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await UpcomingPageViewModel.ReadUpcomingAsync();
        }

        private async void ListView_OnRefreshing(object sender, EventArgs e)
        {
            await UpcomingPageViewModel.ReadUpcomingAsync();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
