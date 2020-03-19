﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_.Model;
namespace TeamProject_.ModelView
{
    public class UserCollection : List<User>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public UserCollection()
        {
            foreach (User item in User.ReadAllUsers())
            {
                Add(item);
            }
        }
        public void AddNewUser(long User_ID_TG)
        {
            User newOne = User.AddUser(User_ID_TG);
            Add(newOne);
            if(CollectionChanged!=null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newOne));
            }
        }
    }
}
