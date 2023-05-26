using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK.Practice
{
    public class StackExample : MonoBehaviour
    {
        int[] stack;
        int top;
        int stackLengh = 10;

        private void Start()
        {
            stack = new int[stackLengh]; // 배열 길이 초기화
            top = 0;
        }

        private void Push(int value)
        {
            if (top < stackLengh)
            {
                stack[top++] = value;
            }
            else
            {
                Debug.LogError("스택이 꽉찼습니다.");
            }
        }

        private int? Pop()
        {
            if (stack.Length > 0)
            { 
                return stack[top--]; 
            }
            else
            {
                Debug.LogError("스택이 비었습니다.");
                return null;
            }
        }

        private bool Contain(int value)
        {
            if (stack.Length > 0)
            {
                for (int i = 0; i < stack.Length; i++)
                {
                    if (stack[i] == value)
                        return true;
                }
            }
            return false;
        }
    }
}