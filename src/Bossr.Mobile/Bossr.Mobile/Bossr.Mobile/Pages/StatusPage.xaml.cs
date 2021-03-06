﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bossr.Mobile.Models;
using Bossr.Mobile.ViewModels;
using Xamarin.Forms;

namespace Bossr.Mobile.Pages
{
    public partial class StatusPage : TabbedPage
    {
        public StatusPage(World selectedWorld)
        {
            InitializeComponent();
            BindingContext = new StatusPageViewModel(selectedWorld);

            Children.Add(new SpawnablePage(selectedWorld));
            Children.Add(new RecentPage(selectedWorld));
            Children.Add(new UpcomingPage(selectedWorld));
        }
    }
}
