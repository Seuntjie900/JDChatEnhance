using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace JDChatEnhance
{
    class ChatItem:System.Windows.Forms.Panel
    {
        Label LblDate = new Label();
        Label lblUid = new Label();
        Label lblUser = new Label();
        TextBox txtMessage = new TextBox();
        
        public ChatItem(JDCAPI.Chat Chat, string mode, int width)
        {
            
            //this.Width = width;
            this.Location = new System.Drawing.Point(9999, 9999);
            this.Dock = DockStyle.Bottom;
            LblDate.Text = Chat.Date.ToShortTimeString();
            this.Controls.Add(LblDate);
            LblDate.Location = new System.Drawing.Point(0,0);
            LblDate.AutoSize = true;
            LblDate.Font = new System.Drawing.Font(LblDate.Font.FontFamily, 10);
            LblDate.BackColor = System.Drawing.Color.White;
            LblDate.MouseClick += lblUser_MouseClick;
            LblDate.MouseDoubleClick += LblDate_MouseDoubleClick;
            lblUid.Text = "(" + Chat.UID + ")";
            lblUid.Location = new System.Drawing.Point(LblDate.Width + 1, 0);
            lblUid.AutoSize = true;
            lblUid.Font = new System.Drawing.Font(lblUid.Font.FontFamily, 10);
            lblUid.BackColor = System.Drawing.Color.White;
            lblUid.MouseClick += lblUser_MouseClick;
            lblUid.MouseDoubleClick += LblDate_MouseDoubleClick;
            this.Controls.Add(lblUid);
            lblUser.Text="<"+Chat.User+">";
            lblUser.Location = new System.Drawing.Point(lblUid.Location.X + lblUid.Width + 1, 0);
            lblUser.AutoSize = true;
            lblUser.Font = new System.Drawing.Font(lblUser.Font.FontFamily, 10);
            lblUser.BackColor = System.Drawing.Color.White;
            lblUser.MouseClick += lblUser_MouseClick;
            lblUser.MouseDoubleClick += LblDate_MouseDoubleClick;
            this.Controls.Add(lblUser);            
            txtMessage.Location = new System.Drawing.Point(lblUser.Location.X + lblUser.Width + 1, 0);
            txtMessage.Enabled = true;
            txtMessage.Multiline = true;
            txtMessage.ReadOnly = true;
            txtMessage.Width = width - txtMessage.Location.X;
            txtMessage.WordWrap = true;
            txtMessage.Text = Chat.Message;
            txtMessage.Font = new System.Drawing.Font(txtMessage.Font.FontFamily, 10);
            int lines = GetLines(txtMessage, Chat.Message);
            txtMessage.Height = (txtMessage.Height - 2) * lines;
            txtMessage.BackColor = this.BackColor;            
            txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txtMessage.BackColor = System.Drawing.Color.White;
            this.Height = txtMessage.Height;
            this.Controls.Add(txtMessage);
            this.SendToBack();
        }

        void LblDate_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (sender == LblDate)
            {
                Clipboard.SetText(LblDate.Text + " " + lblUid.Text + " " + lblUser.Text + " " + txtMessage.Text);
            }
            else if (sender == lblUid)
            {
                string uid = lblUid.Text.Substring(1, lblUid.Text.Length - 2);
                Clipboard.SetText(uid);
            }
            else if (sender == lblUser)
            {
                Clipboard.SetText(lblUser.Text.Substring(1, lblUser.Text.Length - 2));
            }
        }

        void lblUser_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                MenuItem item1 = new MenuItem();
                item1.Text = "Copy Name";
                item1.Click += item1_Click;
                MenuItem item2 = new MenuItem();
                item2.Text = "Copy UID";
                item2.Click += item2_Click;
                MenuItem item3 = new MenuItem();
                item3.Text = "Copy Message";
                item3.Click += item3_Click;
                MenuItem item4 = new MenuItem();
                item4.Text = "Copy All";
                item4.Click += item4_Click;
                MenuItem item5 = new MenuItem();
                item5.Text = "PM user";
                item5.Click += item5_Click;
                MenuItem[] items = new MenuItem[5];
                items[0] = item1;
                items[1] = item2;
                items[2] = item3;
                items[3] = item4;
                items[4] = item5;
                ContextMenu meny = new ContextMenu(items);
                meny.Show(sender as Control, e.Location);
            }
            
        }

        void item5_Click(object sender, EventArgs e)
        {
            (this.Parent.Parent.Parent.Parent.Parent as JCCHat).newpm(lblUser.Text.Substring(1, lblUser.Text.Length - 2), lblUid.Text.Substring(1, lblUid.Text.Length - 2));
        }

        void item4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(LblDate.Text + " " + lblUid.Text + " " + lblUser.Text + " " + txtMessage.Text);
        }

        void item3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtMessage.Text);
        }

        void item2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblUid.Text.Substring(1, lblUid.Text.Length-2));
        }

        void item1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblUser.Text.Substring(1, lblUser.Text.Length - 2));
        }

        

        int GetLines(TextBox txt, string content)
        {
            System.Windows.Media.Typeface tf = new System.Windows.Media.Typeface(txt.Font.FontFamily.Name);
            System.Windows.Media.FormattedText ft = new System.Windows.Media.FormattedText(
                content, 
                System.Globalization.CultureInfo.CurrentCulture, 
                System.Windows.FlowDirection.LeftToRight, 
                tf, 
                txt.Font.Size, 
                System.Windows.Media.Brushes.Black);
            return (int)(ft.Width / txt.Width)+1;
        }

        
    }
}
