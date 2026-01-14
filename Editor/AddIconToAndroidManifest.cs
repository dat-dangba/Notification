#if UNITY_EDITOR && UNITY_ANDROID
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEditor.Callbacks;

public static class AndroidManifestMerger
{
    private const string ANDROID_NS = "http://schemas.android.com/apk/res/android";

    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget != BuildTarget.Android)
            return;

        string manifestPath = Path.Combine(pathToBuiltProject, "launcher/src/main/AndroidManifest.xml");

        var xml = new XmlDocument();
        xml.Load(manifestPath);

        var manifest = xml.SelectSingleNode("/manifest");
        var application = manifest.SelectSingleNode("application");

        var meta = xml.CreateElement("meta-data");
        meta.SetAttribute("name", ANDROID_NS, "com.google.firebase.messaging.default_notification_icon");
        meta.SetAttribute("resource", ANDROID_NS, "@drawable/icon_small_notification");
        application.AppendChild(meta);

        xml.Save(manifestPath);
    }
}
#endif