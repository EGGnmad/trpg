using System;
using Cysharp.Threading.Tasks;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.Events;

namespace TRPG.Vivox
{
    public class VivoxClient : MonoBehaviour
    {
        public bool IsInitialized { get; private set; } = false;
        public string CurrentChannelName { get; private set; } = "Lobby";
        public string DisplayName { get; private set; }
        
        public event Action<VivoxMessage> MessageReceived;
        public UnityEvent JoinCompleted;
        
        private async void Start()
        {
            await InitializeAsync();
            IsInitialized = true;

            await LoginAsync();

            await JoinChannelAsync(CurrentChannelName);
            Debug.Log("접속 완료");
            JoinCompleted.Invoke();
        }

        public async UniTask InitializeAsync()
        {
            await VivoxService.Instance.InitializeAsync();
        }

        public async UniTask LoginAsync()
        {
            DisplayName = Guid.NewGuid().ToString();
            LoginOptions options = new()
            {
                DisplayName = this.DisplayName,
            };
            
            await VivoxService.Instance.LoginAsync(options);
        }
        
        public async UniTask JoinChannelAsync(string channelName)
        {
            CurrentChannelName = channelName;
            await VivoxService.Instance.JoinGroupChannelAsync(CurrentChannelName, ChatCapability.TextAndAudio);

            VivoxService.Instance.ChannelMessageReceived += ReceiveMessage;
        }

        public async UniTask LeaveCurrentChannelAsync()
        {
            if (CurrentChannelName == String.Empty) return;
            
            CurrentChannelName = String.Empty;
            VivoxService.Instance.ChannelMessageReceived -= ReceiveMessage;
            
            await VivoxService.Instance.LeaveChannelAsync(CurrentChannelName);
        }

        public new async UniTaskVoid SendMessage(string message)
        {
            await VivoxService.Instance.SendChannelTextMessageAsync(CurrentChannelName, message);
        }

        public void ReceiveMessage(VivoxMessage message)
        {
            if (message.FromSelf) return;
            MessageReceived?.Invoke(message);
        }
    }
}
