using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChat.ChatApp
{
    public enum MessageStateType
    {
        Received = 0, Send = 1, Seen = 2,
    }

    public enum MessageType
    {
        Text = 0, Video = 1, Audio = 2, Image = 3,
    }

    public enum MessageDirectionType
    {
        System = 0, Income = 1, Outcome = 2,
    }

    public enum UserStateType
    {
        Online = 0, Offline = 1, Busy = 2, AFK = 3,
    }
}
