using System;
using System.Collections.Generic;
using FitMetrics.ViewModels;
using FitMetrics.Views;
using Xamarin.Forms;

namespace FitMetrics
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(Geodude), typeof(Geodude));
        }

    }
}
