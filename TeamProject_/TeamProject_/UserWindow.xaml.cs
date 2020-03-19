using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TeamProject_.Model;
using TeamProject_.ModelView;
using Telegram.Bot;

namespace TeamProject_
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        Method method = new Method("848578183:AAEyE9rbGZtyq4eunSdruS91Jj-gHn2F9Oc");
       
        TeleBot bot;
        AnswerCollection answers = new AnswerCollection();
           QuestionCollection qc = new QuestionCollection();    
        public UserWindow(string name, TeleBot bot)
        {
            this.bot = bot;
            InitializeComponent();
            //string smth = ConfigurationSettings.AppSettings.Get("TeleBotClientKey");
            //client = new TelegramBotClient("848578183:AAEyE9rbGZtyq4eunSdruS91Jj-gHn2F9Oc");
            labletxt.Content = name;
        }

        private void btnsend_Click(object sender, RoutedEventArgs e)
        {
          bot.SendToOne(Int64.Parse(labletxt.Content.ToString()), msg.Text);
        }

        private void btnsend_ClickTXT(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openfiledialog.ShowDialog().Value == true)
            {
                method.SendDocumentIputFile(Convert.ToInt32(labletxt.Content), openfiledialog.FileName);
            }   
        }

        private void btnsend_ClickPhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
           
            if (openfiledialog.ShowDialog().Value == true)
            {
                method.SendPhotoIputFile(Convert.ToInt32(labletxt.Content), openfiledialog.FileName);
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
                method.SendVideoInputFile(Convert.ToInt32(labletxt.Content), openfiledialog.FileName);
            }
        }

        private void btnsend_ClickMusic(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "All Supported Audio | *.mp3; *.wma | MP3s | *.mp3 | WMAs | *.wma";
            if (openfiledialog.ShowDialog().Value == true)
            {
                method.SendAudioIputFile(Convert.ToInt32(labletxt.Content), openfiledialog.FileName);
            }

        }

        private void btnsend_ClickDoc(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "Pdf Files|*.pdf";
            if (openfiledialog.ShowDialog().Value == true)
            {
                method.SendDocumentIputFile(Convert.ToInt32(labletxt.Content), openfiledialog.FileName);
            }
        }
    }
}


