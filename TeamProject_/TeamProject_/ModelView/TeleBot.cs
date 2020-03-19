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
            client.OnMessage += Answer;
        }

        public void SendToEveryone(string message)
        {
            foreach (var item in new UserCollection())
            {
                client.SendTextMessageAsync(item.user_id, message);
            }
        }
        public List<List<Message>> GetMsgsOfAllUsers()
        {
            List<List<Message>> result = new List<List<Message>>();

            foreach (var user in new UserCollection().Distinct())
            {
                
                result.Add(GetGetMsgsOfOneUser((int)user.user_id));
            }

            return result;


        }
        public List<Message> GetGetMsgsOfOneUser(int User_ID_TG)
        {
            List<Message> tmp = new List<Message>();
            foreach (var msg in new MessageCollection().Where(ms => ms.USER_ID == User_ID_TG))
            {
                tmp.Add(msg);
            }
            return tmp;
        }

        public LinguisticComponent GetOneLinguisticComponent(string question)
        {
            LinguisticComponent tmp = new LinguisticComponent();
            tmp.question = new QuestionCollection().Where(qs=>qs.QUESTION==question).FirstOrDefault();
            foreach (var ans in new AnswerCollection().Where(ans=>ans.QUESTION_ID==tmp.question.ID))
            {
                tmp.answers.Add(ans);
            }
            return tmp;
        }

        public List<LinguisticComponent> GetAllLinguisticComponents()
        {
            List<LinguisticComponent> res = new List<LinguisticComponent>();
            foreach (var qs in new QuestionCollection())
            {
                res.Add(GetOneLinguisticComponent(qs.QUESTION));
            }
            return res;
        }

        private void Answer(object sender, MessageEventArgs e)
        {
            if(e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                if(e.Message.ReplyToMessage==null)
                {
                    if (new QuestionCollection().Where(qs => qs.QUESTION == e.Message.Text).ToList().Count > 0)
                    {
                        if(new AnswerCollection().Where(ans=>ans.QUESTION_ID== new QuestionCollection().Where(qs => qs.QUESTION == e.Message.Text).FirstOrDefault().ID).ToList().Count>0)
                        {
                            int count = new AnswerCollection().Where(ans => ans.QUESTION_ID == new QuestionCollection().Where(qs => qs.QUESTION == e.Message.Text).FirstOrDefault().ID).ToList().Count;
                            client.SendTextMessageAsync(e.Message.Chat.Id, new AnswerCollection().Where(ans=>ans.QUESTION_ID== new QuestionCollection().Where(qs => qs.QUESTION == e.Message.Text).FirstOrDefault().ID).ToList()[new Random().Next(0,count)].ANSWER);
                        }
                        else
                        {
                            client.SendTextMessageAsync(e.Message.Chat.Id, "Ответа на этот вопрос нет!");
                        }
                    }
                    else
                    {
                        client.SendTextMessageAsync(e.Message.Chat.Id, "Вопрос добавлен!");
                        

                    }
                }
                else
                {
                    Upgrade(e.Message.Text, e.Message.ReplyToMessage.Text);
                }
            }
        }

        public void Upgrade(string question)
        {
            new QuestionCollection().AddNewQuestion(TransformString(question));
        }

        public void Upgrade(string answer, string question)
        {
            new AnswerCollection().AddNewQuestion(TransformString(question), answer);
        }

        public string TransformString(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if(!Char.IsLetterOrDigit(str[i])||!Char.IsWhiteSpace(str[i]))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }

        private void StoreMessage(object sender, MessageEventArgs e)
        {
            if(e.Message.Type== Telegram.Bot.Types.Enums.MessageType.Text)
            {
                if (User.ReadByUserId(e.Message.Chat.Id) == null)
                {
                    int id = new UserCollection().OrderByDescending(us => us.ID).FirstOrDefault().ID + 1;
                    new UserCollection().Add(new User() { user_id = e.Message.Chat.Id, ID = id });
                }
            }
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
