using System.Collections.Generic;

namespace WpfChat.ChatApp
{
    [System.Serializable]
    public struct ContactInfoData
    {
        public int              _ID;
        public string           _NickName;
        public UserStateType    _ContactStateType;

        [System.NonSerialized]
        public object _Image;
    }

    public class Contact
    {
        public ContactInfoData _ContactInfoData;

        private List<Message> _MessageList = new List<Message>();

        public Contact(ContactInfoData pContactInfo)
        {
            UpdateContactInfo(pContactInfo);
        }

        public void AddMessage(Message pMessage)
        {
            _MessageList.Add(pMessage);
        }

        public void AddMessages(List<Message> pMessages)
        {
            _MessageList = pMessages;
        }

        public void DeleteMessage(Message pMessage)
        {
            _MessageList.Remove(pMessage);
        }

        public List<Message> GetMessages()
        {
            return _MessageList;
        }

        public void UpdateContactInfo(ContactInfoData pContactInfo)
        {
            _ContactInfoData = pContactInfo;
        }
    }
}
