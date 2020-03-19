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
using TeamProject_.ModelView;
using Telegram.Bot;

namespace TeamProject_
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        TeleBot bot;
        AnswerCollection answers = new AnswerCollection();
           QuestionCollection qc = new QuestionCollection();    
        public UserWindow(string name, TeleBot bot)
        {
            this.bot = bot;
            InitializeComponent();
            labletxt.Content = name;
        }

        private void btnsend_Click(object sender, RoutedEventArgs e)
        {
            bot.SendToOne(Int64.Parse(labletxt.Content.ToString()), msg.Text);
        }
    }
}
