using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using System.Xml.Linq;
using System.Linq;

/// <summary>
/// iOS 빌드 시 필요한 옵션을 설정하는 클래스입니다.
/// </summary>
public class InfoplistOptionSetter
{
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target == BuildTarget.iOS)
        {
            string plistPath = Path.Combine(pathToBuiltProject, "Info.plist");

            // Info.plist 
            XDocument plist = XDocument.Load(plistPath);
            XElement dict = plist.Root.Element("dict");

            // 기본 세팅
            SetOrUpdateKey(dict, "CFBundleDevelopmentRegion", "ko");
            SetOrUpdateKey(dict, "CFBundleDisplayName", "세종은 처음이지?");
            SetOrUpdateKey(dict, "CFBundleIdentifier", "com.sejongapp.sj");
            SetOrUpdateKey(dict, "CFBundleShortVersionString", "1.0");
            SetOrUpdateKey(dict, "CFBundleVersion", "1");

            // 필요한 권한 추가
            SetOrUpdateKey(dict, "NSCameraUsageDescription", "이 앱은 카메라를 사용하지 않습니다.");
            SetOrUpdateKey(dict, "NSLocationWhenInUseUsageDescription", "이 앱은 위치 정보를 필요로 합니다.");
            SetOrUpdateKey(dict, "NSLocationAlwaysUsageDescription", "이 앱은 항상 위치 정보를 필요로 합니다.");
            SetOrUpdateKey(dict, "NSBluetoothPeripheralUsageDescription", "이 앱은 블루투스 접근을 필요로 합니다.");
            SetOrUpdateKey(dict, "NSBluetoothAlwaysUsageDescription", "이 앱은 항상 블루투스 접근을 필요로 합니다.");
            SetOrUpdateKey(dict, "NSMicrophoneUsageDescription", "이 앱은 마이크 권한을 필요로 합니다.");
            SetOrUpdateKey(dict, "NSDownloadsFolderUsageDescription", "이 앱은 다운로드 폴더에 접근할 수 있습니다.");

            // http 및 미디어 , 웹 콘텐츠 등 다운로드 허용
            SetOrUpdateAppTransportSecurity(dict);

            // Prepare to save the modified plist
            XDocument newPlist = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XDocumentType("plist", "-//Apple//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null),
                plist.Root
            );

            // Save the modified plist
            newPlist.Save(plistPath);
        }
    }

    private static void SetOrUpdateKey(XElement dict, string key, string value)
    {
        XElement existingKey = dict.Elements("key").FirstOrDefault(e => e.Value == key);
        if (existingKey != null)
        {
            XElement nextElement = existingKey.ElementsAfterSelf().FirstOrDefault();
            if (nextElement != null)
            {
                nextElement.ReplaceWith(new XElement("string", value));
            }
            else
            {
                existingKey.AddAfterSelf(new XElement("string", value));
            }
        }
        else
        {
            dict.Add(new XElement("key", key));
            dict.Add(new XElement("string", value));
        }
    }

    private static void SetOrUpdateAppTransportSecurity(XElement dict)
    {
        XElement existingKey = dict.Elements("key").FirstOrDefault(e => e.Value == "NSAppTransportSecurity");
        XElement atsDict;

        if (existingKey != null)
        {
            atsDict = existingKey.ElementsAfterSelf().OfType<XElement>().FirstOrDefault(e => e.Name == "dict");
            if (atsDict == null)
            {
                atsDict = new XElement("dict");
                existingKey.AddAfterSelf(atsDict);
            }
            atsDict.RemoveAll();
        }
        else
        {
            dict.Add(new XElement("key", "NSAppTransportSecurity"));
            atsDict = new XElement("dict");
            dict.Add(atsDict);
        }

        atsDict.Add(new XElement("key", "NSAllowsArbitraryLoads"));
        atsDict.Add(new XElement("true"));
        atsDict.Add(new XElement("key", "NSAllowsArbitraryLoadsForMedia"));
        atsDict.Add(new XElement("true"));
        atsDict.Add(new XElement("key", "NSAllowsArbitraryLoadsInWebContent"));
        atsDict.Add(new XElement("true"));

        // Add exception domain for the specific HTTP URL
        XElement exceptionDomains = new XElement("dict");
        XElement domainDict = new XElement("dict");
        domainDict.Add(new XElement("key", "NSExceptionAllowsInsecureHTTPLoads"));
        domainDict.Add(new XElement("true"));
        exceptionDomains.Add(new XElement("key", "119.65.154.110"));
        exceptionDomains.Add(domainDict);

        atsDict.Add(new XElement("key", "NSExceptionDomains"));
        atsDict.Add(exceptionDomains);
    }
}
