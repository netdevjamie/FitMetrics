using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitMetrics.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMetrics.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Geodude : ContentPage
    {
        public Geodude()
        {
            InitializeComponent();
        }
        public static async Task NavigateTo()
        {
            Console.Write("I'm trying to navigate here");
            var vm = new GeodudeViewModel();
            await vm.Startup();
            await Shell.Current.GoToAsync($"{nameof(Geodude)}");
            Shell.Current.BindingContext = vm;
            _ = vm.Poll(500);
        }

        public static void WaitAndExecute(Action a)
        {
            var task = new Task((() =>
            {
                Task.Delay(1000);
                a();
            }));

        }
    }

}