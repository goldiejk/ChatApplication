using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfChat.ChatApp;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace WpfChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<User_Controls.UsersContact> contacts = new List<User_Controls.UsersContact>();
        private List<UserControls.OutcomingMessage> messages = new List<UserControls.OutcomingMessage>();

        private Chat chat;
        private int _CurrentContactID;

        private SqlConnection _Connection = null;
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\DEV\ChatApp\WpfChat\WpfChat\DBChat.mdf;Integrated Security=True";

        public MainWindow()
        {
            InitializeComponent();

            chat = new Chat();
            chat.OnUserLogin += OnUserLogin;

            RefreshData(chat.GetContacts());

            _Connection = new SqlConnection(ConnectionString);

            try
            {
                _Connection.Open();
            }
            catch
            {
                MessageBox.Show("Nepodarilo se pripojeni do DB!!!");
            }

            //JSONExample();
        }

        private void RefreshData(List<Contact> pContactList)
        {
            UsersContacts.Children.Clear();
            contacts.Clear();

            List<Contact> contactList = pContactList;

            for (int i = 0; i < contactList.Count; i++)
            {
                User_Controls.UsersContact control = new WpfChat.User_Controls.UsersContact();
                control.UpdateContact(contactList[i]);
                UsersContacts.Children.Add(control);
                contacts.Add(control);
                control.OnContactClicked += OnContactClicked;
            }
        }

        private void OnContactClicked(int pID)
        {
            _CurrentContactID = pID;
            RefreshData(pID);
        }

        private void RefreshData(int pID)
        {
            Messages.Children.Clear();
            messages.Clear();

            List<Message> messageList = chat.GetContactMessages(pID);

            for (int i = 0; i < messageList.Count; i++)
            {
                UserControls.OutcomingMessage control = new WpfChat.UserControls.OutcomingMessage();
                control.SetMessage(messageList[i]._MessageInfoData._Content);
                Messages.Children.Add(control);
                messages.Add(control);
            }
        }

        private void OnUserLogin(User pUser)
        {
            
        }

        private void JSONExample()
        {
            UserInfoData data = new UserInfoData()
            {
                _ID = 0,
                _Email = "ahoj",
            };

            //Tento radek prevede UserInfoData na typ string
            string s = JsonConvert.SerializeObject(data);

            User user = chat.GetUser();
            string suser = JsonConvert.SerializeObject(user);

            Console.WriteLine(s);

            //Tento radek prevede string na typ UserInfoData
            UserInfoData newUserData = JsonConvert.DeserializeObject<UserInfoData>(s);

            Console.WriteLine(newUserData._ID.ToString());
            Console.WriteLine(newUserData._Email.ToString());
        }

        private void SendMSGTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && SendMSGTextBox.Text != "")
            {
                string s = SendMSGTextBox.Text;
                SendMSGTextBox.Text = "";

                MessageInfoData data = new MessageInfoData()
                {
                    _Content = s,
                    _Date = DateTime.Now,
                    _ID = 0,
                    _MessageDirectionType = MessageDirectionType.Outcome,
                    _MessageStateType = MessageStateType.Send,
                    _MessageType = MessageType.Text
                };

                Message message = new Message(data);
                chat.SendMessage(_CurrentContactID, message);

                RefreshData(_CurrentContactID);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (chat == null) return;

            TextBox text = (TextBox)sender;
            string s = text.Text;

            RefreshData(chat.GetContacts(s));
        }
    }
}
