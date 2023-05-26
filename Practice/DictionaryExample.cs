using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryExample : MonoBehaviour
{
    // 키값은 정수형이며 값은 string인 Dictionary
    // 키값은 중복될 수 없다.
    Dictionary<int, string> dic;
    Dictionary<string, int> dic2;

    void Start()
    {
        dic = new Dictionary<int, string>();
        dic2 = new Dictionary<string, int>();

        dic.Add(1, "가나다");
        dic.Add(2, "홍길동");
        dic.Add(3, "우리집");
        dic.Add(4, "가나다2");

        dic2.Add("홍길동", 1);
        dic2.Add("강감찬", 2);
        dic2.Add("이효리", 3);

        Debug.Log(dic[1]);
        Debug.Log(dic[2]);
        Debug.Log(dic[3]);
        Debug.Log(dic[4]);

        Debug.Log(dic2["홍길동"]);
        Debug.Log(dic2["강감찬"]);
        Debug.Log(dic2["이효리"]);

        // 아래의 코드는 키값이 1이 값이 존재할 경우 str 이라는 변수에 저장하는 코드이다.
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

        // 아래의 코드는 키값이 홍길동이며 값이 존재할 경우 iResult에 저장된다.
        int iResult;
        if( dic2.TryGetValue("홍길동", out iResult))
        {
            Debug.Log(iResult);
        }

        // dic 저장소 안에 저장된 모든 키값과 값을 출력
        foreach( KeyValuePair<int, string> one in dic )
        {
            Debug.Log("키 = " + one.Key + " 값 =" + one.Value);
        }
        
    }

    void Update()
    {
        
    }
}
