using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenCapture : MonoBehaviour
{
    public Camera targetCamera;
    public bool size2ScreenSize;
    public int resWidth;
    public int resHeight;
    
    string _path;

    void Start()
    {
        if (size2ScreenSize)
        {
            resWidth = Screen.width;
            resHeight = Screen.height;
        }
        _path = Application.datapath + "/ScreenShot/";
        Debug.Log(_path);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ClickScreenShot();
        }
    }

    public void ClickScreenShot()
    {
        DirectoryInfo dir = new DirectoryInfo(_path);
        if (!dir.Exists)
        {
            Directory.CreateDirectory(_path);
        }
        string name;
        name = _path + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        targetCamera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        Rect rec = new Rect(0, 0, screenShot.width, screenShot.height);
        targetCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        screenShot.Apply();

        byte[] bytes = screenShot.EncodeToPNG();
        File.WriteAllBytes(name, bytes);
    }
}
