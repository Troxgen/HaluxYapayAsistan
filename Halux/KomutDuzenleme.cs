using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Halux
{
    public partial class KomutDuzenleme : Form
    {
        public KomutDuzenleme()
        {
            InitializeComponent();
        }
        XmlDocument xmlDoc = new XmlDocument();
         public void Reboot()
        {
            string applicationPath = Application.ExecutablePath;
            Process.Start(applicationPath);
            Application.Exit();

        }
        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            button4.Visible = false;
            button5.Visible = false;
            button7.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            button4.Visible = false;
            button5.Visible = false;
            button7.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            button4.Visible = false;
            button5.Visible = false;
            button7.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

          
                XmlNode rootNode = xmlDoc.SelectSingleNode("//komutlar");
                XmlElement newKomut = xmlDoc.CreateElement("komut");

                XmlElement komutAdi = xmlDoc.CreateElement("komutadi");
                komutAdi.InnerText = EkleKomutAdiText.Text;
                newKomut.AppendChild(komutAdi);

                XmlElement islev = xmlDoc.CreateElement("islev");
                islev.InnerText = EkleKomutTuruCombo.SelectedItem.ToString();
                newKomut.AppendChild(islev);

                if (!string.IsNullOrEmpty(EkleKomutDegeriText.Text))
                {
                    XmlElement deger = xmlDoc.CreateElement("deger");
                    deger.InnerText = EkleKomutDegeriText.Text;
                    newKomut.AppendChild(deger);
                }

                rootNode.AppendChild(newKomut);
                xmlDoc.Save("Veriler.xml");

            EkleKomutAdiText.Clear();
            EkleKomutAdiText.Clear();
            button4.Visible = true;
            button5.Visible = true;
            button7.Visible = true;
            panel1.Visible = false;
            Reboot();
        }

        private void button2_Click(object sender, EventArgs e)
        {

                XmlNode komutNode = xmlDoc.SelectSingleNode("//komut[komutadi='" + SilKomutAdiCombo.SelectedItem.ToString() + "']");
                if (komutNode != null)
                {
                    komutNode.ParentNode.RemoveChild(komutNode);
                    xmlDoc.Save("Veriler.xml");

                    SilKomutAdiCombo.Items.Remove(SilKomutAdiCombo.SelectedItem);
                }
                else
                {
                }
            panel2.Visible = false;
            button4.Visible = true;
            button5.Visible = true;
            button7.Visible = true;
            Reboot();

        }

        private void button3_Click(object sender, EventArgs e)
        {
                XmlNode komutNode = xmlDoc.SelectSingleNode("//komut[komutadi='" + DuzenleKomutAdiCombo.SelectedItem.ToString() + "']");
                if (komutNode != null)
                {
                    komutNode.SelectSingleNode("komutadi").InnerText = DuzenleKomutAdiCombo.Text;
                    komutNode.SelectSingleNode("islev").InnerText = DuzenleKomutTuruCombo.SelectedItem.ToString();

                    XmlNode degerNode = komutNode.SelectSingleNode("deger");
                    if (degerNode != null)
                    {
                        degerNode.InnerText = DuzenleDegerText.Text;
                    }
                    else if (!string.IsNullOrEmpty(DuzenleDegerText.Text))
                    {
                        XmlElement deger = xmlDoc.CreateElement("deger");
                        deger.InnerText = DuzenleDegerText.Text;
                        komutNode.AppendChild(deger);
                    }

                    xmlDoc.Save("Veriler.xml");
                    MessageBox.Show("Komut başarıyla güncellendi.");
                DuzenleDegerText.Clear();
                }
                else
                {
                }

            panel3.Visible = false;
            button4.Visible = true;
            button5.Visible = true;
            button7.Visible = true;
            Reboot();

        }

        private void KomutDuzenleme_Load(object sender, EventArgs e)
        {
            EkleKomutTuruCombo.Items.Clear();
            DuzenleKomutTuruCombo.Items.Clear();
            DuzenleKomutAdiCombo.Items.Clear();
            SilKomutAdiCombo.Items.Clear();

            xmlDoc.Load("Veriler.xml");
            XmlNodeList komutNodes = xmlDoc.SelectNodes("//komut/komutadi");
            foreach (XmlNode komutNode in komutNodes)
            {
                string komutAdi = komutNode.InnerText;
                Console.WriteLine(komutAdi);    
                SilKomutAdiCombo.Items.Add(komutAdi);
                DuzenleKomutAdiCombo.Items.Add(komutAdi);
            }
            XmlNodeList komutTuru = xmlDoc.SelectNodes("//komut/islev");
            foreach (XmlNode komutNode in komutTuru)
            {
                string komutAdi = komutNode.InnerText;
                Console.WriteLine(komutAdi);
                EkleKomutTuruCombo.Items.Add(komutAdi);
                DuzenleKomutTuruCombo.Items.Add(komutAdi);
            }


        }
    }
}
