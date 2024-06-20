using System;
using UnityEngine;

namespace TRPG.UI
{
    public class ConsoleLogger : Logger
    {
        private void OnEnable()
        {
            Application.logMessageReceived += Log;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= Log;
        }

        public void Log(string logString, string stackTrace, LogType type)
        {
            string color = String.Empty;
            
            switch (type)
            {
                case LogType.Error:
                case LogType.Exception:
                    color = "red";
                    break;
                case LogType.Warning:
                    color = "yellow";
                    break;
                default:
                    color = "white";
                    break;
            }
            
            Log($"<color={color}>{logString}</color>");
        }
    }
}