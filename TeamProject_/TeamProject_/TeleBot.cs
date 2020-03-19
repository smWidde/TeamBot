using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TeamProject_
{
    class TeleBot
    {
        static readonly string path = "TeamTestBotDB.sqlite";

        public TelegramBotClient client;

        public TeleBot(string apikey)
        {
            client = new TelegramBotClient(apikey);
            client.OnMessage += TeleReceiver;
        }

        private void TeleReceiver(object sender, MessageEventArgs e)
        {
            if(e.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
            {

            }
            else if(e.Message.ReplyToMessage != null)
            {

            }
            else
            {
                using(SQLiteConnection connection = new SQLiteConnection($"Data Source={path}"))
                {
                    using(SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM QUESTION WHERE QUESTION = @qs",connection))
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@qs", e.Message.Text));

                        if((int)cmd.ExecuteScalar() > 0)
                        {
                            Answer.GetAnswers().Where(que=>que.QUESTION_ID==)
                        }
                        else
                        {
                            client.SendTextMessageAsync(e.Message.Chat.Id, "Такого вопроса нет,извини!");
                            cmd.CommandText = "INSERT INTO QUESTION(QUESTION) VALUES(@tadd)";
                            cmd.Parameters.Add(new SQLiteParameter("@tadd", e.Message.Text.Replace(".","").Replace(",", "").Replace(";", "").Replace(":", "").ToLower()));
                            cmd.ExecuteNonQuery();
                            
                        }
                    }
                }
            }
        }
    }
}
