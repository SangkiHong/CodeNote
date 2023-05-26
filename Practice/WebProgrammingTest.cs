using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace SK.Test
{
    public class WebProgrammingTest : MonoBehaviour
    {
        private string directory_PatchInfo = "D:/Game/PatchInfo";
        private string directory_Source = "D:/Game/PatchInfo/Source";
        private string directory_Download = "D:/Game/DownLoad";
        private string directory_DownloadPatch = "D:/Game/DownLoad/Patch";

        private void Start()
        {
            StartCoroutine(DownLoadPatchList("DownLoadList.csv"));
        }

        IEnumerator DownLoadPatchList(string _fileName)
        {
            string url = Path.Combine(directory_PatchInfo, _fileName);
            UnityWebRequest uWrequest = UnityWebRequestAssetBundle.GetAssetBundle(url);
            yield return uWrequest.SendWebRequest();

            // 에셋을 저장할 경로의 폴더가 존재하지 않는다면 생성
            if (!Directory.Exists(directory_Download))
                Directory.CreateDirectory(directory_Download);

            // 파일 입출력을 통해 받아온 에셋을 저장
            FileStream fs = new FileStream(Path.Combine(directory_Download, _fileName), FileMode.Create);
            fs.Write(uWrequest.downloadHandler.data, 0, (int)uWrequest.downloadedBytes);
            fs.Close();

            yield return ReadPatchList(_fileName);
        }

        IEnumerator ReadPatchList(string _fileName)
        {
            string path = Path.Combine(Path.Combine(directory_Download, _fileName));

            // 에셋을 패치할 경로의 폴더가 존재하지 않는다면 생성
            if (!Directory.Exists(directory_Source))
                Directory.CreateDirectory(directory_Source);

            using (StreamReader sr = new StreamReader(path))
            {
                string line = string.Empty;
                string url = string.Empty;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] row = line.Split(new char[] { ',' });
                    if (row[0] != "index")
                    {
                        url = Path.Combine(directory_Source, row[2]);
                        
                        // 해당 url의 파일을 다운로드
                        yield return DownLoadPatchFile(url);
                    }
                }
            }

            yield return null;
        }

        IEnumerator DownLoadPatchFile(string sourceURL)
        {
            // 에셋을 패치할 경로의 폴더가 존재하지 않는다면 생성
            if (!Directory.Exists(directory_DownloadPatch))
                Directory.CreateDirectory(directory_DownloadPatch);

            UnityWebRequest uWrequest = UnityWebRequestAssetBundle.GetAssetBundle(sourceURL);
            yield return uWrequest.SendWebRequest();

            // 파일 스트림 생성
            FileStream fs = new FileStream(Path.Combine(directory_DownloadPatch), FileMode.Create);
            fs.Write(uWrequest.downloadHandler.data, 0, (int)uWrequest.downloadedBytes);
            fs.Close();
            yield return null;
        }
    }
}