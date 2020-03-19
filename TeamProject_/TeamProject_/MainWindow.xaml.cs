using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TeamProject_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static TelegramBotClient client;
        static string path = "TeamTestBotDB.sqlite";
        public MainWindow()
        {
            if (!CheckExistDataBase(path))
                CreateDataBase(path);
            client = new TelegramBotClient("1149248725:AAG8ECl7OECLm7TOz6ob2yU1CFVks3LkroA");
            
            InitializeComponent();
        }

       
        private static bool CheckExistDataBase(string path) => File.Exists(path);
        private static void CreateDataBase(string path)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source = {path}"))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS QUESTION" +
                   "([ID] INTEGER PRIMARY KEY AUTOINCREMENT," +
                   "[QUESTION] VARCHAR(21) NOT NULL," , connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS ANSWER" +
                    "([ID] INTEGER PRIMARY KEY AUTOINCREMENT,[ANSWER] varchar(50) not null,[QUESTION_ID] integer," +
                    "FOREIGN KEY([QUESTION_ID]) REFERENCES QUESTION(ID));", connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS [USER]" +
                    "([ID] INTEGER PRIMARY KEY AUTOINCREMENT, USER_ID_TG integer);", connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS [MESSAGE]" +
                    "([ID] INTEGER PRIMARY KEY AUTOINCREMENT, USER_ID integer,MSG varchar(50), IS_BOT bit)" +
                    "FOREIGN KEY([USER_ID]) REFERENCES [USER](ID));", connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
