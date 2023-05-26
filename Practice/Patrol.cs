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
        //1. �迭�� ù��° ���Ҹ� ��ǥ�������� �����Ѵ�.
        //   1-1. �迭�� ������ ���� ������ �迭�� ù��° ����
        //2. ��ǥ�������� �̵����� ��ǥ������ ������ �迭���ҷ� ����
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
