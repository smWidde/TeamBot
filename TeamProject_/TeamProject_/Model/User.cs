using System.Collections.Generic;
using System.Data.SQLite;
namespace TeamProject_
{
    class User
    {
        static readonly string path = "TeamTestBotDB.sqlite";
        public int ID { private get; set; }
        public int user_id { private get; set; }
        public static List<User> ReadAllUsers()
        {
            List<User> result = new List<User>();
            using (SQLiteConnection con = new SQLiteConnection($"Data Source={path}"))
            {
                using (SQLiteCommand com = new SQLiteCommand("SELECT * FROM User", con))
                {
                    using (SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new User()
                            {
                                ID = reader.GetInt32(0),
                                user_id = reader.GetInt32(1)
                            });
                        }
                    }
                }
                return result;
            }
        }
        public static User ReadByUserId(int User_ID_TG)
        {
            User user1 = null;
            using (SQLiteConnection con = new SQLiteConnection($"Data Source={path}"))
            {
                using (SQLiteCommand com = new SQLiteCommand("SELECT * FROM User WHERE USER_ID_TG=@user_id", con))
                {
                    com.Parameters.Add(new SQLiteParameter("@user_id", User_ID_TG));
                    using (SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user1 = new User()
                            {
                                ID = reader.GetInt32(0),
                                user_id = reader.GetInt32(1)
                            };
                        }
                    }
                }
            }
            return user1;
        }
        public static User ReadById(int ID)
        {
            User user1 = null;
            using (SQLiteConnection con = new SQLiteConnection($"Data Source={path}"))
            {
                using (SQLiteCommand com = new SQLiteCommand("SELECT * FROM User WHERE ID=@id", con))
                {
                    com.Parameters.Add(new SQLiteParameter("@id", ID));
                    using (SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user1 = new User()
                            {
                                ID = reader.GetInt32(0),
                                user_id = reader.GetInt32(1)
                            };
                        }
                    }
                }
            }
            return user1;
        }
        public static void AddUser(int User_ID_TG)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={path}"))
            {
                using (SQLiteCommand command = new SQLiteCommand("INSERT INTO USER(USER_ID_TG) VALUES(@user_id)", connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@user_id", User_ID_TG));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}