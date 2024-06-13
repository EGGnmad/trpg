using System;
using TMPro;
using UnityEngine;

namespace TRPG.UI
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public abstract class DeviceManager : MonoBehaviour
    {
        protected TMP_Dropdown _dropdown;

        private void Start()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            Init();
            UpdateDropdown();
        }

        public abstract void Init();
        public abstract void UpdateDropdown();
        public abstract void ChangeDevice(Int32 index);
    }
}
