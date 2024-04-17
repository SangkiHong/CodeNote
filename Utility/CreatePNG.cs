using UnityEngine;
using System.IO;
using System.Collections;

namespace ProjectD
{
    public class CreatePNG : MonoBehaviour
    {
        public Camera targetCamera;       //보여지는 카메라.

        [SerializeField] private GameObject[] targetObjects;

        public int resWidth;
        public int resHeight;
        public TextureFormat textureFormat;
        string path;

        [ContextMenu("Create PNG")]
        public void Create()
            => StartCoroutine(CreateCoroutine());

        public IEnumerator CreateCoroutine()
        {
            for (int i = 0; i < targetObjects.Length; i++)
                targetObjects[i].SetActive(false);

            path = Application.dataPath + "/ScreenShot/";

            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                Directory.CreateDirectory(path);
            }

            for (int i = 0; i < targetObjects.Length; i++)
            {
                if (i > 0) targetObjects[i - 1].SetActive(false);

                targetObjects[i].SetActive(true);

                string name;
                name = path + targetObjects[i].name + ".png";

                RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
                targetCamera.targetTexture = rt;
                Texture2D screenShot = new Texture2D(resWidth, resHeight, textureFormat, false);

                targetCamera.Render();
                RenderTexture.active = rt;

                screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
                screenShot.Apply();

                byte[] bytes = screenShot.EncodeToPNG();
                File.WriteAllBytes(name, bytes);

                yield return new WaitForEndOfFrame();
                
                targetObjects[i].SetActive(false);
            }
        }
    }
}
