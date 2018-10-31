using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace messenger
{
    public class Client
    {
        string ip;
        private Socket ClientSocket;
        private Socket ReadSocket;
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        private AsynchronousSocketListener AsListener;
        public Client(string ip, string username, AsynchronousSocketListener listener)
        {
            this.AsListener = listener;
            this.ip = ip;
            ClientSocket = ConnectToHost(ip);
            Send("USR_" + username);
            //performListen(ClientSocket);
        }

        private void performListen(Socket clientSocket)
        {
            clientSocket.BeginAccept(new AsyncCallback(AcceptCallback), clientSocket);
        }

        public List<string> GetUsersConnectedToServer()
        {
            Send("USERS_");
            return null;
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.  
            StateObject state = new StateObject();
            state.WorkSocket = handler;

            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(readCallback), state);

        }

        private void readCallback(IAsyncResult ar)
        {
            string content = string.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.WorkSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read 
                // more data.
                content = state.sb.ToString();
                //if (content.IndexOf("<EOF>") > -1)
                //{
                // All the data has been read from the 
                // client. Display it on the console.
                Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                    content.Length, content);
                
                //}
            }
        }

        private Socket ConnectToHost(string hostIp)
        {
            Socket retSocket = null;
            try
            {
                Socket socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(hostIp, 1234);

                if (socket.Connected)
                {
                    retSocket = socket;
                    return retSocket;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return null;
        }

        public void Send(string data)
        {
            ClientSocket = ConnectToHost(ip);
            AsListener.Send(ClientSocket, data);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
