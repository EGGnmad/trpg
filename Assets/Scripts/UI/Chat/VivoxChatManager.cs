using System;
using TMPro;
using TRPG.Vivox;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TRPG.UI
{
    public class VivoxChatManager : Logger
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _sendBtn;
        
        [Inject] private VivoxClient _client;

        private void Start()
        {
            _client.MessageReceived += (msg) =>
            {
                base.Log($"<noparse>{msg.SenderDisplayName}: {msg.MessageText}</noparse>");
            };
            
            // Send
            _sendBtn.onClick.AddListener(() =>{
                Log(_inputField.text);
            });
            
            _inputField.onEndEdit.AddListener((_) =>
            {
                if (!Input.GetKeyDown(KeyCode.Return)) return;
                
                _sendBtn.onClick.Invoke();
                _inputField.Select();
                _inputField.ActivateInputField();
            });
        }
        
        protected override void Log(string message)
        {
            if(_client == null) Debug.LogError("VivoxClient is not ready!");
            if (message.Length <= 0) return;
            
            // 서버에 전송
            _client.SendMessage(message).Forget();
            
            // 자신의 메시지 보여주기
            base.Log($"You: <noparse>{message}</noparse>");
            
            // 텍스트 초기화
            _inputField.text = String.Empty;
        }
    }
}