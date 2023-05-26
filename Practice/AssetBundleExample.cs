using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace SK.Practice
{
    public class AssetBundleExample : MonoBehaviour
    {
        [SerializeField] private string fileName;

        private void Start()
        {
            StartCoroutine(LoadBundle(fileName));
        }

        IEnumerator LoadBundle(string _fileName)
        {
            string url = "file:///" + Application.dataPath + "/AssetBundles/" + _fileName + ".assetbundle";
            UnityWebRequest uWrequest = UnityWebRequestAssetBundle.GetAssetBundle(url);
            yield return uWrequest.SendWebRequest();

            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uWrequest);
            
            // 각각의 파일을 로드
            /*var prefab = bundle.LoadAsset<GameObject>(_fileName);
            GameObject obj = Instantiate(prefab);
            obj.transform.position = Vector3.zero;*/

            // 번들 내 모든 오브젝트를 로드
            GameObject[] prefabs = bundle.LoadAllAssets<GameObject>();
            for (int i = 0; i < prefabs.Length; i++)
            {
                GameObject obj = Instantiate(prefabs[i]);
                obj.transform.position = Vector3.left * i;
            }
        }
    }
}