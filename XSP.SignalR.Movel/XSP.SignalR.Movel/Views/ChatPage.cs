using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using XSP.SignalR.Movel.Models;

namespace XSP.SignalR.Movel.Views
{
    public class ChatPage : ContentPage
    {
        // Controles do Xamarin.Forms
        ListView _lstMessage;
        Entry _txtMessage;
        Button _btnSend;
        
        string _userName;

        // Variáveis para utilização do SignalR
        IHubProxy _hub;
        string url = @"http://localhost:63853/";
        HubConnection connection;

        public ChatPage(string userName)
        {
            _userName = userName;

            connection = new HubConnection(url);
            _hub = connection.CreateHubProxy("ChatHub");

            var viewModel = new ChatModel();

            _hub.On<string, string>("broadcastMessage", (name, message) => viewModel.ListMessage.Add(new ChatMessage()
            {
                Date = DateTime.Now,
                Text = message,
                Name = name
            }));

            OnConnect();

            this.Content = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            _lstMessage = new ListView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            _txtMessage = new Entry
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            _btnSend = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Text = "Send"
            };

            var cell = new DataTemplate(typeof(TextCell));
            cell.SetBinding(TextCell.TextProperty, "Text");
            cell.SetBinding(TextCell.DetailProperty, "Name");

            _lstMessage.ItemTemplate = cell;

            _lstMessage.ItemsSource = viewModel.ListMessage;

            _btnSend.Clicked += OnSend;

            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(
                    left: 0,
                    right: 0,
                    bottom: 0,
                    top: Device.OnPlatform(iOS: 20, Android: 0, WinPhone: 0)),
                Children = {
                    _lstMessage,
                    _txtMessage,
                    _btnSend
                }
            };
        }

        private async void OnConnect()
        {
            await connection.Start();
        }

        private async void OnSend(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_txtMessage.Text))
            {
                await DisplayAlert("Alert", "Type a message!", "OK");
            }
            else
            {
                await _hub.Invoke("Send", _userName, _txtMessage.Text);

                _txtMessage.Text = string.Empty;
            }
        }
    }
}
