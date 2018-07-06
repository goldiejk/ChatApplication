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
using WpfChat.WPFDataClasses;

namespace WpfChat.User_Controls
{
    /// <summary>
    /// Interaction logic for UsersContact.xaml
    /// </summary>
    public partial class UsersContact : UserControl
    {
        public Action<int> OnContactClicked;
        public WPFContact _WPFContact;

        public UsersContact()
        {
            InitializeComponent();
            _WPFContact  = new WPFContact();
            DataContext = _WPFContact;
        }

        public void UpdateContact(WpfChat.ChatApp.Contact pContact)
        {
            NickName.Text = pContact._ContactInfoData._NickName;

            _WPFContact.SetContactData(pContact._ContactInfoData);
        }

        private void Contact_Click(object sender, RoutedEventArgs e)
        {
            OnContactClicked?.Invoke(_WPFContact.ContactInfoData._ID);

            Console.WriteLine(_WPFContact.ContactInfoData._ID.ToString());

            /*if (OnContactClicked != null)
            {
                OnContactClicked(0);
            }*/
        }
    }
}
