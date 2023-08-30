using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using UnityEngine;

public class ErrorLog : MonoBehaviour
{
    private static ErrorLog instance;
    
    public bool isErrorLog = true;
    public bool isGenericLog = true;

    private string errorLogPath;
    private string genericLogPath;

    private int maxLine = 1000;

    void Awake()
    {
        SetSingleton();
        SetPath();

        Application.logMessageReceived += LogHandle;
    }

    void SetSingleton()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogError("동일한 게임 오브젝트가 존재합니다.");
            Destroy(gameObject);
            return;
        }
    }

    void SetPath()
    {
#if UNITY_EDITOR
        genericLogPath = Application.dataPath + @"/GenericLog.txt";
        errorLogPath = Application.dataPath + @"/ErrorLog.txt";
#elif UNITY_ANDROID || UNITY_WSA_10_0
        errorLogPath = Application.persistentDataPath + @"/GenericLog.txt";
        errorLogPath = Application.persistentDataPath + @"/ErrorLog.txt";
#else
        genericLogPath = Application.dataPath + @"/GenericLog.txt";
        errorLogPath = Application.dataPath + @"/ErrorLog.txt";
#endif
    }

    void LogHandle(string condition, string stackTrace, LogType type)
    {
        switch (type)
        {
            case LogType.Error:
                if(isErrorLog)
                    SaveLog(errorLogPath, condition, stackTrace);
                break;
            case LogType.Assert:
                break;
            case LogType.Warning:
                break;
            case LogType.Log:
                if (isGenericLog)
                    SaveLog(genericLogPath, condition, stackTrace);
                break;
            case LogType.Exception:
                if (isErrorLog)
                    SaveLog(errorLogPath, condition, stackTrace);
                break;
            default:
                break;
        }
    }

    void SaveLog(string path, string condition, string stackTrace)
    {
        string[] allLines = new string[0];
        FileInfo fi = new FileInfo(path);

        if (fi.Exists)
        {
             allLines = File.ReadAllLines(path);
        }

        List<string> list = new List<string>(allLines);

        list.Add(DateTime.Now.ToString("s", DateTimeFormatInfo.InvariantInfo));
        list.Add(condition);
        list.Add(stackTrace);

        while(list.Count > maxLine)
        {
            list.RemoveAt(0);
        }

        File.WriteAllLines(path, list.ToArray());
    }
}
