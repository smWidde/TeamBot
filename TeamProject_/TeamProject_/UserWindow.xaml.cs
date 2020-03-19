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
        static TelegramBotClient client;
        AnswerCollection answers = new AnswerCollection();
           QuestionCollection qc = new QuestionCollection();    
        public UserWindow(string name)
        {
           
            InitializeComponent();
            labletxt.Content = name;
            foreach (var item in qc)
            {
                msgchat.Text +="\n"+ item.QUESTION;
            }
            foreach (var item in answers)
            {
                msgchat.Text += "\n" + item.ANSWER;
            }

        }

        private void btnsend_Click(object sender, RoutedEventArgs e)
        {
            //client.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(name), msgchat.Text);
            msgchat.Text = String.Empty;
        }
    }
}
