using System;
using UnityEngine;

namespace TRPG.Debug
{
    public class ConsoleToGUI : MonoBehaviour
    {
        static string myLog = "";
        private string output;
        private string stack;
        
        private void OnEnable()
        {
            UnityEngine.Debug.LogWarning("ConsoleToGUI is enabled!");
            Application.logMessageReceived += Log;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= Log;
        }

        public void Log(string logString, string stackTrace, LogType type)
        {
            output = logString;
            stack = stackTrace;
            myLog = output + "\n" + myLog;
            if (myLog.Length > 5000)
            {
                myLog = myLog.Substring(0, 4000);
            }
        }
        
        void OnGUI()
        {
            GUIStyle temp =  new GUIStyle(GUI.skin.textField);
            temp.fontSize = 34;
            //if (!Application.isEditor) //Do not display in editor ( or you can use the UNITY_EDITOR macro to also disable the rest)
            {
                myLog = GUI.TextArea(new Rect(0, Screen.height - 300, Screen.width, 300), myLog, temp);
            }
        }
    }
}