using System;
using UnityEngine;

namespace TRPG.UI
{
    public abstract class Logger : MonoBehaviour
    {
        public event Action<string> OnLog;

        protected virtual void Log(string message)
        {
            OnLog?.Invoke(message);
        }
    }
}