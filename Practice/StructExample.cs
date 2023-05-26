using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace SK
{
    // 구조체를 정의할 때 2바이트 단위로 하면 연산 속도가 빠름
    // 값 타입이기 때문에 Stack 메모리를 사용
    public struct CharInfo
    {
        // 패딩바이트(Padding Byte) : 구조체 내 가장 큰 데이터 단위로 크기가 정해짐, 해당 구조체는 2byte 단위로 결정되기 때문에 6byte가 됨
        // 서버 통신을 감안한다면 패딩바이트를 고려하는 것이 중요
        private byte job; // 1 byte
        public byte gender; // 1 byte
        public byte classtype; // 1 byte
        private ushort age; // 2 byte

        // 프로퍼티
        public byte JOB => job;
        public ushort AGE => age;

        // 구조체 안에 함수 선언도 가능
        public void PrintInfo()
        {
            Debug.Log("Job: " + job);
            Debug.Log("Gender: " + gender);
            Debug.Log("Age: " + age);
        }

        // 읽기 전용 프로퍼티의 값 변경 함수
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

            // sizeof연산자 : 데이터 형식의 크기를 반환
            int a = 100;
            Debug.Log(sizeof(int));

            // 구조체의 크기는 sizeof 로 가져올 수 없음
            // 구조체의 크기를 가져오기 위해서 다른 함수를 사용해야 함
            int size = Marshal.SizeOf(CHARINFO);
            Debug.Log("CHARINFO size: " + size);

            charList = new List<CharInfo>();
            // 구조체 리스트
            for (int i = 0; i < 3; i++)
            {
                CharInfo chadata = new CharInfo();
                chadata.SetJob((byte)i);
                charList.Add(chadata);
            }

            // charList[0].AGE = 10; // 리스트 내의 구조체는 일반적인 변경 불가
            // 임시 변수를 만들어 변경
            CharInfo tmp = charList[0];
            tmp.gender = 0;
        }
    }
}
