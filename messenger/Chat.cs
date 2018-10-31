//><

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace messenger
{
    public partial class Chat : Form
    {
        private bool isClient;
        private string hostIpvar;
        private string username;
        private Client client;

        public Chat(bool isClient, string hostIpvar, string username, AsynchronousSocketListener lmao)
        {
            InitializeComponent();
            ListBox.CheckForIllegalCrossThreadCalls = false;

            lmao.SetChatInstance(this);
            this.isClient = isClient;
            this.hostIpvar = hostIpvar;
            this.username = username;

            if (isClient)
            {
                hostIp.Text = hostIpvar;
            }
            else
            {
                hostIp.Text = Utils.GetLocalIPAddress();
            }

            client = new Client(hostIpvar, username, lmao);
            if(client != null)
            {
                string textToShow = "Conectando al servidor: " + hostIp;
                MessageBox.Show(textToShow, "Esperando host", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void userList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void UpdateUserList(string user)
        {
            userList.Items.Add(user);
        }
 
        private void send_Click(object sender, EventArgs e)
        {
            client.Send(textBox1.Text);
            textBox1.Text = "";
        }

        private void Chat_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        public void UpdateChatText(string text)
        {
            chatBox.Items.Add(text);
        }
    }

   
}
