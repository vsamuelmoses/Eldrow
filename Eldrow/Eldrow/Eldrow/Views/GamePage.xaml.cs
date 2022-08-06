using Eldrow.Extensions;
using Eldrow.Models;
using Eldrow.Services;
using Eldrow.ViewModels;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eldrow.Views
{
    public partial class GamePage : ContentPage, IDialogs
    {
        public GamePage()
        {
            InitializeComponent();

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(GamePage)).Assembly;
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
                    BindingContext = new GameViewModel(this, word);
                        });

        }

        public Task ShowMessageAsync(string title, string message)
        {
            return DisplayAlert(title, message, "OK");
        }
    }
}