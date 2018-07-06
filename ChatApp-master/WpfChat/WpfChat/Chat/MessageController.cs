using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChat.ChatApp
{
    public class MessageController
    {
        private Chat _Chat;

        public MessageController(Chat pChat)
        {
            _Chat = pChat;
        }

        public List<Message> GetContactMessages(int pID)
        {
            List<Message> messages = _Chat.User.GetContactDict()[pID].GetMessages();

            return messages;
        }

        public void SendMessage(int pContactID, Message pMessage)
        {
            _Chat.User.GetContactDict()[pContactID].AddMessage(pMessage);
        }

        public void ReceiveMessage(Message pMessage)
        {
            
        }

        public void DeleteMessage(Message pMessage)
        {

        }
    }
}
