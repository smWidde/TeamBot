using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_.Model;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TeamProject_.ModelView
{
    class TeleBot
    {
        TelegramBotClient client { get; set; }

        public TeleBot()
        {
            client = new TelegramBotClient("1149248725:AAG8ECl7OECLm7TOz6ob2yU1CFVks3LkroA");
            client.OnMessage += Receive;
        }

        private void Receive(object sender, MessageEventArgs e)
        {
            if(User.ReadByUserId(e.Message.Chat.Id)==null)
            {
                User.AddUser(e.Message.Chat.Id);
            }
            if (e.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
            {

            }
            else if (e.Message.ReplyToMessage != null)
            {

            }
            else
            {

            }
        }
    }
}
