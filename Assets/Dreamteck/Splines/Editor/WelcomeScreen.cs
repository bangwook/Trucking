using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Dreamteck.Splines
{
    [InitializeOnLoad]
    public class PluginInfo
    {
        public static string version = "1.0.93";
        private static bool open = false;
        static PluginInfo()
        {
            if (open) return;
            bool showInfo = EditorPrefs.GetString("Dreamteck.Splines.Info.version", "") != version;
            if (!showInfo) return;
            EditorWindow.GetWindow<WelcomeScreen>(true);
            EditorPrefs.SetString("Dreamteck.Splines.Info.version", version);
            open = true;
        }
    }

    public class WelcomeScreen : WelcomeWindow
    {
        [MenuItem("Help/Dreamteck/About Dreamteck Splines")]
        public static void OpenWindow()
        {
            WelcomeScreen window = EditorWindow.GetWindow<WelcomeScreen>(true);
            window.Load();
        }

        protected override void GetHeader()
        {
            header = ImageDB.GetImage("plugin_header.png", "Splines/Editor/Icons");
        }

        public override void Load()
        {
            base.Load();
            minSize = maxSize = new Vector2(450, 550);
            SetTitle("Dreamteck Splines " + PluginInfo.version, "Dreamteck Splines");
            panels = new WindowPanel[6];
            panels[0] = new WindowPanel("Home", true, 0.25f); 
            panels[1] = new WindowPanel("Changelog", false, panels[0], 0.25f);
            panels[2] = new WindowPanel("Learn", false, panels[0], 0.25f);
            panels[3] = new WindowPanel("Support", false, panels[0], 0.25f);
            panels[4] = new WindowPanel("Examples", false, panels[2], 0.25f);
            panels[5] = new WindowPanel("Playmaker", false, panels[0], 0.25f);



            panels[0].elements.Add(new WindowPanel.Space(400, 10));
            panels[0].elements.Add(new WindowPanel.Thumbnail("Utilities/Editor/Images", "changelog.png", "What's new?", "See all new features, important changes and bugfixes in " + PluginInfo.version, new ActionLink(panels[1], panels[0])));
            panels[0].elements.Add(new WindowPanel.Thumbnail("Utilities/Editor/Images", "get_started.png", "Get Started", "Learn how to use Dreamteck Splines in a matter of minutes", new ActionLink(panels[2], panels[0])));
            panels[0].elements.Add(new WindowPanel.Thumbnail("Utilities/Editor/Images", "support.png", "Support", "Got a problem or a feature request? Our support is here to help!", new ActionLink(panels[3], panels[0])));
            panels[0].elements.Add(new WindowPanel.Thumbnail("Utilities/Editor/Images", "playmaker.png", "Playmaker Actions", "Install Playmaker actions for Dreamteck Splines", new ActionLink(panels[5], panels[0])));
            panels[0].elements.Add(new WindowPanel.Space(400, 20));
            panels[0].elements.Add(new WindowPanel.Thumbnail("Utilities/Editor/Images", "rate.png", "Rate", "If you like Dreamteck Splines, please consider rating it on the Asset Store", new ActionLink("http://u3d.as/sLk")));
            panels[0].elements.Add(new WindowPanel.Thumbnail("Splines/Editor/Icons", "blenda.png", "Blenda", "Mix sounds and music directly in the Unity editor with our new plugin!", new ActionLink("http://u3d.as/16pv")));
            panels[0].elements.Add(new WindowPanel.Space(400, 10));
            panels[0].elements.Add(new WindowPanel.Label("This window will not appear again automatically. To open it manually go to Help/Dreamteck/About Dreamteck Splines", wrapText, new Color(1f, 1f, 1f, 0.5f), 400, 100));



            string path = ResourceUtility.FindFolder(Application.dataPath, "Dreamteck/Splines/Editor");
            string changelogText = "Changelog file not found.";
            if (Directory.Exists(path))
            {
                if (File.Exists(path + "/changelog.txt"))
                {
                    string[] lines = File.ReadAllLines(path + "/changelog.txt");
                    changelogText = "";
                    for (int i = 0; i < lines.Length; i++)
                    {
                        changelogText += lines[i] + "\r\n";
                    }
                }
            }
            panels[1].elements.Add(new WindowPanel.Space(400, 10));
            panels[1].elements.Add(new WindowPanel.ScrollText(400, 400, changelogText));

            panels[2].elements.Add(new WindowPanel.Space(400, 10));
            panels[2].elements.Add(new WindowPanel.Thumbnail("Utilities/Editor/Images", "youtube.png", "Video Tutorials", "Watch a series of Youtube videos to get started.", new ActionLink("https://www.youtube.com/playlist?list=PLkZqalQdFIQ4S-UGPWCZTTZXiE5MebrVo")));
            panels[2].elements.Add(new WindowPanel.Thumbnail("Utilities/Editor/Images", "pdf.png", "User Manual", "Read a thorough documentation of the whole package along with a list of API methods.", new ActionLink("http://dreamteck.io/page/dreamteck_splines/user_manual.pdf")));
            panels[2].elements.Add(new WindowPanel.Thumbnail("Utilities/Editor/Images", "pdf.png", "API Reference", "A description of the classes, methods and properties inside the Dreamteck Splines API", new ActionLink("http://dreamteck.io/page/dreamteck_splines/api_reference.pdf")));
            panels[2].elements.Add(new WindowPanel.Thumbnail("Utilities/Editor/Images", "examples.png", "Examples", "Install example scenes", new ActionLink(panels[4], panels[2])));

            panels[3].elements.Add(new WindowPanel.Space(400, 10));
            panels[3].elements.Add(new WindowPanel.Thumbnail("Utilities/Editor/Images", "discord.png", "Discord Server", "Join our Discord community and chat with other developers and the team.", new ActionLink("https://discord.gg/bkYDq8v")));
            panels[3].elements.Add(new WindowPanel.Button(400, 30, "Contact Support", new ActionLink("http://dreamteck.io/team/contact.php?target=1")));

            panels[4].elements.Add(new WindowPanel.Space(400, 10));
            bool packagExists = false;
            string dir = ResourceUtility.FindFolder(Application.dataPath, "Dreamteck/Splines/");
            if (Directory.Exists(dir))
            {
                if (File.Exists(dir + "/Examples.unitypackage")) packagExists = true;
            }
            if (packagExists) panels[4].elements.Add(new WindowPanel.Button(400, 30, "Install Examples", new ActionLink(InstallExamples)));
            else panels[4].elements.Add(new WindowPanel.Label("Examples package not found", null, Color.white));

            panels[5].elements.Add(new WindowPanel.Space(400, 10));
            packagExists = false;
            dir = ResourceUtility.FindFolder(Application.dataPath, "Dreamteck/Splines/");
            if (Directory.Exists(dir))
            {
                if (File.Exists(dir + "/PlaymakerActions.unitypackage")) packagExists = true;
            }
            if(packagExists) panels[5].elements.Add(new WindowPanel.Button(400, 30, "Install Actions", new ActionLink(InstallPlaymaker)));
            else panels[5].elements.Add(new WindowPanel.Label("Playmaker actions not found", null, Color.white));
        }

        void InstallExamples()
        {
            string dir = ResourceUtility.FindFolder(Application.dataPath, "Dreamteck/Splines/");
            AssetDatabase.ImportPackage(dir + "/Examples.unitypackage", false);
            EditorUtility.DisplayDialog("Import Complete", "Example scenes have been added to Dreamteck/Splines", "Yey!");
            panels[5].Back();
        }

        void InstallPlaymaker()
        {
            string dir = ResourceUtility.FindFolder(Application.dataPath, "Dreamteck/Splines/");
            AssetDatabase.ImportPackage(dir + "/PlaymakerActions.unitypackage", false);
            EditorUtility.DisplayDialog("Import Complete", "Playmaker actions for Dreamteck Splines have been installed.", "Yey!");
            panels[4].Back();
        }
    }
}
