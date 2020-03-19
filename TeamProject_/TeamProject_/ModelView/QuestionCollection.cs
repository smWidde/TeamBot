using System.Collections.Generic;
using System.Collections.Specialized;
using TeamProject_.Model;
namespace TeamProject_.ModelView
{
    class QuestionCollection:List<Question>,INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public QuestionCollection()
        {
            foreach(Question item in Question.Read_All_Questions())
            {
                Add(item);
            }
        }
        public void AddNewQuestion(string question)
        {
            Question newOne = Question.Add_Question(question);
            Add(newOne);
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newOne));
        }
    }
}
