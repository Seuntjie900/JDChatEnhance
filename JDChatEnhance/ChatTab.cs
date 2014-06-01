using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using JDCAPI;
namespace JDChatEnhance
{
    class ChatTab: TabPage
    {
        Panel Chat = new Panel();
        TextBox intext = new TextBox();
        Panel input = new Panel();
        jdInstance instance;
        Panel container = new Panel();
        string type = "";

        public ChatTab(jdInstance instance, string RoomName, string id, string type)
        {
            init(instance, RoomName, id, type);
            
        }
        private void init(jdInstance instance, string RoomName, string id, string type)
        {
            this.type = type;
            container.Dock = DockStyle.Fill;
            container.AutoScroll = true;
            this.Text = RoomName;
            this.Name = id;
            this.instance = instance;           
            this.Controls.Add(container);
            Chat.Width = this.Width-25;
            Chat.Height = 0;
            container.Controls.Add(Chat);
            Chat.BackColor = Color.White;
            this.Controls.Add(input);
            input.Dock = DockStyle.Bottom;
            input.Height = 25;
            intext.Width = input.Width - 100;
            input.Controls.Add(intext);
            intext.Dock = DockStyle.Left;
            intext.Multiline = true;
            Button send = new Button();
            send.Text = "Send";
            send.Dock = DockStyle.Right;
            send.Width = 100;
            input.Controls.Add(send);
            this.Paint += JCCHat_Paint;
            intext.KeyUp += intext_KeyUp;            
            send.Click += send_Click;
            this.MouseClick += ChatTab_MouseClick;
            this.MouseDown += ChatTab_MouseClick;
        }

        void ChatTab_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                (this.Parent as TabControl).TabPages.Remove(this);
            }
        }


        public ChatTab(jdInstance instance, string name, string id ,string type, Size size)
        {
            this.Size = size;
            init(instance, name, id, type);
        }

        public void updateSize()
        {
            UpdateChatWidth(this.Width-25);
            
        }
            
       

        delegate void NewChatItem(ChatItem item);
        void UpdateChat(ChatItem item)
        {
            if (InvokeRequired)
            {
                Invoke(new NewChatItem(UpdateChat), item);
                return;
            }
            this.Chat.Controls.Add(item);
            if ((this.Parent as TabControl).SelectedTab != this && !this.Text.Contains("*"))
            {
                this.Text = "*" + this.Text;
            }
        }

        delegate void dUpdteeChatHeight(int height);
        void UpdateChatHeight(int height)
        {
            if (InvokeRequired)
            {
                Invoke(new dUpdteeChatHeight(UpdateChatHeight), height);
                return;
            }
            Chat.Height = height;
        }
        delegate void dUpdteeChatWidth(int Width);
        void UpdateChatWidth(int Width)
        {
            if (InvokeRequired)
            {
                Invoke(new dUpdteeChatWidth(UpdateChatWidth), Width);
                return;
            }
            Chat.Width = Width;
        }

        delegate void dUpdateScroll(int value);
        void UpdateScroll(int value)
        {
            if (InvokeRequired)
            {
                Invoke(new dUpdateScroll(UpdateScroll), value);
                return;
            }
            container.VerticalScroll.Value = value;
        }


        delegate void dUpdateName();
        public void UpdateName()
        {
            if (InvokeRequired)
            {
                Invoke(new dUpdateName(UpdateName));
            }
            this.Text = this.Text.Replace("*","");
        }
        delegate void dFocustxt();
        public void Focustxt()
        {
            if (InvokeRequired)
            {
                Invoke(new dFocustxt(Focustxt));
            }
            intext.Focus();
        }


        void send_Click(object sender, EventArgs e)
        {
            instance.Chat(((this.type == "none") ? "" : (this.type == "pm") ? "/pm " + this.Name + " " : "@" + this.Name + " ") + intext.Text);
            intext.Text = "";
            intext.Focus();
        }
        void intext_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && intext.Text != "")
            {
                intext.Text = intext.Text.Replace("\r\n", "");
                instance.Chat(((this.type == "none") ? "" : (this.type == "pm") ? "/pm " + this.Name + " " : "@" + this.Name + " ") + intext.Text);
                intext.Text = "";
                intext.Focus();
            }
        }


        public void addmesage(Chat chat)
        {
            //checkactive(chat);
            //checkpm(chat);

            bool scroll = (container.VerticalScroll.Value < container.VerticalScroll.Maximum);
            int val = container.VerticalScroll.Value;
            ChatItem tmp = new ChatItem(chat, "none", Chat.Width);
            //tmp.Dock = DockStyle.Top;
            UpdateChatHeight(Chat.Height + tmp.Height);
            UpdateChat(tmp);
            if (scroll)
                UpdateScroll( container.VerticalScroll.Maximum);
            else
                UpdateScroll( val);
        }


        delegate void dpaint(object sender, PaintEventArgs e);
        void JCCHat_Paint(object sender, PaintEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new dpaint(JCCHat_Paint), sender, e);
            }
            updateSize();
            intext.Width = input.Width - 100;
        }

        
    }
}
