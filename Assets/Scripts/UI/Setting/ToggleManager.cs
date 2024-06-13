using UnityEngine;

namespace TRPG.UI
{
    public class ToggleManager : MonoBehaviour
    {
        [SerializeField] private KeyCode _keyCode;
        [SerializeField] private GameObject _gameObject;
        
        void Update()
        {
            if (Input.GetKeyDown(_keyCode))
            {
                Toggle();
            }
        }

        public void Toggle()
        {
            _gameObject.SetActive(!_gameObject.activeSelf);
        }
    }
}
