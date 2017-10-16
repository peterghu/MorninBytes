using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Interactivity;
using MorninBytes.ViewModels;

namespace MorninBytes.Views
{
    public partial class WebsiteManager
    {
        WebsiteUIViewModel _presenter = new WebsiteUIViewModel();

        public WebsiteManager()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }


        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

    }
}
