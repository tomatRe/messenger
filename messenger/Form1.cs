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
                MessageBox.Show("Tronco dame tu puto nombre ;_;", "Subnormal",
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
                    Chat chat = new Chat(!server.Checked, hostIp.Text, username.Text);
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

        /*private void ConnectToHost()
        {

            try
            {
                Socket socket1 = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint direccion = new IPEndPoint(IPAddress.Parse(hostIp.Text), 1234);

                socket1.Connect(direccion);
                Console.WriteLine("Conectado con exito!");

                //------Chat------

                //------Chat------

                socket1.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            string textToShow = "Conectando al servidor: " + hostIp.Text;
            MessageBox.Show(textToShow, "Esperando host", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CreateServer()
        {

            Socket socket1 = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint direccion = new IPEndPoint(IPAddress.Any, 1234);

            try
            {
                socket1.Bind(direccion);
                socket1.Listen(1);
                Socket escuchar = socket1.Accept();

                string textToShow = "Servidor creado con la Ip: " + GetLocalIPAddress();
                MessageBox.Show(textToShow, "Esperando clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //------Chat------

                //------Chat------

                socket1.Close();
                escuchar.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/
    }
}
