using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SK
{
    public class P2pServer : MonoBehaviour
    {
        private const int MAX_BUFFER_SIZE = 128;

        private string strIp = "192.168.0.2";
        private int port = 8082;

        private Socket serverSocket;

        private byte[] receiveBuffer, sendBuffer;

        private void Awake()
        {
            // 소켓 생성
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(strIp), port);
            // 버퍼 초기화
            receiveBuffer = new byte[MAX_BUFFER_SIZE];
            sendBuffer = new byte[MAX_BUFFER_SIZE];

            // Bind
            serverSocket.Bind(iPEndPoint);

            // Listen
            serverSocket.Listen(1000);
        }

        private void FixedUpdate()
        {
            // Accept
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket userSock = (Socket)serverSocket.EndAccept(ar);
            Debug.Log("서버::유저가 접속함");

            userSock.BeginReceive(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None, ReceiveCallback, userSock);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket userSock = (Socket)ar.AsyncState;

            Debug.Log("서버::받은 데이터를 유저에게 보냄");
            userSock.BeginSend(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None, SendCallback, userSock);

            Array.Clear(receiveBuffer, 0, receiveBuffer.Length);
        }

        private void SendCallback(IAsyncResult ar)
        {
            Socket userSock = (Socket)ar.AsyncState;

            userSock.BeginReceive(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None, ReceiveCallback, userSock);

            Array.Clear(sendBuffer, 0, sendBuffer.Length);
        }
    }
}
