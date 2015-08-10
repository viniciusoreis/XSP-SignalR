using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XSP.SignalR.Movel.Models
{
    public class ChatModel : INotifyPropertyChanged
    {
        public ChatModel()
        {
            ListMessage = new ObservableCollection<ChatMessage>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        ObservableCollection<ChatMessage> _listMessage;
        public ObservableCollection<ChatMessage> ListMessage
        {
            get { return _listMessage; }
            set
            {
                _listMessage = value;
                OnPropertyChanged();
            }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
