using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChat.ChatApp
{
    [System.Serializable]
    public struct MessageInfoData
    {
        public ulong                _ID;
        public DateTime             _Date;
        public MessageStateType     _MessageStateType;
        public string               _Content;
        public MessageType          _MessageType;
        public MessageDirectionType _MessageDirectionType;
    }

    public class Message
    {
        public MessageInfoData _MessageInfoData;

        public Message(MessageInfoData pMessageInfoData)
        {
            _MessageInfoData = pMessageInfoData;
        }

        public void UpdateMessage(MessageInfoData pMessageInfoData)
        {
            _MessageInfoData = pMessageInfoData;
        }
    }
}
