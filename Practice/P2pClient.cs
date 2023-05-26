using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

namespace SK.Assets.Scripts.Practice
{
    public class P2pClient : MonoBehaviour
    {
        // 채팅 UI
        [SerializeField] private Text chatText;
        [SerializeField] private InputField inputField;

        private string strIp = "192.168.0.2";
        private int port = 8082;

        private Socket clientSocket;
        private IPEndPoint iPEndPoint;
        private const int MAX_BUFFER_SIZE = 128;
        private byte[] receiveBuffer, sendBuffer;

        private void Awake()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            iPEndPoint = new IPEndPoint(IPAddress.Parse(strIp), port);

            receiveBuffer = new byte[MAX_BUFFER_SIZE];
            sendBuffer = new byte[MAX_BUFFER_SIZE];
        }

        private void FixedUpdate()
        {
            if (!clientSocket.Connected)
            {
                clientSocket.Connect(iPEndPoint);
                Debug.Log("클라이언트::연결 시도");
            }
        }

        public void OnEndChat(string data)
        {
            Debug.Log("클라이언트::입력 채팅 = " + data);
            inputField.text = String.Empty;

            // 입력 데이터를 다른 피어로 전송
            sendBuffer = Encoding.Default.GetBytes(data);
            clientSocket.BeginSend(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, SendCallback, null);
            clientSocket.BeginReceive(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None, ReceiveCallback, null);
        }

        private void SendCallback(IAsyncResult ar)
        {
            Array.Clear(sendBuffer, 0, sendBuffer.Length);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            chatText.text = Encoding.Default.GetString(receiveBuffer);
            Array.Clear(receiveBuffer, 0, receiveBuffer.Length);

            clientSocket.BeginReceive(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None, ReceiveCallback, null);
        }

        private void SetChatMessage(string message, Socket socket)
        {
            short id = 1001;
            byte[] uid = BitConverter.GetBytes(id); // ID
            byte[] data = Encoding.Default.GetBytes(message); // 전달할 데이터

            // 버퍼에 이어서 복사
            sendBuffer[0] = (byte)sendBuffer.Length; // 패킷 전체 길이
            Array.Copy(uid, 0, sendBuffer, 1, uid.Length);
            Array.Copy(data, 0, sendBuffer, 3, data.Length);

            socket.BeginSend(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, SendCallback, socket);

            // sendBuffer 배열에 있는 데이터를 각각의 데이터로 다시 분리
            /*byte[] _len = new byte[1];
            byte[] _uid = new byte[2];
            byte[] _data = new byte[125];

            Array.Copy(sendBuffer, 0, _len, 0, _len.Length);
            Array.Copy(sendBuffer, 1, _uid, 0, _uid.Length);
            Array.Copy(sendBuffer, 3, _data, 0, _data.Length);

            Debug.Log(BitConverter.ToInt16(_len, 0));
            Debug.Log(BitConverter.ToInt16(_uid, 0));
            Debug.Log(Encoding.Default.GetString(_data));*/
        }
    }
}