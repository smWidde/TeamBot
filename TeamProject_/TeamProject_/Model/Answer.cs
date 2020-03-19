﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_.Model
{
    public class Answer
     {
        static readonly string path = "TeamTestBotDB.sqlite";
        
        
        public  int ID { get; set; }

        public  string ANSWER { get; set; }

        public  int QUESTION_ID { get; set; }

        public static List<Answer> GetAnswers()
        {
            List<Answer> result = new List<Answer>();
            using(SQLiteConnection connection = new  SQLiteConnection($"Data Source={path}"))
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM ANSWER",connection))
                {
                    using(SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new Answer() { ID = reader.GetInt32(0), ANSWER = reader.GetString(1), QUESTION_ID = reader.GetInt32(2) });
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static Answer GetAnswersByID(int ID_Question)
        {
            Answer result = null;
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={path}"))
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM ANSWER WHERE ID = @id", connection))
                {
                    cmd.Parameters.Add(new SQLiteParameter("@id", ID_Question));
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result=new Answer() { ID = reader.GetInt32(0), ANSWER = reader.GetString(1), QUESTION_ID = reader.GetInt32(2) };
                            }
                        }
                       
                    }
                }
            }
            return result;
        }

        public static Answer AddAnswer(int ID_Question,string answer)
        {
            Answer res = new Answer();
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={path}"))
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO ANSWER(QUESTION_ID,ANSWER) VALUES(@qid,@ans)", connection))
                {
                    cmd.Parameters.Add(new SQLiteParameter("@qid", ID_Question));
                    cmd.Parameters.Add(new SQLiteParameter("@ans", answer));
                    cmd.ExecuteNonQuery();

                    res = Answer.GetAnswers().OrderByDescending(ans => ans.ID).FirstOrDefault();
                }
            }
            return res;
        }
     }

}
