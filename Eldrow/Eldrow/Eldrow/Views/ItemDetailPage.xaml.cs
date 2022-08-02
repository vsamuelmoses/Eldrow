using Eldrow.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Eldrow.Views
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