using System;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Diagnostics;
using AudioSwitcher.AudioApi.CoreAudio;
using System.Xml;
using System.Runtime.InteropServices;
using System.Media;
using System.Drawing;

namespace Halux
{
    public partial class AnaEkran : Form
    {
        private SpeechSynthesizer konusma = new SpeechSynthesizer();
        public const int KEYEVENTF_EXTENDEDKEY = 1;
        public const int KEYEVENTF_KEYUP = 0;
        public const int VK_MEDIA_NEXT_TRACK = 0xB0;
        public const int VK_MEDIA_PLAY_PAUSE = 0xB3;
        public const int VK_MEDIA_PREV_TRACK = 0xB1;
        public bool AlgilamaDurumu = false;
        private Timer commandTimer;
        private string Prefix;
        private bool Kapanma = false;
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);

        private SpeechRecognitionEngine PrefixKelimeTanimlama;
        private SpeechRecognitionEngine KomutTanimlama;
        private XmlDocument xmlDoc;

        public AnaEkran()
        {
            InitializeComponent();
            SesTanimlama();
            BaslatmaZamanlayicisi();
        }
        
        private void SesTanimlama()
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load("Veriler.xml");

            var cultureInfo = new System.Globalization.CultureInfo("en-US");
            XmlNode prefixNode = xmlDoc.SelectSingleNode("//prefix");
            if (prefixNode != null)
            {
                Prefix = prefixNode.InnerText.ToLower();
            }

            PrefixKelimeTanimlama = new SpeechRecognitionEngine(cultureInfo);
            PrefixKelimeTanimlama.SetInputToDefaultAudioDevice();
            Choices keywordChoices = new Choices(Prefix);
            GrammarBuilder keywordGrammarBuilder = new GrammarBuilder(keywordChoices) { Culture = cultureInfo };
            Grammar keywordGrammar = new Grammar(keywordGrammarBuilder);
            PrefixKelimeTanimlama.LoadGrammar(keywordGrammar);
            PrefixKelimeTanimlama.SpeechRecognized += PrefixKelimeTanimlama_SpeechRecognized;
            PrefixKelimeTanimlama.RecognizeAsync(RecognizeMode.Multiple);

            KomutTanimlama = new SpeechRecognitionEngine(cultureInfo);
            KomutTanimlama.SetInputToDefaultAudioDevice();
            KomutYukleme();
            KomutTanimlama.SpeechRecognized += KomutTanimlama_SpeechRecognized;
        }

        private void KomutYukleme()
        {
            Choices commandChoices = new Choices();
            foreach (XmlNode node in xmlDoc.SelectNodes("//komut"))
            {
                string commandName = node.SelectSingleNode("komutadi").InnerText.ToLower();
                commandChoices.Add(commandName);
            }

            var cultureInfo = new System.Globalization.CultureInfo("en-US");
            GrammarBuilder KomutGrammerBuilder = new GrammarBuilder(commandChoices) { Culture = cultureInfo };
            Grammar KomutGrammer = new Grammar(KomutGrammerBuilder);
            KomutTanimlama.LoadGrammar(KomutGrammer);
        }

        private void BaslatmaZamanlayicisi()
        {
            commandTimer = new Timer();
            commandTimer.Interval = 5000;
            commandTimer.Tick += SesGelmeyinceKapanma;
        }

        private void SesGelmeyinceKapanma(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("Resimler/Başlıksız-2.png");
            commandTimer.Stop();
            AlgilamaDurumu = false;
            KomutTanimlama.RecognizeAsyncCancel();
            PrefixKelimeTanimlama.RecognizeAsync(RecognizeMode.Multiple);
            string soundFilePath = "Sesler/kapanış.wav";
            SoundPlayer soundPlayer = new SoundPlayer(soundFilePath);
            soundPlayer.Play();

        }

        private void PrefixKelimeTanimlama_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            pictureBox1.Image = Image.FromFile("Resimler/Başlıksız-1.png");

            if (e.Result.Text.Equals(Prefix, StringComparison.OrdinalIgnoreCase))
            {
                PrefixKelimeTanimlama.RecognizeAsyncCancel();
                AlgilamaDurumu = true;
                KomutTanimlama.RecognizeAsync(RecognizeMode.Multiple);
                commandTimer.Start();
                string soundFilePath = "Sesler/açılış.wav";
                SoundPlayer soundPlayer = new SoundPlayer(soundFilePath);
                soundPlayer.Play();
            }
        }

        private void KomutTanimlama_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (AlgilamaDurumu)
            {
                KomutTanimlama.RecognizeAsyncCancel();
                commandTimer.Stop(); 
                string command = e.Result.Text.ToLower();
                KomutCalistir(command);
                AlgilamaDurumu = false;
                PrefixKelimeTanimlama.RecognizeAsync(RecognizeMode.Multiple);
                pictureBox1.Image = Image.FromFile("Resimler/Başlıksız-2.png");
            }
        }

        private void KomutCalistir(string command)
        {
            foreach (XmlNode node in xmlDoc.SelectNodes("//komut"))
            {
                string commandName = node.SelectSingleNode("komutadi").InnerText.ToLower();
                if (commandName == command)
                {
                    string islev = node.SelectSingleNode("islev").InnerText;
                    string deger = node.SelectSingleNode("deger")?.InnerText;
                    string deger2 = node.SelectSingleNode("deger2")?.InnerText;
                    string SoylenecekKelime = node.SelectSingleNode("SoylecekCumle")?.InnerText;

                    Performislev(islev, deger, deger2, SoylenecekKelime);
                    break;
                }
            }
        }

        private void Performislev(string islev, string deger, string deger2, string SoylenecekKelime)
        {
            switch (islev)
            {
               
                case "ProgramAc":
                    konusma.Speak(SoylenecekKelime);
                    Process.Start(deger);
                    break;

                case "Goster":
                    konusma.Speak(SoylenecekKelime);
                    this.Show();
                    break;

                case "Gizle":
                    konusma.Speak(SoylenecekKelime);
                    this.Hide();
                    break;

                case "SesDegeri":
                    if (int.TryParse(deger, out int volume))
                    {
                        SetVolume(volume);
                    }
                    break;

                case "KisaYol":
                    konusma.Speak(SoylenecekKelime);
                    SendKeys.Send(deger);
                    break;

                case "SarkiAtla":
                    konusma.Speak(SoylenecekKelime);
                    keybd_event(VK_MEDIA_PREV_TRACK, 0, KEYEVENTF_EXTENDEDKEY, IntPtr.Zero);
                    break;

                case "SarkiDon":
                    konusma.Speak(SoylenecekKelime);
                    keybd_event(VK_MEDIA_NEXT_TRACK, 0, KEYEVENTF_EXTENDEDKEY, IntPtr.Zero);
                    break;

                case "oynat":
                    konusma.Speak(SoylenecekKelime);
                    keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_EXTENDEDKEY, IntPtr.Zero);
                    break;
                case "Soyle":
                    konusma.Speak(SoylenecekKelime);
                    break;
                case "Kur":
                         XmlDocument Kur = new XmlDocument();
                         Kur.Load("https://www.tcmb.gov.tr/kurlar/today.xml");
                        string DolarAlis = Kur.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerText;
                        string EuroAlis = Kur.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerText;
                        konusma.Speak("Dolar Fiyatı Anlık " + DolarAlis + "Tl, Euro Fiyatı Anlık =" + EuroAlis);
                    break;
                case "PowerOff":
                    Process.Start("shutdown", "/t 600");
                    konusma.Speak(SoylenecekKelime);
                    break;
                case "PowerOffCancel":
                    Process.Start("shutdown", "/a");
                    konusma.Speak(SoylenecekKelime);
                    break;
                default:
                    konusma.Speak("Anlaşılmadı.");
                    break;
            }
            pictureBox1.Image = Image.FromFile("Resimler/Başlıksız-2.png");
            string soundFilePath = "Sesler/kapanış.wav";
            SoundPlayer soundPlayer = new SoundPlayer(soundFilePath);
            soundPlayer.Play(); 
            KomutTanimlama.RecognizeAsyncCancel();

        }

        private void SetVolume(int volume)
        {
            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            defaultPlaybackDevice.Volume = volume;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Kapanma = true;
            Application.Exit();
        }

        private void AnaEkran_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Kapanma == true)
            {
                e.Cancel = true;
                this.Hide();
            }

        }

        private void AnaEkran_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("Resimler/Başlıksız-2.png");
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new System.Drawing.Icon("icon.ico");
            notifyIcon.Visible = true;

            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Kapat");
            exitMenuItem.Click += (s, args) =>
            {
                Kapanma = false;
                Application.Exit();
                this.Close();
                konusma.Speak("Kapatılıyor");
            };
            contextMenuStrip.Items.Add(exitMenuItem);
            notifyIcon.ContextMenuStrip = contextMenuStrip;
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.ForeColor = System.Drawing.Color.Red;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = System.Drawing.Color.White;
        }
        private void komutDüzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KomutDuzenleme kd = new KomutDuzenleme();
            kd.ShowDialog();
        }

        private void ayarlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ayarlar AyarlarForm = new Ayarlar();
            AyarlarForm.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
