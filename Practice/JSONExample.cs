using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

namespace SK.Practice
{
    public class JSONExample : MonoBehaviour
    {
        private void Start()
        {
            TextAsset jsonAsset = Resources.Load<TextAsset>("JSON/JsonExample");
            JSONNode root = JSON.Parse(jsonAsset.text);
            
            // StringŸ�� ��带 ���
            Debug.Log(root["CharacterName"].Value);

            // �迭 ��带 ���
            for (int i = 0; i < root["Test"].Count; i++)
            {
                Debug.Log(root["Test"][i].Value);
            }

            TextAsset jsonAsset2 = Resources.Load<TextAsset>("JSON/JsonExample_2");
            JSONNode root2 = JSON.Parse(jsonAsset2.text);

            JSONNode arrMonsterInfo = root2["MonsterInfo"];
            for (int i = 0; i < arrMonsterInfo.Count; i++)
            {
                Debug.Log(arrMonsterInfo[i]["name"].Value);
                Debug.Log(arrMonsterInfo[i]["age"].Value);
            }
        }
    }
}
