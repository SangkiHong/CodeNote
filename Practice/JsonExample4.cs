using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;

namespace SK.Practice
{
    /*
     * 직렬화는 개체를 저장하거나 메모리, 데이터 베이스 또는 파일로 전송하기 위해서 개체를
     * 바이트 스트림으로 변환하는 프로세스를 의미한다.
     * Serializable, SerializeField
     */

    [Serializable]
    public class StageData
    {
        [SerializeField] public string score;
        [SerializeField] public string[] arrData;
    }

    [Serializable]
    public class Serialization<T>
    {
        [SerializeField] List<T> _t;

        public List<T> ToList() { return _t; }
        public Serialization(List<T> t) { _t = t; }
    }

    public class JsonExample4 : MonoBehaviour
    {
        [SerializeField]
        private List<StageData> list;

        void Start()
        {
            list = new List<StageData>();

            TextAsset textAsset = Resources.Load<TextAsset>("Json/JsonExample_4");
            JSONNode root = JSON.Parse(textAsset.text);
            StageData tempData = null;
            JSONNode tempNode = null;

            for (int i = 0; i < root.Count; i++)
            {
                tempData = new StageData();
                tempNode = root[i.ToString()];

                tempData.score = tempNode["Score"];

                int dataSize = tempNode["arr"].Count;
                tempData.arrData = new string[dataSize];

                for (int j = 0; j < tempNode["arr"].Count; j++)
                {
                    tempData.arrData[j] = tempNode["arr"][j];
                }
                list.Add(tempData);
            }

            // 리스트에 저장된 원소를 Json데이터로 변환하려면
            Serialization<StageData> tempInst = new Serialization<StageData>(list);
            string jsonDataList = JsonUtility.ToJson(tempInst);
            Debug.Log(jsonDataList);
        }
    }
}