using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleWifi;

namespace WindowsFormsApplication27
{
    
    public partial class Form1 : Form
    {
        private Wifi wifi;
        public Form1()
        {
            InitializeComponent();
        }

        private bool connectwifi(AccessPoint ap,string password)
        {
            AuthRequest authrequest = new AuthRequest(ap);
            authrequest.Password = password;
            return ap.Connect(authrequest);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            wifi = new Wifi();
            List<AccessPoint> aps = wifi.GetAccessPoints();
            foreach (AccessPoint ap in aps)
            {
                ListViewItem listobj = new ListViewItem(ap.Name);
                listobj.SubItems.Add(ap.SignalStrength + "%");
                listobj.Tag = ap;
                listView1.Items.Add(listobj);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count>0&& textBox1.Text.Length>0)
            {
                ListViewItem selecteditem = listView1.SelectedItems[0];
                AccessPoint ap = (AccessPoint)selecteditem.Tag;
                if (connectwifi(ap, textBox1.Text))
                {
                    label2.Text = ap.Name +"تم الاتصال بــ"  ; 
                } 
                else 
                { label2.Text = ap.Name+ "تعذر الاتصال بــ";}
              
            }
            else
            { label2.Text = "اكتب كلمة المرور او اختر شبكة"; }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (wifi.ConnectionStatus == WifiStatus.Connected) { wifi.Disconnect(); label2.Text = "تم فطع الاتصال"; }
        }

       
    }
}
