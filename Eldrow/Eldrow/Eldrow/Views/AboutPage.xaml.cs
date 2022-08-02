using Eldrow.Extensions;
using Eldrow.ViewModels;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eldrow.Views
{
    public partial class AboutPage : ContentPage, IDialogs
    {
        public AboutPage()
        {
            InitializeComponent();

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AboutPage)).Assembly;
            var stream = assembly.GetManifestResourceStream(@"Eldrow.Words.Four.txt");
            var words = new List<string>();
            using (var reader = new System.IO.StreamReader(stream))

            {
                while (!reader.EndOfStream)
                    words.Add(reader.ReadLine());
            }

            var hidden = words.Random();

            Word.Create(hidden)
                .IfSucc(word => {
                    BindingContext = new AboutViewModel(this, word);
                        });

        }

        public Task ShowMessageAsync(string title, string message)
        {
            return DisplayAlert(title, message, "OK");
        }
    }
}