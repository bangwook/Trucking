using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class FirebaseUtilBuilder
{
    public enum Platform
    {
        Google, Amazon
    }

    public const string google = "google-services";
    public const string amazon = "google-services_amazon";

    private static string TargetPath => Path.Combine(Application.streamingAssetsPath, "google-services-desktop.json");

    public static void SetAndroidPlatform(Platform platform)
    {
        var json = platform == Platform.Google ? google : amazon;

        var asset = AssetDatabase.FindAssets(json).FirstOrDefault();
        if (asset != null)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(asset);
            File.Copy(assetPath, TargetPath, true);
            AssetDatabase.ImportAsset(TargetPath);
        }
        else
        {
            Debug.LogError($"not found firebase cfg : {json}");
        }
    }
}