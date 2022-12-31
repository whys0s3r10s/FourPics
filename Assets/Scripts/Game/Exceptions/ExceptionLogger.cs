using UnityEngine;

namespace FourPics
{
    public class ExceptionLogger : MonoBehaviour
    {
        private void Awake()
        {
            Application.logMessageReceived += OnLogMessageReceived;

            DontDestroyOnLoad(gameObject);
        }

        private void OnLogMessageReceived(string logText, string stackTrace, LogType logType)
        {
            switch (logType)
            {
                case LogType.Exception:
                    CommonLogger.LogError(logText + stackTrace);
                    break;
                case LogType.Warning:
                    CommonLogger.LogWarn(logText + stackTrace);
                    break;
            }
        }
    }
}