using TMPro;
using TRPG.Vivox;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using VContainer;

namespace TRPG.UI
{
    public class ChatWindow : DragWindow
    {
        [Header("Chat View")]
        [SerializeField] private ScrollRect _scrollView;

        [Header("Loggers")] 
        [SerializeField] private Logger[] _loggers;
        
        [Inject] private VivoxClient _client;

        private void Start()
        {
            foreach (var logger in _loggers)
            {
                logger.OnLog += ShowMessage;
            }
        }

        public async void ShowMessage(string message)
        {
            // 생성
            GameObject messagePrefab = await Addressables.InstantiateAsync("Assets/Prefabs/Chat/Message.prefab").Task;

            // 채팅창에 추가
            messagePrefab.transform.SetParent(_scrollView.content);
            messagePrefab.transform.localScale = Vector3.one;
            messagePrefab.GetComponent<TextMeshProUGUI>().text = message;
        }
    }
}