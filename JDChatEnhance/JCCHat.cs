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
    public class JCCHat:Panel
    {
        public string RoomName { get; set; }
        TabControl Tabs = new TabControl();
        
        
        jdInstance instance;
        List<User> ActiveUsers = new List<User>();
        long ActiveTime = 60 * 10;
        Timer Active = new Timer();
        
        

        public JCCHat(jdInstance Instance)
        {
            instance = Instance;
            ChatTab defaulttab = new ChatTab(instance, "All","ALL", "none");
            Tabs.TabPages.Add(defaulttab);
            this.Controls.Add(Tabs);
            Tabs.Dock = DockStyle.Fill;
            instance.OnChat += instance_OnChat;
            instance.OnOldChat += instance_OnChat;
            Active.Interval = 100;
            Active.Tick += Active_Tick;
            Active.Start();
            Active.Enabled = true;
            Tabs.SelectedIndexChanged +=Tabs_SelectedIndexChanged;
            Tabs.MouseClick += Tabs_MouseClick;
            
            
        }

       
        void Tabs_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                TabPage tab = Tabs.TabPages.Cast<TabPage>()
                   .Where((t, i) =>
                    Tabs.GetTabRect(i)
                    .Contains(e.Location)).First();
                if (tab != null && tab!= Tabs.TabPages[0]) Tabs.TabPages.Remove(tab);
            }
        }

        private void Tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            (Tabs.TabPages[Tabs.SelectedIndex] as ChatTab).UpdateName();
            (Tabs.TabPages[Tabs.SelectedIndex] as ChatTab).Focustxt();
        }

        void Active_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ActiveUsers.Count; i++ )
            {
                if ((DateTime.Now - ActiveUsers[i].LastActive).TotalSeconds > ActiveTime)
                {
                    ActiveUsers.RemoveAt(i--);
                }
            }
        }

        private delegate void dNewTab(ChatTab tab);
        void NewTab(ChatTab tab)
        {
            if (InvokeRequired)
            {
                Invoke(new dNewTab(NewTab), tab);
                return;
            }
            this.Tabs.TabPages.Add(tab);
        }
        private delegate void dSwitchTab(ChatTab tab);
        void SwitchTab(ChatTab tab)
        {
            if (InvokeRequired)
            {
                Invoke(new dSwitchTab(SwitchTab), tab);
                return;
            }
            this.Tabs.SelectedTab = tab;
        }
        public void newpm(string name, string id)
        {
            try
            {
                ChatTab tmp = new ChatTab(instance, name, id, "pm", Tabs.TabPages[0].Size);
                NewTab(tmp);
                SwitchTab(tmp);
            }
            catch
            {

            }
        }
        void instance_OnChat(Chat chat)
        {

            if (checkpm(chat))
            {
                bool found = false;

                string id = chat.UID;
                string name = chat.User;
                if (id == instance.uid)
                {
                    string tmp = chat.RawMessage;
                    tmp = tmp.Substring(tmp.IndexOf("→")+3);
                    id = tmp.Substring(0,tmp.IndexOf(")"));
                    name = tmp.Substring(tmp.IndexOf("<") + 1, tmp.IndexOf(">") - tmp.IndexOf("<"));
                }
                foreach (TabPage tp in Tabs.TabPages)
                {
                    if (tp.Name == id)
                    {
                       
                        (tp as ChatTab).addmesage(chat);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    //Tabs.TabPages.Add(new ChatTab(instance, chat.User));
                    try
                    {
                        ChatTab tmp = new ChatTab(instance, name,id,"pm", Tabs.TabPages[0].Size);
                        NewTab(tmp);
                    }
                    catch
                    {

                    }

                    (Tabs.TabPages[Tabs.TabPages.Count - 1] as ChatTab).addmesage(chat);
                }
            }
            else if (!checkroom(chat))            
            {
                (Tabs.TabPages[0] as ChatTab).addmesage(chat);
            }
           
        }

        bool checkroom(Chat chat)
        {
            string[] words = chat.Message.Split(' ');
            if (words[0][0] == '@' && words[0].Length > 1)
            {
                string name = words[0].Substring(1);
                string newmsg = "";
                for (int i = 1; i < words.Length; i++)
                {
                    newmsg += words[i];
                    if (i != words.Length - 1)
                        newmsg += " ";
                }
                chat.Message = newmsg;
                bool found = false;
                foreach (TabPage tp in Tabs.TabPages)
                {
                    if (tp.Name == name)
                    {
                        (tp as ChatTab).addmesage(chat);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    if (chat.UID == instance.uid)
                    {
                        try
                        {
                            ChatTab tmp = new ChatTab(instance, name, name, "room", Tabs.TabPages[0].Size);
                            NewTab(tmp);
                        }
                        catch
                        {

                        }

                        (Tabs.TabPages[Tabs.TabPages.Count - 1] as ChatTab).addmesage(chat);
                    }
                    else
                    {
                        chat.Message = "@" + name + " "+ chat.Message;
                        (Tabs.TabPages[0] as ChatTab).addmesage(chat);
                    }
                }
                //addmesage(chat);
                return true;
            }
            return false;
        }

        void checkactive(Chat chat)
        {
            bool foundhim = false;
            foreach (User u in ActiveUsers)
            {
                if (long.Parse(chat.UID) == u.UID)
                {
                    u.LastMessage = chat.Message;
                    u.LastActive = DateTime.Now;
                    foundhim = true;
                }
            }
            if (!foundhim)
            {
                User tmp = new User();
                tmp.LastActive = DateTime.Now;
                tmp.LastMessage = chat.Message;
                tmp.UID = long.Parse(chat.UID);
                tmp.Username = chat.User;
                ActiveUsers.Add(tmp);
            }
        }

        bool checkpm(Chat chat)
        {
            if (chat.Type == "pm")
            {
                return true;
            }
            return false;
        }

        

        
    }
}
