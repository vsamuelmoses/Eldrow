using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eldrow.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ball : ContentView
    {
        public Ball()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(Ball), "-", BindingMode.OneWay, null, propertyChanged: OnPropertyChanged);

        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisControl =(Ball)bindable;
            thisControl.UpdateText((string)newValue);
        }

        private void UpdateText(string newValue)
        {
            label.Text = newValue;
        }

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create("Color", typeof(Color), typeof(Ball), Color.DarkRed, BindingMode.OneWay, propertyChanged: OnColorChanged);

        private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisControl = (Ball)bindable;
            thisControl.UpdateColor((Color)newValue);
        }

        private void UpdateColor(Color color)
        {
            frame.BackgroundColor = color;
        }
    }
}