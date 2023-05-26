using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace SK
{
    // ����ü�� ������ �� 2����Ʈ ������ �ϸ� ���� �ӵ��� ����
    // �� Ÿ���̱� ������ Stack �޸𸮸� ���
    public struct CharInfo
    {
        // �е�����Ʈ(Padding Byte) : ����ü �� ���� ū ������ ������ ũ�Ⱑ ������, �ش� ����ü�� 2byte ������ �����Ǳ� ������ 6byte�� ��
        // ���� ����� �����Ѵٸ� �е�����Ʈ�� ����ϴ� ���� �߿�
        private byte job; // 1 byte
        public byte gender; // 1 byte
        public byte classtype; // 1 byte
        private ushort age; // 2 byte

        // ������Ƽ
        public byte JOB => job;
        public ushort AGE => age;

        // ����ü �ȿ� �Լ� ���� ����
        public void PrintInfo()
        {
            Debug.Log("Job: " + job);
            Debug.Log("Gender: " + gender);
            Debug.Log("Age: " + age);
        }

        // �б� ���� ������Ƽ�� �� ���� �Լ�
        public void SetJob(byte changeJob)
        {
            job = changeJob;
        }
        public void SetAge(ushort changeAge)
        {
            age = changeAge;
        }
    }
    

    public class StructExample : MonoBehaviour
    {
        CharInfo CHARINFO;
        List<CharInfo> charList;

        private void Awake()
        {
            CHARINFO.SetJob(2);
            CHARINFO.gender = 3;
            CHARINFO.PrintInfo();

            // sizeof������ : ������ ������ ũ�⸦ ��ȯ
            int a = 100;
            Debug.Log(sizeof(int));

            // ����ü�� ũ��� sizeof �� ������ �� ����
            // ����ü�� ũ�⸦ �������� ���ؼ� �ٸ� �Լ��� ����ؾ� ��
            int size = Marshal.SizeOf(CHARINFO);
            Debug.Log("CHARINFO size: " + size);

            charList = new List<CharInfo>();
            // ����ü ����Ʈ
            for (int i = 0; i < 3; i++)
            {
                CharInfo chadata = new CharInfo();
                chadata.SetJob((byte)i);
                charList.Add(chadata);
            }

            // charList[0].AGE = 10; // ����Ʈ ���� ����ü�� �Ϲ����� ���� �Ұ�
            // �ӽ� ������ ����� ����
            CharInfo tmp = charList[0];
            tmp.gender = 0;
        }
    }
}
