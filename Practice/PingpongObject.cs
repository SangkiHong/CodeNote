using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingpongObject : MonoBehaviour
{
    public Vector3 vStart;
    public Vector3 vEnd;

    void Start()
    {
        transform.position = vStart;
    }

    void Update()
    {
        //1. ������ ��ǥ�������� �̵�
        //2. ��ǥ�������� �����ߴٸ� �������� ��ǥ������ ����
        //3. 1,2�� ������ �ݺ�

        if (transform.position == vEnd)
        {
            VectorHelper.Vector3Swap(ref vStart, ref vEnd);
            //Vector3 tmp = vStart;
            //vStart = vEnd;
            //vEnd = tmp;
        }
        

        transform.position =
            Vector3.MoveTowards(transform.position, vEnd, Time.deltaTime * 30f);
    }
}
