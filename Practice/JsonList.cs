using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;

[Serializable]
public class Mob
{
    [SerializeField] string name;
    public string NAME
    {
        get { return name;  }
        set { name = value; }
    }

    [SerializeField] int age;
    public int AGE
    {
        get { return age; }
        set { age = value; }
    }
}

public class Serialization<T>
{
    [SerializeField]
    List<T> _t;

    public List<T> ToList() { return _t; }
    public Serialization(List<T> _tmp)
    {
        _t = _tmp;
    }
}

public class JsonList : MonoBehaviour
{
    List<Mob> list;

    void Start()
    {
        list = new List<Mob>();

        // 파일을 읽어 들인것 /////////////////////
        TextAsset mobJson = Resources.Load<TextAsset>("Json/JsonExample_3");
        JSONNode root = JSON.Parse(mobJson.text);

        JSONNode arrMonsterInfo = root["MonsterInfo"];
        for (int i = 0; i < arrMonsterInfo.Count; i++)
        {
            Mob tmp = new Mob();
            tmp.NAME = arrMonsterInfo[i]["name"].Value;
            tmp.AGE = byte.Parse(arrMonsterInfo[i]["age"].Value);
            list.Add(tmp);
        }

        //// 리스트에 저장된 모든 원소 출력 ///////////////////////////
        foreach( Mob mob in list )
        {
            Debug.Log(mob.NAME);
            Debug.Log(mob.AGE);
        }

        // 리스트에 저장된 데이터를 Json으로 변환
        // 직렬화(Serialization)
        // 개체(Object)를 저장하거나 메모리(Memory), 데이터 베이스(Database) 또는 파일(File)로 전송하기 위한 개체를
        // 바이트 스트림으로 변환하는 프로세스로서 저장이나 전송후 다시 개체로 만들기 위해서 사용한다.

        Serialization<Mob> Instance = new Serialization<Mob>(list);
        string jsonDataList = JsonUtility.ToJson(Instance);
        Debug.Log(jsonDataList);

        // Json데이터(문자열)를 리스트로 변환해서 저장
        List<Mob> moblist = JsonUtility.FromJson<Serialization<Mob>>(jsonDataList).ToList();
        for(int i = 0; i < moblist.Count; i++)
        {
            Debug.Log(moblist[i].NAME);
            Debug.Log(moblist[i].AGE);
        }
    }

    void Update()
    {
        
    }
}
