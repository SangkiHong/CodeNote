using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundleBuild
{
    public static int MAX_FILE_COUNT = 30;
    public static int MAX_FILE_SIZE = 10;

    [MenuItem("AssetBundle/Build")]
    public static void AssetBundleBuild()
    {
        long maxSize = MAX_FILE_SIZE * (1024 * 1024);  
        string assetPath = Application.dataPath;

        ClearAllBundleNames();

        // Select Prefab file's Directory
        string prefabDirectory = EditorUtility.OpenFolderPanel("Select  Prefab file's Directory", "./Assets", "");
        //string prefabDirectory = Path.Combine(assetPath, "03.Prefabs");

        if (string.IsNullOrEmpty(prefabDirectory) == false)
        {
            if (!Directory.Exists(prefabDirectory))
                Directory.CreateDirectory(prefabDirectory);

            string searchPattern = "*.prefab";
            string[] allFiles = Directory.GetFiles(prefabDirectory, searchPattern, SearchOption.AllDirectories);

            long sumFileSize = 0;
            int bundleIndex = 1;
            int assetCount = 0;

            assetPath = assetPath.Replace("Assets", string.Empty);
            Debug.Log(assetPath);

            foreach (var file in allFiles)
            {
                FileInfo info = new FileInfo(file);
                long fileSize = info.Length;
                Debug.Log($"(BundleIndex : {bundleIndex}), (Name : {info.Name}), (Size : {fileSize})");

                if ((sumFileSize + fileSize) > maxSize || (assetCount + 1) > MAX_FILE_COUNT)
                {
                    bundleIndex++;
                    sumFileSize = 0;
                    assetCount = 0;
                }

                assetCount++;
                sumFileSize += fileSize;

                string rePath = file.Replace(assetPath, string.Empty);
                AssetImporter asset = AssetImporter.GetAtPath(rePath);
               
                if (prefabDirectory.ToLower().Contains("part"))
                {
                    asset.assetBundleName = $"part_{bundleIndex}";
                }
                else
                {
                    asset.assetBundleName = "tool";
                }
            }




            // Export
            string exportDirectory = EditorUtility.OpenFolderPanel("Select Export Bundle Directory", "./Bundle", "");

            if (!Directory.Exists(exportDirectory))
                Directory.CreateDirectory(exportDirectory);

            BuildPipeline.BuildAssetBundles(exportDirectory, BuildAssetBundleOptions.None, BuildTarget.WSAPlayer); 
        }
    }


    private static void ClearAllBundleNames()
    {
        string assetPath = Application.dataPath;

        // Fix target bundle object's directory
        string prefabDirectory = Path.Combine(assetPath, "03.Prefabs");

        if (Directory.Exists(prefabDirectory))
        {
            string searchPattern = "*.prefab";
            string[] allFiles = Directory.GetFiles(prefabDirectory, searchPattern, SearchOption.AllDirectories);

            assetPath = assetPath.Replace("Assets", string.Empty);
            foreach (var file in allFiles)
            {
                string rePath = file.Replace(assetPath, string.Empty);
                AssetImporter asset = AssetImporter.GetAtPath(rePath);
                asset.assetBundleName = string.Empty;
            }
        }
    }
}
