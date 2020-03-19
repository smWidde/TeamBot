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
using System.Windows.Shapes;
using TeamProject_.Model;
using TeamProject_.ModelView;

namespace TeamProject_
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        UserCollection coll = new UserCollection();
        List<User> users = new List<User>();
        TeleBot client;
        public UsersWindow(TeleBot client)
        {
            this.client = client;
            InitializeComponent();
            foreach (var item in coll)
            {
                lsbox.Items.Add(item.user_id);
              
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           if(lsbox.SelectedItems==null)
                return;
            foreach (var item in coll)
            {
                if (lsbox.SelectedItem.ToString() == item.user_id.ToString())
                {
                    string name = item.user_id.ToString();
                    UserWindow taskWindow = new UserWindow(name, client);
                    taskWindow.Show();
                    this.Close();
                }
            }
        }
    }
}
