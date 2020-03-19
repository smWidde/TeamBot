using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        public UserCollection users;
        private MessageCollection messages;
        private AnswerCollection answers;
        private QuestionCollection questions;

        public List<LinguisticComponent> Q_A;
        public int CurrentUser;
        public List<Message> CurrentChat;
        public TeleBot(string Token)
        {
            client = new TelegramBotClient(Token);
            client.OnMessage += StoreMessage;
            client.OnMessage += Answer;
            users = new UserCollection();
            messages = new MessageCollection();
            answers = new AnswerCollection();
            questions = new QuestionCollection();
            Q_A = GetAllLinguisticComponents();
            CurrentUser = -1;
            questions.CollectionChanged += LinguisticQuestAdder;
            answers.CollectionChanged += LinguisticAnswAdder;
            messages.CollectionChanged += CurrentChatAdder;
            client.StartReceiving();
        }
        private void CurrentChatAdder(object sender, NotifyCollectionChangedEventArgs e)
        {
            Message msg = (Message)e.NewItems[0];
            if(msg.USER_ID==CurrentUser)
            {
                CurrentChat = GetGetMsgsOfOneUser(User.ReadById(CurrentUser).user_id);
            }
        }
        private void LinguisticAnswAdder(object sender, NotifyCollectionChangedEventArgs e)
        {
            Answer answ = (Answer)e.NewItems[0];
            Question quest = Question.Read_ID(answ.QUESTION_ID);
            Q_A.Where(qs => qs.question.ID == quest.ID).FirstOrDefault().answers.Add(answ);
        }

        private void LinguisticQuestAdder(object sender, NotifyCollectionChangedEventArgs e)
        {
            Question quest = (Question)e.NewItems[0];
            Q_A.Add(new LinguisticComponent() { question = quest, answers = new List<Answer>() });
        }
        public void SendToEveryone(string message)
        {
            foreach (var item in users)
            {
                client.SendTextMessageAsync(item.user_id, message);
            }
        }
        private List<List<Message>> GetMsgsOfAllUsers()
        {
            List<List<Message>> result = new List<List<Message>>();
            foreach (var user in users.Distinct())
            {
                result.Add(GetGetMsgsOfOneUser((int)user.user_id));
            }
            return result;
        }
        private List<Message> GetGetMsgsOfOneUser(long User_ID_TG)
        {
            List<Message> tmp = new List<Message>();
            foreach (var msg in messages.Where(ms => ms.USER_ID == User_ID_TG))
            {
                tmp.Add(msg);
            }
            return tmp;
        }
        private LinguisticComponent GetOneLinguisticComponent(string question)
        {
            LinguisticComponent tmp = new LinguisticComponent();
            tmp.question = questions.Where(qs=>qs.QUESTION==question).FirstOrDefault();
            foreach (var ans in answers.Where(ans=>ans.QUESTION_ID==tmp.question.ID))
            {
                tmp.answers.Add(ans);
            }
            return tmp;
        }
        private List<LinguisticComponent> GetAllLinguisticComponents()
        {
            List<LinguisticComponent> res = new List<LinguisticComponent>();
            foreach (var qs in questions)
            {
                res.Add(GetOneLinguisticComponent(qs.QUESTION));
            }
            return res;
        }

        private void Answer(object sender, MessageEventArgs e)
        {
            int user_id = -1;
            if(e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                if(e.Message.ReplyToMessage==null)
                {
                    if (questions.Where(qs => qs.QUESTION == e.Message.Text).ToList().Count > 0)
                    {
                        if(answers.Where(ans=>ans.QUESTION_ID== questions.Where(qs => qs.QUESTION == e.Message.Text).FirstOrDefault().ID).ToList().Count>0)
                        {
                            int count = answers.Where(ans => ans.QUESTION_ID == questions.Where(qs => qs.QUESTION == e.Message.Text).FirstOrDefault().ID).ToList().Count;
                            string answ = answers.Where(ans => ans.QUESTION_ID == questions.Where(qs => qs.QUESTION == e.Message.Text).FirstOrDefault().ID).ToList()[new Random().Next(0, count)].ANSWER;
                            client.SendTextMessageAsync(e.Message.Chat.Id, answ);
                            user_id = User.ReadByUserId(e.Message.Chat.Id).ID;
                            messages.AddNewMessage(user_id, answ, true);
                        }
                        else
                        {
                            client.SendTextMessageAsync(e.Message.Chat.Id, "Ответа на этот вопрос нет!");
                            user_id = User.ReadByUserId(e.Message.Chat.Id).ID;
                            messages.AddNewMessage(user_id, "Ответа на этот вопрос нет!", true);
                        }
                    }
                    else
                    {
                        Upgrade(e.Message.Text);
                        client.SendTextMessageAsync(e.Message.Chat.Id, "Вопрос добавлен!");
                        user_id = User.ReadByUserId(e.Message.Chat.Id).ID;
                        messages.AddNewMessage(user_id, "Вопрос добавлен!", true);
                    }
                }
                else
                {
                    Upgrade(e.Message.Text, e.Message.ReplyToMessage.Text);
                }
            }
        }

        private void Upgrade(string question)
        {
            questions.AddNewQuestion(TransformString(question));
        }

        private void Upgrade(string answer, string question)
        {
            string quest = TransformString(question);
            if (Question.Read_Question(quest) == null)
                Upgrade(quest);
            answers.AddNewAnswer(quest, answer);
        }

        private string TransformString(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if(!Char.IsLetterOrDigit(str[i]))
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
                int user_id = -1;
                if (User.ReadByUserId(e.Message.Chat.Id) == null)
                {
                    users.AddNewUser(e.Message.Chat.Id);
                }
                user_id = User.ReadByUserId(e.Message.Chat.Id).ID;
                messages.AddNewMessage(user_id, e.Message.Text, false);
            }
        }

    }
}
