using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

public class NetworkClient : MonoBehaviour {

	// Use this for initialization
	void Start () {
        hostServer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Client() {
        Debug.Log("Checking server for hosts");
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAdress = ipHostInfo.AddressList[0];
        IPEndPoint remoteEP = new IPEndPoint(ipAdress, 11000);
        IPEndPoint localEP = new IPEndPoint(ipAdress, 11001);
        Socket sender = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        //localEP = sender.LocalEndPoint as IPEndPoint;
        try
        {
            sender.Connect(remoteEP);
            Debug.Log("Connected to server");
            byte[] msg = Encoding.ASCII.GetBytes("Client<EOF>");
            sender.Send(msg);
            byte[] response = new byte[1024];
            string host = null;
            while (true)
            {
                response = new byte[1024];
                int bytesRec = sender.Receive(response);
                host += Encoding.ASCII.GetString(response);
                if (host.IndexOf("<EOF>") > -1)
                {
                    break;
                }
            }
            Debug.Log("Host Get: " + host.Replace("<EOF>",""));

            msg = Encoding.ASCII.GetBytes("Connect" + host);
            Socket reSend = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            reSend.Connect(remoteEP);
            reSend.Send(msg);
            host = null;
            while (true)
            {
                response = new byte[1024];
                int bytesRec = reSend.Receive(response);
                host += Encoding.ASCII.GetString(response);
                if (host.IndexOf("<EOF>") > -1)
                {
                    break;
                }
            }
            host= host.Replace("<EOF>", "");
            string ip = host.Substring(0, host.IndexOf('@'));
            string port = host.Substring(host.IndexOf('@')+1, host.Length - host.IndexOf('@')-1);
            int portNum = Convert.ToInt32(port);
            IPAddress address = IPAddress.Parse(ip);
            IPEndPoint hostEnd = new IPEndPoint(address, portNum);
            Socket hostSocket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            hostSocket.Connect(hostEnd);
            string toHost = "Hi host!";
            response = Encoding.ASCII.GetBytes(toHost);
            hostSocket.Send(response);
            hostSocket.Close();
            sender.Close();
        }
        catch { }

    }

    void hostServer() {
        byte[] bytes = new byte[1024];
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAdress = ipHostInfo.AddressList[0];
        IPEndPoint remoteEP = new IPEndPoint(ipAdress, 11000);
        Socket sender = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            sender.Connect(remoteEP);

            byte[] msg = Encoding.ASCII.GetBytes("Connected to game<EOF>");

            msg = Encoding.ASCII.GetBytes("HostTest Host<EOF>");
            sender.Send(msg);
            Debug.Log("Waiting For Response");

            Socket listener = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(sender.LocalEndPoint);
            listener.Listen(10);
            listener.Accept();
            Debug.Log("GOT IT!");
        }
        catch { }
    }
}
