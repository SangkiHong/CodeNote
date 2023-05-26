using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueExample
{
    int[] queue;
    public int front;
    public int rear;

    public void CreateQueue(int _size)
    {
        queue = new int[_size];
        front = -1;
        rear = -1;
    }

    // 매개변수로 전달받은 item을 배열에 저장
    public void enQueue(int item)
    {
        if (IsFull())
        {
            Debug.Log("더이상 데이터를 추가할 수 없습니다.");
            return;
        }
        queue[++rear] = item;
    }

    public void DisplayData()
    {
        for(int i = front+1; i < queue.Length; i++)
        {
            Debug.Log(queue[i]);
        }
    }

    // 배열에서 원소를 내보낸다
    public int deQueue()
    {
        if(IsEmpty())
        {
            Debug.Log("더이상 데이터가 존재하지 않습니다.");
            return -1;
        }
        return queue[++front];
    }

    public bool IsFull()
    {
        if(rear == queue.Length - 1)
        {
            return true;
        }
        return false;
    }

    public bool IsEmpty()
    {
        if(rear == front)
        {
            Debug.Log("Queue가 비어있습니다.");
            return true;
        }
        return false;
    }

    public void Peek()
    {

    }
}
