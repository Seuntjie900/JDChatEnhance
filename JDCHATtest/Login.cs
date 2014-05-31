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
            this.parent = parent;
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
