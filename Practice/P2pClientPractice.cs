using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace SK.Practice
{
    public class P2pClientPractice : MonoBehaviour
    {
        private const int MAX_BUFFER_SIZE = 128;

        private Socket sock;
        private string strIP = "192.168.0.2";
        private int port = 8082;
        IPEndPoint ip;

        // 패킷을 저장할 큐
        Queue<byte[]> packetQueue;

        Vector3 vEnd;
        float speed = 2f;

        byte[] sBuffer; // 송신 버퍼
        byte[] rBuffer; // 수신 버퍼

        private void Start()
        {
            // 초기화
            packetQueue = new Queue<byte[]>();
            sBuffer = new byte[MAX_BUFFER_SIZE];
            rBuffer = new byte[MAX_BUFFER_SIZE];

            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ip = new IPEndPoint(IPAddress.Parse(strIP), port);
        }

        private void FixedUpdate()
        {
            if (!sock.Connected) sock.Connect(ip);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
                {
                    vEnd = hitInfo.point;

                    SetMovePacket();
                }
            }

            if (packetQueue.Count > 0)
            {
                byte[] tmp = packetQueue.Dequeue();

                byte[] byLength = new byte[1];
                byte[] byHeader = new byte[2];
                byte[] pos = new byte[4];

                Array.Copy(tmp, 0, byLength, 0, 1);
                Array.Copy(tmp, 1, byHeader, 0, 2);

                short header = BitConverter.ToInt16(byHeader, 0);

                switch (header)
                {
                    case 1001: // 이동 패킷
                        Array.Copy(tmp, 5, pos, 0, 4);
                        vEnd.x = BitConverter.ToInt16(pos, 0);
                        Array.Copy(tmp, 9, pos, 0, 4);
                        vEnd.y = BitConverter.ToInt16(pos, 0);
                        Array.Copy(tmp, 13, pos, 0, 4);
                        vEnd.z = BitConverter.ToInt16(pos, 0);
                        break;
                }
            }

            MovePeer();
        }

        private void MovePeer()
        {
            transform.position = Vector3.MoveTowards(transform.position, vEnd, speed * Time.deltaTime);
        }

        private void SetMovePacket()
        {
            // 목적지 좌표만을 data로 하는 패킷을 제작
            byte[] _packetId = BitConverter.GetBytes(1001);
            byte[] _xPos = BitConverter.GetBytes(vEnd.x);
            byte[] _yPos = BitConverter.GetBytes(vEnd.y);
            byte[] _zPos = BitConverter.GetBytes(vEnd.z);

            sBuffer[0] = 128;
            Array.Copy(_packetId, 0, sBuffer, 1, sBuffer.Length);
            Array.Copy(_xPos, 0, sBuffer, 5, sBuffer.Length);
            Array.Copy(_yPos, 0, sBuffer, 9, sBuffer.Length);
            Array.Copy(_zPos, 0, sBuffer, 13, sBuffer.Length);

            sock.BeginSend(sBuffer, 0, sBuffer.Length, SocketFlags.None, SendCallback, sock);
        }

        private void SendCallback(IAsyncResult ar) 
        {
            byte[] tmp = new byte[128];

            Array.Copy(sBuffer, tmp, sBuffer.Length);
            Array.Clear(sBuffer, 0, sBuffer.Length);

            packetQueue.Enqueue(tmp);

            sock.BeginReceive(rBuffer, 0, rBuffer.Length, SocketFlags.None, ReceiveCallback, sock);
        }

        private void ReceiveCallback(IAsyncResult ar) 
        {
        
        }
    }
}