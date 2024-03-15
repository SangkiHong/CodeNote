
public int maxResolutionX = 1024;
public int maxResolutionY = 1024;

IEnumerator GetTexture(string path)
{
    if (string.IsNullOrEmpty(path))
    {
        Debug.LogError($"SetImage::Path Error - path : {path}");
        yield break;
    }

    using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
    {
        yield return uwr.SendWebRequest();

        Texture2D newTexture;

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"SetImage::Error - path: {path}, " + uwr.error);

            newTexture = defaultTexture;
        }
        else
        {
            newTexture = DownloadHandlerTexture.GetContent(uwr);

            // Limit TextureSize & Resize
            // Ref URL: https://discussions.unity.com/t/limit-www-texture-file-size/99343/2
#if UNITY_EDITOR
            //Debug.Log($"Width: {newTexture.width}, Height: {newTexture.height}");
#endif
            // resize the newTexture to fit in the desired bounds
            if (newTexture.width > maxResolutionX || newTexture.height > maxResolutionY)
            {
                float widthRatio = (float)maxResolutionX / newTexture.width;
                float heightRatio = (float)maxResolutionY / newTexture.height;
                if (widthRatio < heightRatio)
                {
                    newTexture = Resize(newTexture, (int)(newTexture.width * widthRatio), (int)(newTexture.height * widthRatio));
                }
                else
                {
                    newTexture = Resize(newTexture, (int)(newTexture.width * heightRatio), (int)(newTexture.height * heightRatio));
                }
            }
        }

        uwr.Dispose();
    }
}

// returns a texture resized to the given dimensions
public Texture2D Resize(Texture2D source, int width, int height)
{
    // initialize render texture with target width and height
    RenderTexture rt = new RenderTexture(width, height, 32);
    // set as the active render texture
    RenderTexture.active = rt;
    // render source texture to the render texture
    GL.PushMatrix();
    GL.LoadPixelMatrix(0, width, height, 0);
    Graphics.DrawTexture(new Rect(0, 0, width, height), source);
    GL.PopMatrix();
    // initialize destination texture with target width and height
    Texture2D dest = new Texture2D(width, height);
    // copy render texture to the destination texture
    dest.ReadPixels(new Rect(0, 0, width, height), 0, 0);
    dest.Apply();
    // resume rendering to the main window
    RenderTexture.active = null;
    DestroyImmediate(source, true);
    return dest;
}
