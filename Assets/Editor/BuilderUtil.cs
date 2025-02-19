using UnityEditor;

public static class BuilderUtil
{
    static public void AddDefine(string define, BuildTargetGroup group)
    {
        var setting = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);

        if (setting.IndexOf(define) >= 0)
            return;

        if (setting.IndexOf(define + ";") >= 0)
            return;

        if (setting.EndsWith(";"))
        {
            setting += define;
        }
        else
        {
            setting += ";" + define;
        }

        PlayerSettings.SetScriptingDefineSymbolsForGroup(group, setting);
    }

    static public void RemoveDefine(string define, BuildTargetGroup group)
    {
        var setting = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);

        if (setting.IndexOf(define + ";") >= 0)
        {
            var value = setting.Replace(define + ";", "");
            PlayerSettings.SetScriptingDefineSymbolsForGroup(group, value);
            return;
        }

        if (setting.IndexOf(define) >= 0)
        {
            var value = setting.Replace(define, "");
            PlayerSettings.SetScriptingDefineSymbolsForGroup(group, value);
            return;
        }
    }
}