using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_.Model;

namespace TeamProject_.ModelView
{
    public class MessageCollection : List<Message>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public MessageCollection()
        {
            foreach (Message item in Message.GetMessages())
            {
                Add(item);
            }
        }
        public void AddNewMessage(int user_id, string message, bool isbot)
        {
            Message newOne = Message.AddMessage(user_id,message,isbot);
            Add(newOne);
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newOne));
        }
    }
}
