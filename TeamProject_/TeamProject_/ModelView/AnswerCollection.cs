using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_.Model;
namespace TeamProject_.ModelView
{
    class AnswerCollection : List<Answer>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public AnswerCollection()
        {
            foreach (Answer item in Answer.GetAnswers())
            {
                Add(item);
            }
        }
        public void AddNewAnswer(string question, string answer)
        {
            int question_id = Question.Read_Question(question).ID;
            Answer newOne = Answer.AddAnswer(question_id, answer);
            Add(newOne);
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newOne));
        }
    }
}
