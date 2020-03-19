using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_.Model;
namespace TeamProject_.ModelView
{
    class UserCollection : List<User>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public UserCollection()
        {
            foreach (User item in User.ReadAllUsers())
            {
                Add(item);
            }
        }
        public void AddNewQuestion(long User_ID_TG)
        {
            User newOne = User.AddUser(User_ID_TG);
            Add(newOne);
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newOne));
        }
    }
}
