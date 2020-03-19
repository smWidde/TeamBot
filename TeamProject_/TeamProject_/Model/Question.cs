using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telegram.Bot;

namespace TeamProject_.Model
{
    class Question
    {
        private static string path = "TeamTestBotDB.sqlite";
        public int ID { get; private set; }
        public string QUESTION { get; private set; }
        public static List<Question> Read_All_Questions()
        {
            List<Question> list = new List<Question>();
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source = {path}"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("select*from QUESTION;", connection))
                {
                    try
                    {
                        SQLiteDataReader reader = command.ExecuteReader();
                        while(reader.Read())
                        {
                            list.Add(new Question(){ID=reader.GetInt32(0), QUESTION=reader.GetString(1)});
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    return list;
                }
            }
        }
        public static Question Read_ID(int ID)
        {
            Question question = null;
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source = {path}"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("select*from table QUESTION where ID=@id;", connection))
                {
                    try
                    {
                        command.Parameters.Add(new SQLiteParameter("@id", ID));
                        SQLiteDataReader reader = command.ExecuteReader();
                        reader.Read();
                        question.ID = reader.GetInt32(0);
                        question.QUESTION = reader.GetString(1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    return question;
                }
            }
        }
        public static Question Read_Question(string question)
        {
            Question quest = null;
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source = {path}"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("select*from table QUESTION where QUESTION=@question;", connection))
                {
                    try
                    {
                        command.Parameters.Add(new SQLiteParameter("@question", question));
                        SQLiteDataReader reader = command.ExecuteReader();
                        reader.Read();
                        quest.ID = reader.GetInt32(0);
                        quest.QUESTION = reader.GetString(1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    return quest;
                }
            }

        }
        public static Question Add_Question(string question)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={path}"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("INSERT INTO QUESTION(QUESTION) VALUES(@que)", connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@que", question));
                    command.ExecuteNonQuery();
                }
                return Read_Question(question);
            }
        }
    }
}
