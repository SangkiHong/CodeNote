using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

namespace SK.Practice
{
    public class FieldOffsetExample : MonoBehaviour
    {
        [StructLayout(LayoutKind.Explicit)]
        struct CustomData
        {
            // 8 Byte 데이터에 여러 데이터를 넣음
            [FieldOffset(0)] public long data;
            [FieldOffset(0)] public byte hp;
            [FieldOffset(1)] public byte mp;
            [FieldOffset(2)] public byte con;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct XDATA
        {
            char a;
            char b;
            char c;
        }

        private void Start()
        {
            CustomData myData = new CustomData();
            myData.hp = 100;
            myData.mp = 50;
            myData.con = 10;

            CustomData otherData = new CustomData();
            // long 데이터만을 전달함으로 전체 데이터를 전달할 수 있음
            otherData.data = myData.data;

            XDATA xData = new XDATA();

            // 구조체의 크기는 sizeof 연산자를 통해 구할 수 없음
            //Debug.Log(sizeof(xData));

            // Marshal의 멤버함수를 통해 구조체의 사이즈를 구함
            Debug.Log(Marshal.SizeOf(xData));

            // 바이트 패딩이란 저장구조형식에 맞추어서 데이터를 읽기 위해서 바이트에 패딩을 추가한 것
            // 구조체 내 데이터 형식 중 사이즈가 가장 큰 데이터의 배수로 패딩이 생성됨
            // 구조체 크기가 상이한 이유는 패딩바이트가 추가 되었기 때문이다.
        }
    }
}