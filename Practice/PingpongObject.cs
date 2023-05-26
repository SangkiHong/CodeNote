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
        //1. 설정한 목표지점까지 이동
        //2. 목표지점까지 도달했다면 시작점과 목표지점을 변경
        //3. 1,2번 과정이 반복

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
