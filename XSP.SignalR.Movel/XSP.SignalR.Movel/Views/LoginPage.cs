using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.ServiceModel;
using System.Text;

using Xamarin.Forms;

namespace XSP.SignalR.Movel.Views
{
    public class LoginPage : ContentPage
    {
        // Controles do Xamarin.Forms
        Entry _txtUsername;
        Entry _txtPassword;
        Button _btnLogin;
        Label _lblUsername;
        Label _lblPassword;

        public LoginPage()
        {
            #region Layout

            _lblUsername = new Label
            {
                Text = "Username:",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            _lblPassword = new Label
            {
                Text = "Password:",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            _txtUsername = new Entry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
            };

            _txtPassword = new Entry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                IsPassword = true
            };

            _btnLogin = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Text = "Login"
            };

            _btnLogin.Clicked += OnClick;

            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(
                    left: 0,
                    right: 0,
                    bottom: 0,
                    top: Device.OnPlatform(iOS: 20, Android: 0, WinPhone: 0)),
                Children = {
                    _lblUsername,
                    _txtUsername,
                    _lblPassword,
                    _txtPassword,
                    _btnLogin
                }
            };

            #endregion
        }

        private void OnClick(object sender, EventArgs e)
        {
            LoginClient client = new LoginClient(new BasicHttpBinding(),
                                                 new EndpointAddress("http://localhost:63853/wcf/login.svc"));
            client.LogarCompleted += OnLogar;
            client.LogarAsync(_txtUsername.Text, _txtPassword.Text);

        }

        private void OnLogar(object sender, LogarCompletedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                string error = null;
                if (e.Error != null)
                    error = e.Error.Message;
                else if (e.Cancelled)
                    error = "Cancelled";

                if (!string.IsNullOrEmpty(error))
                {
                    await DisplayAlert("Error", error, "OK", "Cancel");
                }
                else
                {
                    if (e.Result)
                    {
                        App.Current.MainPage = new UserPage();
                    }
                    else
                    {
                        await DisplayAlert("Try again", "Username or password invalid.", "Cancel");
                    }
                }
            });
        }
    }
}
