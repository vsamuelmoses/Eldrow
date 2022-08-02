
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
            if (Text.ToUpper() != Text)
                Text = Text.ToUpper();
        }

        //public string InputTxt
        //{
        //    get { return (string)GetValue(InputTxtProperty); }
        //    set { SetValue(InputTxtProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        //public static readonly BindableProperty InputTxtProperty =
        //    BindableProperty.Create("InputTxt", typeof(string), typeof(Input), "", defaultBindingMode: BindingMode.OneWay, null, propertyChanged:OnTextChanged);

        //private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    var thisControl = (Input)bindable;
        //    thisControl.UpdateText((string)newValue);
        //}

        //private void UpdateText(string newValue)
        //{
        //    Text = newValue?.ToUpper();
        //}
    }
}