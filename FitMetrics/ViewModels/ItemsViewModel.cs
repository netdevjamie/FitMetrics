using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using FitMetrics.Models;
using FitMetrics.Views;

namespace FitMetrics.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        Item _selectedItem;
        string _error;


        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command LinkCommand { get; }
        public Command<Item> ItemTapped { get; }

        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                this.SetProperty(ref _error, value);
            }
        }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            LinkCommand = new Command(OnClick);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnClick(object obj)
        {
            Console.WriteLine("Le click");
            try
            {
                await Geodude.NavigateTo();
            }
            catch (Exception ex)
            {
                FMetrics.BReusable.Logging.logEx(ex);
                this.Error = ex.Message;
            }
        }

        async void OnItemSelected(Item item)
        {
            Console.WriteLine("onitem selected firing");
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}