using Microsoft.Win32;
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
using TeamProject_.Model;
using TeamProject_.ModelView;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TeamProject_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static TeleBot client;
        static string path = "TeamTestBotDB.sqlite";
        Method method = new Method("848578183:AAEyE9rbGZtyq4eunSdruS91Jj-gHn2F9Oc");
        UserCollection coll = new UserCollection();
        public MainWindow()
        {
            if (!CheckExistDataBase(path))
                CreateDataBase(path);
          
            client = new TeleBot("848578183:AAEyE9rbGZtyq4eunSdruS91Jj-gHn2F9Oc");
            InitializeComponent();
            btnsend_Doc.IsEnabled = false;
            btnsend_TXT.IsEnabled = false;
            btnsend_Video.IsEnabled = false;
            btnsend_PHOTO.IsEnabled = false;
            btnsend_Music.IsEnabled = false;
            btnsend_Mass.IsEnabled = false;
            btnsend_Next.IsEnabled = false;

        }

       
        private static bool CheckExistDataBase(string path) => File.Exists(path);
        private static void CreateDataBase(string path)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source = {path}"))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS QUESTION" +
                   "([ID] INTEGER PRIMARY KEY AUTOINCREMENT," +
                   "[QUESTION] VARCHAR(21) NOT NULL)" , connection))
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
                    "([ID] INTEGER PRIMARY KEY AUTOINCREMENT, USER_ID integer,MSG varchar(50), IS_BOT bit," +
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow taskWindow = new UsersWindow(client);
            taskWindow.Show();
            this.Close();
        }

        private void SendAll(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("В этой программе можно отпровлять сообщения", "Информация!!!");
            MessageBox.Show("В главном меню присутствуют 6 кнопок для отправки различных сообщений через телеграм бота", "Информация!!!");
            MessageBox.Show("Для отправки файла, видео итд выберете нужную кнопку", "Информация!!!");
            MessageBox.Show("Что бы отправть сообщение нужно ввести символы в текст бокс", "Информация!!!");
            MessageBox.Show("То что вы выберете отправится всем пользователям использующим телеграм бота", "Информация!!!");
            MessageBox.Show("Если вы хотите отправить сообщение отдельному пользователю нажмите на кнопку Перейти к пользователям", "Информация!!!");
            MessageBox.Show("Выберете кому отправлять и дерзайте", "Информация!!!");
            MessageBox.Show("Удачного пользования!", "Информация!!!");
            btnsend_Doc.IsEnabled = true;
            btnsend_TXT.IsEnabled = true;
            btnsend_Video.IsEnabled = true;
            btnsend_PHOTO.IsEnabled = true;
            btnsend_Music.IsEnabled = true;
            btnsend_Mass.IsEnabled = true;
            btnsend_Next.IsEnabled = true;
        }

        private void btnsend_Click(object sender, RoutedEventArgs e)
        {
            client.SendToEveryone(msg.Text);
        }

        private void btnsend_ClickTXT(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openfiledialog.ShowDialog().Value == true)
            {
                foreach (var item in coll)
                {
                   method.SendDocumentIputFile(Convert.ToInt32(item.user_id), openfiledialog.FileName);
                }
               
            }
        }

        private void btnsend_ClickPhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (openfiledialog.ShowDialog().Value == true)
            {
                foreach (var item in coll)
                {
                    method.SendPhotoIputFile(Convert.ToInt32(item.user_id), openfiledialog.FileName);
                }
            }
        }

        private void btnsend_ClickVideo(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "All Media Files|*.wav;*.aac;*.wma;*.wmv;*.avi;*.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;*.mpa;*.mpe;*.m3u;" +
                 "*.mp4;*.mov;*.3g2;*.3gp2;*.3gp;*.3gpp;*.m4a;*.cda;*.aif;*.aifc;*.aiff;*.mid;*.midi;*.rmi;*.mkv;*.WAV;*.AAC;*.WMA;*.WMV;*.AVI;" +
                 "*.MPG;*.MPEG;*.M1V;*.MP2;*.MP3;*.MPA;*.MPE;*.M3U;*.MP4;*.MOV;*.3G2;*.3GP2;*.3GP;*.3GPP;*.M4A;*.CDA;*.AIF;*.AIFC;*.AIFF;*.MID;" +
                 "*.MIDI;*.RMI;*.MKV";
            if (openfiledialog.ShowDialog().Value == true)
            {
                foreach (var item in coll)
                {
                    method.SendVideoInputFile(Convert.ToInt32(item.user_id), openfiledialog.FileName);
                }
            }
        }

        private void btnsend_ClickMusic(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "All Supported Audio | *.mp3; *.wma | MP3s | *.mp3 | WMAs | *.wma";
            if (openfiledialog.ShowDialog().Value == true)
            {
                foreach (var item in coll)
                {
                    method.SendAudioIputFile(Convert.ToInt32(item.user_id), openfiledialog.FileName);
                }
            }

        }

        private void btnsend_ClickDoc(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "Pdf Files|*.pdf";
            if (openfiledialog.ShowDialog().Value == true)
            {
                foreach (var item in coll)
                {
                    method.SendDocumentIputFile(Convert.ToInt32(item.user_id), openfiledialog.FileName);
                }
            }

        }
    }
}
