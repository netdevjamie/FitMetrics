using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FitMetrics.Services;
using FitMetrics.Views;

namespace FitMetrics
{
    public partial class App : Application
    {

        public App()
        {
            Console.WriteLine("can you hear me now?");
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            Console.Error.WriteLine("can you error now?");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
