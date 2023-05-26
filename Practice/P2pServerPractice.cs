using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using UnityEngine;

namespace SK.Practice
{
    /*public struct PEERINFO
    {
        public int id;
        public string name;
        public byte charType;
        public Socket p2pUser;
        public Vector3 vEnd;
    }*/

    public class P2pServerPractice : MonoBehaviour
    {
        private const int MAX_BUFFER_SIZE = 128;

        private Socket sock;
        private string strIP = "192.168.0.2";
        private int port = 8082;

        private int serverHandle;

        // 패킷을 저장할 큐
        Queue<byte[]> packetQueue;

        // 피어 정보
        //PEERINFO peerInfo;
        Vector3 otherPeerPos;
        public GameObject otherPeer;
        float speed = 2f;

        byte[] sBuffer; // 송신 버퍼
        byte[] rBuffer; // 수신 버퍼

        private void Awake()
        {
            // 초기화
            packetQueue = new Queue<byte[]>();
            sBuffer = new byte[MAX_BUFFER_SIZE];
            rBuffer = new byte[MAX_BUFFER_SIZE];
            otherPeerPos = Vector3.zero;

            // 소켓 생성
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(strIP), port);

            // 서버 핸들 저장
            serverHandle = sock.Handle.ToInt32();

            // Bind
            sock.Bind(ip);

            // Listen
            sock.Listen(1000);

            // Accept
            sock.BeginAccept(AddUser, null);
        }

        private void AddUser(IAsyncResult ar)
        {
            sock = sock.EndAccept(ar);

            // 피어 정보 생성
            //peerInfo = new PEERINFO();
            //peerInfo.id = sock.Handle.ToInt32();

            // SendCallback 함수가 호출된 후 / 자신이 보낸 패킷을 큐에 저장 / 콜백함수 소켓의 Receive 함수 호출
            sock.BeginReceive(rBuffer, 0, rBuffer.Length, SocketFlags.None, ReceiveCallback, sock);
        }

        public void SendCallback(IAsyncResult ar)
        {
            byte[] tmp = new byte[128];

            Array.Copy(sBuffer, tmp, sBuffer.Length);
            Array.Clear(sBuffer, 0, sBuffer.Length);

            packetQueue.Enqueue(tmp);

            sock.BeginReceive(rBuffer, 0, rBuffer.Length, SocketFlags.None, ReceiveCallback, sock);
        }

        public void ReceiveCallback(IAsyncResult ar)
        {
            byte[] tmp = new byte[128];

            Array.Copy(rBuffer, tmp, rBuffer.Length);
            Array.Clear(rBuffer, 0, rBuffer.Length);

            packetQueue.Enqueue(tmp);
            sock.BeginReceive(rBuffer, 0, rBuffer.Length, SocketFlags.None, ReceiveCallback, sock);
        }

        public void MoveOtherPeer()
        {
            otherPeer.transform.position = Vector3.MoveTowards(otherPeer.transform.position, otherPeerPos, Time.deltaTime * speed);
        }

        private void Update()
        {
            // DeQueue 하여 패킷 내용 처리
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
                    case 1001:
                        Array.Copy(tmp, 5, pos, 0, 4);
                        otherPeerPos.x = BitConverter.ToInt16(pos, 0);
                        Array.Copy(tmp, 9, pos, 0, 4);
                        otherPeerPos.y = BitConverter.ToInt16(pos, 0);
                        Array.Copy(tmp, 13, pos, 0, 4);
                        otherPeerPos.z = BitConverter.ToInt16(pos, 0);
                        break;
                }

                // 나의 이동함수 호출
                // 다른 피어의 이동함수 호출
                /*for ()
                {
                    // 이동함수 호출
                }*/
            }

            MoveOtherPeer();
        }
    }
}