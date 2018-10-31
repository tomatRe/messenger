using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace messenger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            localIp.Text = GetLocalIPAddress();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            if (username.TextLength == 0 || username.ToString() == "")
            {
                MessageBox.Show("Nombre de usuario no puede estar vacío (subnormal)", "Subnormal",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (client.Checked && hostIp.TextLength == 0)
                {
                    MessageBox.Show("La direccion de host no puede ser nula", "Failed to connect",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    AsynchronousSocketListener asyncronous = new AsynchronousSocketListener();
                    if (server.Checked)
                    {
                        asyncronous.StartListening();
                    }

                    string ip = hostIp.Text;
                    if (ip == "")
                    {
                        ip = GetLocalIPAddress();
                    }
                    Chat chat = new Chat(!server.Checked, ip, username.Text, asyncronous);
                    chat.Show();
                }
            }
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }

}
