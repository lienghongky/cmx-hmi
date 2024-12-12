using UnityEngine;
using TMPro;

public class UILog : MonoBehaviour
{
    public TextMeshPro text;

    public int maxLogCount = 10;  // Maximum number of log lines to display
    private string logContent = ""; // To hold the current log text
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnEnable()
    {
        // Subscribe to the log callback
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        // Unsubscribe from the log callback to avoid memory leaks
        Application.logMessageReceived -= HandleLog;
    }
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Format the log entry
        string newLogEntry = $"<color={(type == LogType.Error ? "red" : type == LogType.Warning ? "yellow" : "white")}>[{type}] {logString}</color>\n";

        // Append to the current log
        logContent += newLogEntry;

        // Limit the number of lines in the log
        string[] logLines = logContent.Split('\n');
        if (logLines.Length > maxLogCount)
        {
            logContent = string.Join("\n", logLines, logLines.Length - maxLogCount, maxLogCount);
        }

        // Update the UI
        if (text != null)
        {
            text.text = logContent;
        }
    }
}
