#if UNITY_IOS
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

class iOSPostprocessBuild
{
    [PostProcessBuild(999)]
    private static void PostBuildCapability(BuildTarget buildTarget, string path)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            //string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
            //ProjectCapabilityManager projCapability = new ProjectCapabilityManager(projPath, "Unity-iPhone/mmk.entitlements", "Unity-iPhone");
            //projCapability.AddGameCenter();
            //projCapability.AddInAppPurchase();
            ////string[] empty = null;
            ////projCapability.AddiCloud(true, false, empty);
            //projCapability.WriteToFile();

            string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
            var proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(projPath));
            string target = proj.TargetGuidByName(PBXProject.GetUnityTargetName());
            proj.AddCapability(target, PBXCapabilityType.GameCenter);
            proj.AddCapability(target, PBXCapabilityType.InAppPurchase);            
            proj.AddBuildProperty(target, "OTHER_LDFLAGS", "-lxml2"); // FBAudienceNetwork
            File.WriteAllText(projPath, proj.WriteToString());

            FixUnityInterface(path);
            FixApsEnvironment(path);
        }
    }

    private static void FixUnityInterface(string path)
    {
        string unityInterfacePath = path + "/Classes/Unity/UnityInterface.h"; 
        if (File.Exists(unityInterfacePath)) 
        { 
            List<string> interfaceLines = File.ReadAllLines(unityInterfacePath).ToList(); 
            interfaceLines.Insert(1, "#include <stdbool.h>"); 
            StringBuilder sb = new StringBuilder(); 
            foreach (string s in interfaceLines) 
            { 
                sb.Append(s); 
                sb.Append("\n"); 
            } 
            File.WriteAllText(unityInterfacePath, sb.ToString()); 
        }
        else 
        { 
            Debug.LogError("UnityInterface.h doesn't exist"); 
        }
    }

    private static void FixApsEnvironment(string path)
    {
        string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
        var proj = new PBXProject();
        proj.ReadFromString(File.ReadAllText(projPath));
        string target = proj.TargetGuidByName(PBXProject.GetUnityTargetName());
        string targetName = PBXProject.GetUnityTargetName();
        string targetGUID = proj.TargetGuidByName(targetName);

        var entitlementPath = path + "/" + targetName + "/" + targetName + ".entitlements";
        PlistDocument entitlements = new PlistDocument();
        entitlements.root.SetString("aps-environment", "production");
        entitlements.WriteToFile(entitlementPath);

        var entitlementFileName = Path.GetFileName(entitlementPath);
        var unityTarget = PBXProject.GetUnityTargetName();
        var relativeDestination = unityTarget + "/" + entitlementFileName;

        // Add the pbx configs to include the entitlements files on the project
        proj.AddFile(relativeDestination, entitlementFileName);
        proj.AddBuildProperty(targetGUID, "CODE_SIGN_ENTITLEMENTS", relativeDestination);
        proj.AddCapability(target, PBXCapabilityType.PushNotifications);
        File.WriteAllText(projPath, proj.WriteToString());
    }
}
#endif