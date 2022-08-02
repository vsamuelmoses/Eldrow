using Eldrow.ViewModels;
using Eldrow.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Eldrow
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
