using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChat.ChatApp
{
    public class Chat
    {
        //Example of custom delegate, it is almost same as Action type
        //public delegate void OnUserDelegate(User pNewUser, User pOldUser);
        //public OnUserDelegate OnUserDelegateChanged;

        public Action<User> OnUserLogin;
        public Action       OnUserLogout;
        public Action       OnUserRegister;

        private User                _User = null;
        private MessageController   _MessageController = null;
        private ContactController   _ContactController = null;

        //Shorter syntax of property with only GET section as ExampleUser
        public User User => _User;
        /*public User ExmapleUser
        {
            get { return _User; }
        }*/

        public Chat()
        {
            _User = new User();
            _MessageController = new MessageController(this);
            _ContactController = new ContactController(this);
        }

        public void Register()
        {
            OnUserRegister?.Invoke();
        }

        public void Login()
        {
            _User = new User();

            if (OnUserLogin != null)
            {
                OnUserLogin(_User);
            }

            //OnUserLogin?.Invoke(_User);
        }

        public void Logout()
        {
            _User = null;

            OnUserLogout?.Invoke();
        }

        public User GetUser()
        {
            return _User;
        }

        #region MessageController methods

        public void SendMessage(int pContactID, Message pMessage)
        {
            _MessageController.SendMessage(pContactID, pMessage);
        }

        public void ReceiveMessage(Message pMessage)
        {
            _MessageController.ReceiveMessage(pMessage);
        }

        public void DeleteMessage(Message pMessage)
        {
            _MessageController.DeleteMessage(pMessage);
        }

        public List<Message> GetContactMessages(int pID)
        {
            return _MessageController.GetContactMessages(pID);
        }

        #endregion

        #region Contact controller

        public List<Contact> GetContacts()
        {
            return _ContactController.GetContacts();
        }

        public List<Contact> GetContacts(string pSearchText)
        {
            return _ContactController.GetContacts(pSearchText);
        }

        #endregion
    }
}
