using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChat.ChatApp
{
    public class ContactController
    {
        private Chat _Chat;

        public ContactController(Chat pChat)
        {
            _Chat = pChat;

            TestContacts();
        }

        public void SearchContact(string pNickName)
        {

        }

        public void ChooseContact()
        {

        }

        public void AddContact(Contact pContact)
        {

        }

        public void DeleteContact(Contact pContact)
        {

        }

        public void BlockContact(Contact pContact)
        {

        }

        public void ReportContact(Contact pContact)
        {

        }

        public List<Contact> GetContacts()
        {
            return _Chat.User.GetContactList();
        }

        public List<Contact> GetContacts(string pSearchText)
        {
            List<Contact> userContactList = GetContacts();
            List<Contact> contacts = 
                userContactList.Where(x => x._ContactInfoData._NickName.Contains(pSearchText)).ToList();

            /*List<Contact> contacts = new List<Contact>();

            for (int i = 0; i < userContactList.Count; i++)
            {
                Contact contact = userContactList[i];
                if (contact._ContactInfoData._NickName.Contains(pSearchText))
                {
                    contacts.Add(contact);
                }
            }*/

            return contacts;
        }

        private void TestContacts()
        {
            List<Contact> contacts = new List<Contact>();
            for (int i = 0; i < 15; i++)
            {
                ContactInfoData data = new ContactInfoData();
                data._NickName = "Ahoj" + i;
                data._ID = i;
                Contact contact = new Contact(data);
                contact.AddMessages(GetTestMessages());
                contacts.Add(contact);
            }

            _Chat.User.AddContacts(contacts);
        }

        private const string testText1 = "sdgfagaegrqearbgerghghethqegehrthjhthh";
        private const string testText2 = "poiuertyiojoierjoeij";

        private List<Message> GetTestMessages()
        {
            List<Message> messages = new List<Message>();

            Random rnd = new Random();
            int count = rnd.Next(2, 15);
            for (int i = 0; i < count; i++)
            {
                MessageInfoData data = new MessageInfoData()
                {
                    _Content = i % 2 == 0 ? testText1 : testText2,
                    _Date = DateTime.Now.AddSeconds(-i),
                    _ID = (ulong)i,
                    _MessageDirectionType = i % 3 == 0 ?    MessageDirectionType.Income :
                                                            MessageDirectionType.Outcome,
                    _MessageStateType = MessageStateType.Received,
                    _MessageType = MessageType.Text,
                };

                Message message = new Message(data);
                messages.Add(message);
            }

            return messages;
        }
    }
}
