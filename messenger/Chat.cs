//><

using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace messenger
{
    public partial class Chat : Form
    {
        private bool isClient;
        private string hostIpvar;
        private string username;

        public Chat(bool isClient, string hostIpvar, string username)
        {
            InitializeComponent();

            this.isClient = isClient;
            this.hostIpvar = hostIpvar;
            this.username = username;

            if (isClient)
            {
                hostIp.Text = hostIpvar;
            }
            else
                hostIp.Text = GetLocalIPAddress();

            userList.Text = username;
            userList.Update();

            if (isClient)
            {
                ConnectToHost();
            }
            else
            {
                CreateServer();
            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void userList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ConnectToHost()
        {

            try
            {
                Socket socket1 = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint direccion = new IPEndPoint(IPAddress.Parse(hostIp.Text), 1234);

                socket1.Connect(direccion);

                string textToShow = "Conectando al servidor: " + hostIp.Text;
                MessageBox.Show(textToShow, "Esperando host", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //------Chat------

                //------Chat------

                socket1.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateServer()
        {

            Socket socket1 = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint direccion = new IPEndPoint(IPAddress.Any, 1234);

            try
            {
                string textToShow = "Servidor creado con la Ip: " + GetLocalIPAddress();
                MessageBox.Show(textToShow, "Esperando clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Esperando clientes", "Esperando clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                socket1.Bind(direccion);
                socket1.Listen(1);
                Socket escuchar = socket1.Accept();

                //------Chat------

                //------Chat------

                socket1.Close();
                escuchar.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void send_Click(object sender, EventArgs e)
        {

        }
    }
}
