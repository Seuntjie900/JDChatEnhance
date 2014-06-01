using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JDChatEnhance;
namespace Jdchattest
{
    public partial class JDChat : Form
    {
        JDChatEnhance.JCCHat  ch;
        JDCAPI.jdInstance Instance;
        Login loginpage;
        public JDChat()
        {
            
            InitializeComponent();
            Instance = new JDCAPI.jdInstance();
            ch = new JDChatEnhance.JCCHat(Instance);
            this.Controls.Add(ch);
            ch.Dock = DockStyle.Fill;
            loginpage  = new Login(this);
            loginpage.Show();
            this.Visible = false;
        }

        public void Login(bool Doge)
        {
            loginpage.Close();
            this.WindowState = FormWindowState.Normal;
            this.Text = "Logging in";
            foreach (Control c in this.Controls)
            {
                c.Enabled = false;
            }
            Instance.Connect(Doge);
            if (Instance.uid != "" && Instance.uid != null)
            {
                this.Text = "Just-Dice Chat";
                foreach (Control c in this.Controls)
                {
                    c.Enabled = true;
                }
            }
            else
            {
                loginpage = new Login(this);
                loginpage.Show();
                this.WindowState = FormWindowState.Minimized;
            }

        }

        public void Login(string Secret, bool Doge)
        {
            loginpage.Close();
            this.WindowState = FormWindowState.Normal;
            this.Text = "Logging in";
            foreach (Control c in this.Controls)
            {
                c.Enabled = false;
            }
            Instance.Connect(Doge, Secret);
            if (Instance.uid != "" && Instance.uid != null)
            {
                this.Text = "Just-Dice Chat";
                foreach (Control c in this.Controls)
                {                    
                    c.Enabled = true;
                }
            }
            else
            {
                loginpage = new Login(this);
                loginpage.Show();
                this.WindowState = FormWindowState.Minimized;
            }

        }

        public void Login(string User, string Pw, string GA, bool Doge)
        {
            loginpage.Close();
            this.WindowState = FormWindowState.Normal;
            this.Text = "Logging in";
            foreach (Control c in this.Controls)
            {
                c.Enabled = false;
            }
            Instance.Connect(Doge, User, Pw, GA);
            if (Instance.uid != "" && Instance.uid != null)
            {
                this.Text = "Just-Dice Chat";
                foreach (Control c in this.Controls)
                {
                    c.Enabled = true;
                }
            }
            else
            {
                loginpage = new Login(this);
                loginpage.Show();
                this.WindowState = FormWindowState.Minimized;
            }

        }

        void intext_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        void intext_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        void input_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                Instance.Disconnect();
            }
            catch
            {

            }
            base.OnClosing(e);
        }
    }

    
}
