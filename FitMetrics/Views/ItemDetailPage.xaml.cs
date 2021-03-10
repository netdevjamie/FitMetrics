using System.ComponentModel;
using Xamarin.Forms;
using FitMetrics.ViewModels;

namespace FitMetrics.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}