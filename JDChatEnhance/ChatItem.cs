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
            lblUid.Text = "<" + Chat.UID + ">";
            lblUid.Location = new System.Drawing.Point(LblDate.Width + 1, 0);
            lblUid.AutoSize = true;
            this.Controls.Add(lblUid);
            lblUser.Text="("+Chat.User+")";
            lblUser.Location = new System.Drawing.Point(lblUid.Location.X + lblUid.Width + 1, 0);
            lblUser.AutoSize = true;
            this.Controls.Add(lblUser);
            
            txtMessage.Location = new System.Drawing.Point(lblUser.Location.X + lblUser.Width + 1, 0);
            txtMessage.Enabled = true;
            txtMessage.Multiline = true;
            txtMessage.ReadOnly = true;
            txtMessage.Width = width - txtMessage.Location.X;
            txtMessage.WordWrap = true;
            txtMessage.Text = Chat.Message;
            int lines = GetLines(txtMessage, Chat.Message);
            txtMessage.Height = (txtMessage.Height - 2) * lines;
            txtMessage.BackColor = this.BackColor;            
            txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;            
            this.Height = txtMessage.Height;
            this.Controls.Add(txtMessage);
            this.SendToBack();
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
