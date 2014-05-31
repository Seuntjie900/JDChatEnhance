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
        }

        public void Login(string Secret, bool Doge)
        {
            Instance.Connect(Doge, Secret);
            if (Instance.uid!="" && Instance.uid!= null)
            {
                loginpage.Close();
            }

        }

        public void Login(string User, string Pw, string GA, bool Doge)
        {
            Instance.Connect(Doge, User, Pw, GA);
            if (Instance.uid != "" && Instance.uid != null)
            {
                loginpage.Close();
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
            /*JDCAPI.Chat cht = new JDCAPI.Chat();
            cht.UID = "1";
            cht.User = "dooglus";
            cht.Message = "hello there hello there hello there hello there hello there hello there hello there hello there hello there";
            cht.Date = DateTime.Now;
            ch.testmsg(cht);*/
            Instance.Connect(false,"bfa415298a3ae20573ae89e08af099385518e667fb233fe750516e38cfeac41a");
            button1.Visible = false;
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
