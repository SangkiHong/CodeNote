using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class AndroidKeystoreSetter : EditorWindow
{
    private static string keystoreName;
    private static string keystorePassword;
    private static string keyAlias;
    private static string keyPassword;

    static AndroidKeystoreSetter()
    {
        LoadAndApplySettings();
    }

    [MenuItem("Build/Set Android Keystore")]
    public static void ShowWindow()
    {
        GetWindow<AndroidKeystoreSetter>("Android Keystore Settings");
    }

    private void OnEnable()
    {
        LoadSettings();
    }

    private void OnGUI()
    {
        GUILayout.Label("Set Android Keystore Settings", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        keystoreName = EditorGUILayout.TextField("Keystore Name", keystoreName);
        if (GUILayout.Button("Browse", GUILayout.Width(70)))
        {
            string path = EditorUtility.OpenFilePanel("Select Keystore", "", "keystore");
            if (!string.IsNullOrEmpty(path))
            {
                keystoreName = path;
            }
        }
        EditorGUILayout.EndHorizontal();

        keystorePassword = EditorGUILayout.PasswordField("Keystore Password", keystorePassword);
        keyAlias = EditorGUILayout.TextField("Key Alias", keyAlias);
        keyPassword = EditorGUILayout.PasswordField("Key Password", keyPassword);

        if (GUILayout.Button("Save and Apply"))
        {
            SaveAndApplySettings();
        }
    }

    private static void SaveAndApplySettings()
    {
        // Save settings
        EditorPrefs.SetString("KeystoreName", keystoreName);
        EditorPrefs.SetString("KeystorePassword", keystorePassword);
        EditorPrefs.SetString("KeyAlias", keyAlias);
        EditorPrefs.SetString("KeyPassword", keyPassword);

        // Apply settings
        PlayerSettings.Android.keystoreName = keystoreName;
        PlayerSettings.Android.keystorePass = keystorePassword;
        PlayerSettings.Android.keyaliasName = keyAlias;
        PlayerSettings.Android.keyaliasPass = keyPassword;

        Debug.Log("Android Keystore settings have been set.");
    }

    private static void LoadSettings()
    {
        // Load settings
        keystoreName = EditorPrefs.GetString("KeystoreName", "");
        keystorePassword = EditorPrefs.GetString("KeystorePassword", "");
        keyAlias = EditorPrefs.GetString("KeyAlias", "");
        keyPassword = EditorPrefs.GetString("KeyPassword", "");
    }

    private static void LoadAndApplySettings()
    {
        // Load settings
        LoadSettings();

        // Apply settings
        PlayerSettings.Android.keystoreName = keystoreName;
        PlayerSettings.Android.keystorePass = keystorePassword;
        PlayerSettings.Android.keyaliasName = keyAlias;
        PlayerSettings.Android.keyaliasPass = keyPassword;

        Debug.Log("Android Keystore settings have been loaded and applied.");
    }
}
