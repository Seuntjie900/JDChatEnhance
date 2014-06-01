using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jdchattest
{
    public partial class Login : Form
    {
        JDChat parent;
        public Login(JDChat parent)
        {
            InitializeComponent();
            Button Blank = new Button();
            Blank.Click += Blank_Click;
            Blank.Text = "New Account";
            Blank.Height = button1.Height;
            Blank.Top = button1.Top;
            Blank.Left = button1.Left - 100;
            this.Controls.Add(Blank);
            this.parent = parent;
            this.BringToFront();
            parent.WindowState = FormWindowState.Minimized;
        }

        void Blank_Click(object sender, EventArgs e)
        {
            parent.Login(radioButton2.Checked);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSecret.Text != "")
            {
                if (txtSecret.Text.Contains("just-dice.com/"))
                {
                    string secret = txtSecret.Text.Substring(txtSecret.Text.LastIndexOf("/")+1);
                    secret = secret.Replace("/", "");
                    parent.Login(secret, radioButton2.Checked);
                }
            }
            else if (txtUser.Text!="" && txtPass.Text!="")
            {
                parent.Login(txtUser.Text, txtPass.Text, txtGA.Text, radioButton2.Checked);
                
                
            }
            else
            {

            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
