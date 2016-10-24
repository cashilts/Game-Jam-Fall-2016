using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace GameServerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            startListening();

        }

        static string data = null;
        Dictionary<string, IPEndPoint> Connections = new Dictionary<string, IPEndPoint>();
        ManualResetEvent allDone = new ManualResetEvent(true);

        public class stateObject {
            public Socket workSocket = null;
            public const int bufferSize = 1024;
            public byte[] buffer = new byte[1024];
            public StringBuilder sb = new StringBuilder();
        }

        void AcceptCallback(IAsyncResult ar) {
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            allDone.Set();

            stateObject state = new stateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, stateObject.bufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        void ReadCallback(IAsyncResult ar) {
            stateObject state = (stateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            int read = handler.EndReceive(ar);
            state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, read));
            string content = state.sb.ToString();
            if (content.Contains("<EOF>"))
            {
                content = content.Replace("<EOF>", "");
                if (content.StartsWith("Host"))
                {
                    string hostName = content.Substring(4);
                    hostName = hostName.Replace("\0", "");
                    IPEndPoint endPoint = handler.RemoteEndPoint as IPEndPoint;
                    KeyValuePair<String, IPEndPoint> temp = new KeyValuePair<string, IPEndPoint>(hostName, endPoint);
                    Connections.Add(hostName, endPoint);
                }
                else if (content.StartsWith("Client"))
                {
                    content = null;
                    foreach (KeyValuePair<String, IPEndPoint> host in Connections)
                    {
                        content += host.Key;
                    }
                    content += "<EOF>";
                    byte[] bytes = new byte[1024];
                    bytes = Encoding.ASCII.GetBytes(content);
                    handler.Send(bytes);
                }
                else if (content.StartsWith("Connect"))
                {
                    string host = content.Substring(7);
                    host = host.Replace("\0", "");
                    IPEndPoint toSend;
                    Connections.TryGetValue(host, out toSend);
                    string response = toSend.Address + "@" + toSend.Port + "<EOF>";
                    byte[] bytes = new byte[1024];
                    bytes = Encoding.ASCII.GetBytes(response);
                    handler.Send(bytes);
                }
            }
        }


        void startListening()
        {
            byte[] bytes = new byte[1024];
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAdress = ipHostInfo.AddressList[0];
            label1.Text = ipAdress.ToString();
            IPEndPoint localEndPoint = new IPEndPoint(ipAdress, 11000);

            Socket listener = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            while (true)
            {
                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(10);
                    while (true)
                    {
                        allDone.Reset();
                        listener.BeginAccept(new AsyncCallback(this.AcceptCallback), listener);
                        allDone.WaitOne();
                        
                    }
                   
                    
                }
                catch { }
            }
        }
    }
}
