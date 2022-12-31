using System;
using UnityEngine;

namespace FourPics
{
    public class CommonLogger
    {
        public static void Log(string message)
        {
            Debug.Log(message);
        }

        public static void LogWarn(string message)
        {
            Debug.LogWarning(message);
        }

        public static void LogError(string message)
        {
            Debug.LogError(message);
        }

        public static void LogException(Exception exception)
        {
            Debug.LogException(exception);
        }
    }
}
