
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eldrow.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Input : Editor
    {
        public Input()
        {
            InitializeComponent();
            this.TextChanged += Input_TextChanged;
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Text?.ToUpper() != Text)
                Text = Text.ToUpper();
        }
    }
}