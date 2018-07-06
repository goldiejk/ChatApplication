using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WpfChat.ChatApp;

namespace WpfChat.WPFDataClasses
{
    public class WPFContact : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ContactInfoData _Data;

        public ContactInfoData ContactInfoData => _Data;

        public string Name
        {
            get
            {
                return _Data._NickName;
            }
        }

        public UserStateType ContactStateType
        {
            get
            {
                return _Data._ContactStateType;
            }
        }

        public void SetContactData(ContactInfoData pData)
        {
            _Data = pData;

            OnPropertyChanged("Name");
            //OnPropertyChanged("ContactStateType");
        }

        private void OnPropertyChanged(string pParam)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(pParam));
            }
        }
    }
}
