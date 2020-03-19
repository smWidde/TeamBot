using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_.Model
{
    public class Message
    {
        static readonly string path = "TeamTestBotDB.sqlite";

        public int ID { get; set; }
        public int USER_ID { get; set; }
        public string MSG { get; set; }
        public bool IS_BOT { get; set; } 

        public static List<Message> GetMessages()
        {
            List<Message> result = new List<Message>();
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={path}"))
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM MESSAGE", connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new Message() { ID = reader.GetInt32(0), USER_ID = reader.GetInt32(1), MSG = reader.GetString(2), IS_BOT = reader.GetBoolean(3) });
                            }
                        }
                    }
                }
            }
            return result;
        }
        public static Message GetMessagesByID(int ID)
        {
            Message result = null;
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={path}"))
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM MESSAGE WHERE ID = @id", connection))
                {
                    cmd.Parameters.Add(new SQLiteParameter("@id", ID));
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result = new Message() { ID = reader.GetInt32(0), USER_ID = reader.GetInt32(1), MSG = reader.GetString(2), IS_BOT = reader.GetBoolean(3) };
                            }
                        }
                       
                    }
                }
            }
            return result;
        }
        public static List<Message> GetMessagesByUserID(int ID)
        {
            List<Message> result = new List<Message>();
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={path}"))
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM MESSAGE WHERE USER_ID = @id", connection))
                {
                    cmd.Parameters.Add(new SQLiteParameter("@id", ID));
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new Message() { ID = reader.GetInt32(0), USER_ID = reader.GetInt32(1), MSG = reader.GetString(2), IS_BOT = reader.GetBoolean(3) });
                            }
                        }

                    }
                }
            }
            return result;
        }
        public static List<Message> GetMessagesByUserID(Message message)
        {
            List<Message> result = new List<Message>();
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={path}"))
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM MESSAGE WHERE MSG = @msg", connection))
                {
                  
                    cmd.Parameters.Add(new SQLiteParameter("@msg", message.MSG));
                    
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new Message() { ID = reader.GetInt32(0), USER_ID = reader.GetInt32(1), MSG = reader.GetString(2), IS_BOT = reader.GetBoolean(3) });
                            }
                        }

                    }
                }
            }
            return result;
        }

        public static Message AddMessage(int user_id, string message,bool isbot)
        {
            Message res = new Message();
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={path}"))
            {
                
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO MESSAGE(USER_ID,MSG,IS_BOT) VALUES(@uid,@msg,@isb)", connection))
                {
                    cmd.Parameters.Add(new SQLiteParameter("@uid", user_id));
                    cmd.Parameters.Add(new SQLiteParameter("@msg", message));
                    cmd.Parameters.Add(new SQLiteParameter("@isb", isbot));
                    cmd.ExecuteNonQuery();

                    res = Message.GetMessages().OrderByDescending(msg => msg.ID).FirstOrDefault();
                }
            }
            return res;
        }
    }
}
