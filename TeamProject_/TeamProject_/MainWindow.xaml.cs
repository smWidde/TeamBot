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

                using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS ChatTable" +
                    "([id] INTEGER PRIMARY KEY AUTOINCREMENT,", connection))
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

                using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS UserTable" +
                    "([id] INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "[Name] VARCHAR(21) NOT NULL," +
                    "[LastName] VARCHAR(21) NOT NULL," +
                    "[NumberPhone] integer NOT NULL," +
                    "[ID_ChatTable] INTEGER," +
                    "FOREIGN KEY(ID_ChatTable) REFERENCES ChatTable(id));", connection))
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
