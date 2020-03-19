using System.Collections.Generic;
using System.Data.SQLite;
namespace TeamProject_.Model
{
    class User
    {
        static readonly string path = "TeamTestBotDB.sqlite";
        public int ID { private get; set; }
        public long user_id { private get; set; }
        public static List<User> ReadAllUsers()
        {
            List<User> result = new List<User>();
            using (SQLiteConnection con = new SQLiteConnection($"Data Source={path}"))
            {
                con.Open();
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
        public static User ReadByUserId(long User_ID_TG)
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
                con.Open();
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
        public static User AddUser(long User_ID_TG)
        {
            using (SQLiteConnection con = new SQLiteConnection($"Data Source={path}"))
            {
                con.Open();
                using (SQLiteCommand com = new SQLiteCommand("INSERT INTO USER(USER_ID_TG) VALUES(@user_id)", con))
                {
                    com.Parameters.Add(new SQLiteParameter("@user_id", User_ID_TG));
                    com.ExecuteNonQuery();
                }
            }
            return User.ReadByUserId(User_ID_TG);
        }
    }
}