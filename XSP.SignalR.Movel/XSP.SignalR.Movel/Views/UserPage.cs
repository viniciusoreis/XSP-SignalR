using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XSP.SignalR.Movel.Views
{
    public class UserPage : ContentPage
    {
        // Controles do Xamarin.Forms
        Entry _txtName;
        Button _btnSave;
        Label _lblName;

        public UserPage()
        {
            _lblName = new Label
            {
                Text = "Enter a name:",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            _txtName = new Entry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
            };
            _btnSave = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Text = "Save"
            };

            _btnSave.Clicked += OnSave;

            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(
                    left: 0,
                    right: 0,
                    bottom: 0,
                    top: Device.OnPlatform(iOS: 20, Android: 0, WinPhone: 0)),
                Children = {
                    _lblName,
                    _txtName,
                    _btnSave
                }
            };
        }

        private void OnSave(object sender, EventArgs e)
        {
            App.Current.MainPage = new ChatPage(_txtName.Text);
        }
    }
}
