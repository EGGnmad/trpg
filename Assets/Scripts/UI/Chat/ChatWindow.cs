using TMPro;
using TRPG.Vivox;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TRPG.UI
{
    public class ChatWindow : DragWindow
    {
        [Header("Chat View")]
        [SerializeField] private ScrollRect _scrollView;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _sendBtn;

        [Inject] private VivoxClient _client;

        private void Start()
        {
            // Receive
            VivoxService.Instance.ChannelMessageReceived += (msg) =>
            {
                ShowMessage(msg.MessageText);
            };
            
            // Send
            _sendBtn.onClick.AddListener(() =>{
                SendMessage(_inputField.text);
            });
        }

        public void ShowMessage(string message)
        {
            
        }

        public async new void SendMessage(string message)
        {
            if(_client == null) Debug.LogError("VivoxClient is not ready!");
            await _client.SendMessage(message);
        }
    }
}
