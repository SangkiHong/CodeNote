using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public List<Vector3> list;
    int curIndex;
    Vector3 vEnd;
    float speed;

    void Start()
    {
        vEnd = transform.position;
        speed = 2f;
    }

    void Update()
    {
        //1. 배열의 첫번째 원소를 목표지점으로 설정한다.
        //   1-1. 배열의 마직막 원소 다음은 배열의 첫번째 원소
        //2. 목표지점까지 이동한후 목표지점을 다음의 배열원소로 설정
        if (transform.position == vEnd)
        {
            ++curIndex;
            if (curIndex >= list.Count)
            {
                curIndex = 0;
            }
            vEnd = list[curIndex];
        }
        transform.position = 
            Vector3.MoveTowards(transform.position, vEnd, Time.deltaTime * speed);
    }
}
