using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryExample : MonoBehaviour
{
    // Ű���� �������̸� ���� string�� Dictionary
    // Ű���� �ߺ��� �� ����.
    Dictionary<int, string> dic;
    Dictionary<string, int> dic2;

    void Start()
    {
        dic = new Dictionary<int, string>();
        dic2 = new Dictionary<string, int>();

        dic.Add(1, "������");
        dic.Add(2, "ȫ�浿");
        dic.Add(3, "�츮��");
        dic.Add(4, "������2");

        dic2.Add("ȫ�浿", 1);
        dic2.Add("������", 2);
        dic2.Add("��ȿ��", 3);

        Debug.Log(dic[1]);
        Debug.Log(dic[2]);
        Debug.Log(dic[3]);
        Debug.Log(dic[4]);

        Debug.Log(dic2["ȫ�浿"]);
        Debug.Log(dic2["������"]);
        Debug.Log(dic2["��ȿ��"]);

        // �Ʒ��� �ڵ�� Ű���� 1�� ���� ������ ��� str �̶�� ������ �����ϴ� �ڵ��̴�.
        string result = string.Empty;
        if( dic.TryGetValue( 1, out result))
        {
            Debug.Log(result);
        }

        result = string.Empty;
        if ( dic.TryGetValue( 2, out result))
        {
            Debug.Log(result);
        }

        // �Ʒ��� �ڵ�� Ű���� ȫ�浿�̸� ���� ������ ��� iResult�� ����ȴ�.
        int iResult;
        if( dic2.TryGetValue("ȫ�浿", out iResult))
        {
            Debug.Log(iResult);
        }

        // dic ����� �ȿ� ����� ��� Ű���� ���� ���
        foreach( KeyValuePair<int, string> one in dic )
        {
            Debug.Log("Ű = " + one.Key + " �� =" + one.Value);
        }
        
    }

    void Update()
    {
        
    }
}
