using GuiaDCEA.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace GuiaDCEA.Views
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