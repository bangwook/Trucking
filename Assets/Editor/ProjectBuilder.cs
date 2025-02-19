using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using I2.Loc;
using LunarConsolePlugin;
using LunarConsolePluginInternal;
using Tayx.Graphy;
using UnityEditor.Purchasing;
using UnityEngine.Purchasing;

public class ProjectBuilder
{
    private static string[] SCENES = FindEnabledEditorScenes();
    private static string APP_NAME = "Transport Inc.";
    private static string TARGET_DIR = "Build";
    private static string buildIdentifier_google = "cookappsplay.pocket.truck.tycoon.idle.manage.game";
    private static string buildIdentifier_amazon = "com.cookappsplay.truckeramazon";

    private static string buildIdentifier_ios = "com.cookappsplay.trucker";
//    private static string keystorePath = "trucking.keystore";
//    private static string keystorePass = "cook1234";
//    private static string keyaliasName = "trucking";
//    private static string keyaliasPass = "cook1234";

    private static string keystorePath = "cookappsplay.keystore";
    private static string keystorePass = "!znrdoqtmvmffpdl";
    private static string keyaliasName = "cookappsplay";
    private static string keyaliasPass = "!znrdoqtmvmffpdl";

    private static void SetDefaultSetting()
    {
        PlayerSettings.bundleVersion = Trucking.Common.Trucking.GetVersionString();
        PlayerSettings.Android.bundleVersionCode = 100000 + Trucking.Common.Trucking.VERSION_CODE;
        PlayerSettings.statusBarHidden = true;
        PlayerSettings.productName = APP_NAME;
    }

    [MenuItem("Trucking/Set Android")]
    public static void PerformAndroid()
    {
        SetDefaultSetting();

        PlayerSettings.Android.keystoreName = keystorePath;
        PlayerSettings.Android.keystorePass = keystorePass;
        PlayerSettings.Android.keyaliasName = keyaliasName;
        PlayerSettings.Android.keyaliasPass = keyaliasPass;

        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, buildIdentifier_google);
        UnityPurchasingEditor.TargetAndroidStore(AppStore.GooglePlay);
        EditorUserBuildSettings.development = true;
        EditorUserBuildSettings.buildAppBundle = false;
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;
        LunarConsolePluginEditorHelper.SetLunarConsoleEnabled(true);

        if (!HasAndroidSDK())
        {
            Debug.LogError($"HasAndroidSDK : {HasAndroidSDK()}");
        }
    }

    public static bool HasAndroidSDK()
    {
        return EditorPrefs.HasKey("AndroidSdkRoot") &&
               System.IO.Directory.Exists(EditorPrefs.GetString("AndroidSdkRoot"));
    }

    [MenuItem("Trucking/Set Android Real")]
    public static void PerformAndroid_Real()
    {
        SetDefaultSetting();

        PlayerSettings.Android.keystoreName = keystorePath;
        PlayerSettings.Android.keystorePass = keystorePass;
        PlayerSettings.Android.keyaliasName = keyaliasName;
        PlayerSettings.Android.keyaliasPass = keyaliasPass;
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        EditorUserBuildSettings.development = false;
        EditorUserBuildSettings.buildAppBundle = true;
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, buildIdentifier_google);
        UnityPurchasingEditor.TargetAndroidStore(AppStore.GooglePlay);
        LunarConsolePluginEditorHelper.SetLunarConsoleEnabled(false);

        if (!HasAndroidSDK())
        {
            Debug.LogError($"HasAndroidSDK : {HasAndroidSDK()}");
        }
        else
        {
            BuildPipeline.BuildPlayer(FindEnabledEditorScenes(), BuildPathName("build/PocketTruck"),
                BuildTarget.Android,
                BuildOptions.StrictMode);
        }
    }

    [MenuItem("Trucking/Build Android")]
    private static void PerformAndroidBuild()
    {
        PerformAndroid_Real();

        BuildOptions opt = BuildOptions.None;
        char sep = System.IO.Path.DirectorySeparatorChar;
        string buildDirectory = System.IO.Path.GetFullPath(".") + sep + TARGET_DIR;
        Directory.CreateDirectory(buildDirectory);

        string BUILD_TARGET_PATH = buildDirectory + sep + string.Format(APP_NAME + "_{0}",
                                       Trucking.Common.Trucking.GetVersionString().Replace(".", "_"));
        GenericBuild(SCENES, BUILD_TARGET_PATH, BuildTargetGroup.Android, BuildTarget.Android, opt);
    }

    /// <summary>
    /// ios 자동빌드
    /// </summary>
    public static void AutoiOSBuild()
    {
        SetDefaultSetting();
        PlayerSettings.applicationIdentifier = buildIdentifier_ios;
        BuildOptions opt = BuildOptions.None;
        char sep = System.IO.Path.DirectorySeparatorChar;
        string buildDirectory = System.IO.Path.GetFullPath(".") + sep + TARGET_DIR;
        Directory.CreateDirectory(buildDirectory);

        string BUILD_TARGET_PATH = buildDirectory + sep + "iOS";
        Directory.CreateDirectory(BUILD_TARGET_PATH);

        GenericBuild(SCENES, BUILD_TARGET_PATH, BuildTargetGroup.iOS, BuildTarget.iOS, opt);
    }

    /// <summary>
    /// ios 자동빌드 스토어용
    /// </summary>
    public static void AutoiOSBuildStore()
    {
        SetDefaultSetting();
        PlayerSettings.applicationIdentifier = buildIdentifier_ios;
        BuildOptions opt = BuildOptions.None;
        char sep = System.IO.Path.DirectorySeparatorChar;
        string buildDirectory = System.IO.Path.GetFullPath(".") + sep + TARGET_DIR;
        Directory.CreateDirectory(buildDirectory);

        string BUILD_TARGET_PATH = buildDirectory + sep + "iOS";
        Directory.CreateDirectory(BUILD_TARGET_PATH);

        GenericBuild(SCENES, BUILD_TARGET_PATH, BuildTargetGroup.iOS, BuildTarget.iOS, opt);
    }

    /// <summary>
    /// andoid 자동빌드
    /// </summary>
    public static void AutoAndroidBuild()
    {
        SetDefaultSetting();
        FirebaseUtilBuilder.SetAndroidPlatform(FirebaseUtilBuilder.Platform.Google);
        PlayerSettings.applicationIdentifier = buildIdentifier_google;
        PlayerSettings.Android.keystoreName = keystorePath;
        PlayerSettings.Android.keystorePass = keystorePass;
        PlayerSettings.Android.keyaliasName = keyaliasName;
        PlayerSettings.Android.keyaliasPass = keyaliasPass;
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;
        UnityPurchasingEditor.TargetAndroidStore(AppStore.GooglePlay);

        BuildOptions opt = BuildOptions.None;
        char sep = System.IO.Path.DirectorySeparatorChar;
        string buildDirectory = System.IO.Path.GetFullPath(".") + sep + TARGET_DIR;
        Directory.CreateDirectory(buildDirectory);

        string BUILD_TARGET_PATH = buildDirectory + sep + string.Format("AutoBuild" + "_{0}_arm.apk",
                                       Trucking.Common.Trucking.GetVersionString().Replace(".", "_"));
        GenericBuild(SCENES, BUILD_TARGET_PATH, BuildTargetGroup.Android, BuildTarget.Android, opt);
    }

    /// <summary>
    /// android 자동빌드 스토어용
    /// </summary>
    public static void AutoAndroidBuildStore()
    {
        SetDefaultSetting();
        FirebaseUtilBuilder.SetAndroidPlatform(FirebaseUtilBuilder.Platform.Google);
        PlayerSettings.applicationIdentifier = buildIdentifier_google;
        PlayerSettings.Android.keystoreName = keystorePath;
        PlayerSettings.Android.keystorePass = keystorePass;
        PlayerSettings.Android.keyaliasName = keyaliasName;
        PlayerSettings.Android.keyaliasPass = keyaliasPass;
        UnityPurchasingEditor.TargetAndroidStore(AppStore.GooglePlay);

        BuildOptions opt = BuildOptions.None;
        char sep = System.IO.Path.DirectorySeparatorChar;
        string buildDirectory = System.IO.Path.GetFullPath(".") + sep + TARGET_DIR;
        Directory.CreateDirectory(buildDirectory);

        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;
        string BUILD_TARGET_PATH = buildDirectory + sep + string.Format("AutoBuild" + "_{0}_arm.apk",
                                       Trucking.Common.Trucking.GetVersionString().Replace(".", "_"));
        GenericBuild(SCENES, BUILD_TARGET_PATH, BuildTargetGroup.Android, BuildTarget.Android, opt);

        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.X86;
        BUILD_TARGET_PATH = buildDirectory + sep + string.Format("AutoBuild" + "_{0}_x86.apk",
                                Trucking.Common.Trucking.GetVersionString().Replace(".", "_"));
        GenericBuild(SCENES, BUILD_TARGET_PATH, BuildTargetGroup.Android, BuildTarget.Android, opt);
    }

    [MenuItem("Trucking/Build iOS Debug")]
    public static void PerformiOSDebugBuild()
    {
        SetDefaultSetting();
        PlayerSettings.applicationIdentifier = buildIdentifier_ios;
        BuildOptions opt =
            //		BuildOptions.SymlinkLibraries |
            BuildOptions.ConnectWithProfiler |
            BuildOptions.AllowDebugging |
            BuildOptions.Development |
            BuildOptions.AcceptExternalModificationsToPlayer;

        PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;

        char sep = System.IO.Path.DirectorySeparatorChar;
        string buildDirectory = System.IO.Path.GetFullPath(".") + sep + TARGET_DIR;
        Directory.CreateDirectory(buildDirectory);

        string BUILD_TARGET_PATH = buildDirectory + sep + "iOS";
        Directory.CreateDirectory(BUILD_TARGET_PATH);

        GenericBuild(SCENES, BUILD_TARGET_PATH, BuildTargetGroup.iOS, BuildTarget.iOS, opt);
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled)
                continue;
            EditorScenes.Add(scene.path);
        }

        return EditorScenes.ToArray();
    }

    public static string[] GetLevels(bool standalone = false)
    {
        if (standalone)
        {
            return new string[]
            {
                "Assets/Port/Scenes/SceneTitle.unity",
                "Assets/Port/Scenes/SceneGame.unity",
            };
        }
        else
        {
            return new string[]
            {
                "Assets/Port/Scenes/SceneTitle.unity",
                "Assets/Port/Scenes/SceneGame.unity",
            };
        }
    }

    public static string BuildPathName(string filename)
    {
        var result = string.Format("{0}_{1}_{2}_{3}.apk", filename, PlayerSettings.bundleVersion,
            PlayerSettings.Android.targetArchitectures.ToString(), DateTime.Now.ToString("yyMMddHHmm"));
        return result;
    }

    private static void GenericBuild(string[] scenes,
        string target_filename,
        BuildTargetGroup buildTargetGroup,
        BuildTarget build_target,
        BuildOptions build_options)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(buildTargetGroup, build_target);


        var buildReport = BuildPipeline.BuildPlayer(scenes, target_filename, build_target, build_options);
        if (buildReport == null)
        {
            throw new Exception("BuildPlayer failure");
        }
    }
}