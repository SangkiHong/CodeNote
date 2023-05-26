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

    // �Ű������� ���޹��� item�� �迭�� ����
    public void enQueue(int item)
    {
        if (IsFull())
        {
            Debug.Log("���̻� �����͸� �߰��� �� �����ϴ�.");
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

    // �迭���� ���Ҹ� ��������
    public int deQueue()
    {
        if(IsEmpty())
        {
            Debug.Log("���̻� �����Ͱ� �������� �ʽ��ϴ�.");
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
            Debug.Log("Queue�� ����ֽ��ϴ�.");
            return true;
        }
        return false;
    }

    public void Peek()
    {

    }
}
