using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.UI;

public static class NetworkClient {

    public static void checkServer() {
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAdress = ipHostInfo.AddressList[0];
        IPEndPoint remoteEP = new IPEndPoint(ipAdress, 11000);
        Socket testSocket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        testSocket.Bind(remoteEP);
        
    }


    public static void RecieveHosts(IAsyncResult ar) {
        stateObject state = (stateObject)ar.AsyncState;

        Socket handler = state.workSocket;
        int read = handler.EndReceive(ar);
        state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, read));
        string content = state.sb.ToString();
        if (content.Contains("<EOF>")) {
            content = content.Replace("<EOF>", "");
            if (content == "")
            {
                Menu.updateMenuText = "No hosts available";
                Menu.update = true;
            }
            else {
                string text = "";
                for (int i = 0; i < content.Length;) {
                    int hostNameLen = content.IndexOf('-', i);
                    string subString = content.Substring(i, i + hostNameLen);
                    text += subString + '\n';
                    i += hostNameLen;
                }

                Menu.updateMenuText = text;
                Menu.update = true;
            }
        }
        else
        {
            handler.BeginReceive(state.buffer, 0, stateObject.bufferSize, 0, new AsyncCallback(RecieveHosts), state);
        }
    }

    public class stateObject
    {
        public Socket workSocket = null;
        public const int bufferSize = 1024;
        public byte[] buffer = new byte[1024];
        public StringBuilder sb = new StringBuilder();
    }


    static void onRequestConnect(IAsyncResult ar) {
        try
        {
            Socket sender = (Socket)ar.AsyncState;
            string request = "Client<EOF>";
            stateObject state = new stateObject();
            byte[] bytes = new byte[1024];
            bytes = Encoding.ASCII.GetBytes(request);
            sender.Send(bytes);
            state.workSocket = sender;
            sender.BeginReceive(state.buffer, 0, stateObject.bufferSize, 0, new AsyncCallback(RecieveHosts), state);
        }
        catch {
            Menu.updateMenuText = "Unable to establish connection with the server";
            Menu.update = true;
        }
    }


    public static void RequestHosts() {
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAdress = ipHostInfo.AddressList[0];
        IPEndPoint remoteEP = new IPEndPoint(ipAdress, 11000);
        Socket sender = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        sender.BeginConnect(remoteEP, new AsyncCallback(onRequestConnect), sender);   
    }

    static void getConnection(IAsyncResult ar) {
        Socket listener = (Socket)ar.AsyncState;
        Socket connection = listener.EndAccept(ar);
        //Begin multiplayer connection
    }

    public static void startHost(string name) {
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAdress = ipHostInfo.AddressList[0];
        IPEndPoint remoteEP = new IPEndPoint(ipAdress, 11000);
        Socket sender = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        sender.Bind(remoteEP);
        string request = "Host" + name;
        byte[] bytes = new byte[1024];
        bytes = Encoding.ASCII.GetBytes(request);
        sender.Send(bytes);
        sender.Receive(bytes);
        string response = Encoding.ASCII.GetString(bytes);
        if (response == "Valid")
        {
            sender.BeginAccept(new AsyncCallback(getConnection), sender);
        }
        else {
            //Get another host name.
        }
    }
}
