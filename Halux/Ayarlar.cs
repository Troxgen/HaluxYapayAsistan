using System;
using System.Windows.Forms;
using System.Xml;
namespace Halux
{
    public partial class Ayarlar : Form
    {
        public Ayarlar()
        {
            InitializeComponent();
        }
        XmlDocument xmlDoc = new XmlDocument();
        private void Ayarlar_Load(object sender, EventArgs e)
        {
       
        }


        private void button3_Click(object sender, EventArgs e)
        {
            xmlDoc.Load("Veriler.xml");
            XmlNode SesGirisNode = xmlDoc.SelectSingleNode("//prefix");
            if (SesGirisNode != null)
            {
                SesGirisNode.InnerText = textBox1.Text;
                xmlDoc.Save("Veriler.xml");
                MessageBox.Show($"Ses çıkışı '{textBox1.Text}' olarak güncellendi.");
            }
        }
    }
}