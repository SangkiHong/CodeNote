using UnityEngine;

public static class HUtility
{
    public static int maxResolutionX = 1024;
    public static int maxResolutionY = 1024;

    // Ref URL: https://discussions.unity.com/t/limit-www-texture-file-size/99343/2
    public static Texture2D ResizeTexture(Texture2D targetTexture)
    {
        Texture2D resizeTexture = targetTexture;

        if (targetTexture.width > maxResolutionX || targetTexture.height > maxResolutionY)
        {
            float widthRatio = (float)maxResolutionX / targetTexture.width;
            float heightRatio = (float)maxResolutionY / targetTexture.height;

            if (widthRatio < heightRatio)
            {
                resizeTexture = Resize(targetTexture, (int)(targetTexture.width * widthRatio), (int)(targetTexture.height * widthRatio));
            }
            else
            {
                resizeTexture = Resize(targetTexture, (int)(targetTexture.width * heightRatio), (int)(targetTexture.height * heightRatio));
            }
        }

        return resizeTexture;
    }

    private static Texture2D Resize(Texture2D source, int width, int height)
    {
        RenderTexture rt = new RenderTexture(width, height, 32);
        RenderTexture.active = rt;

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, width, height, 0);
        Graphics.DrawTexture(new Rect(0, 0, width, height), source);
        GL.PopMatrix();

        Texture2D dest = new Texture2D(width, height);
        dest.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        dest.Apply();

        RenderTexture.active = null;
        return dest;
    }
}
