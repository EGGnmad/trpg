using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Vivox;
using UnityEngine;

namespace TRPG.Vivox
{
    public class VivoxClient : MonoBehaviour
    {
        public bool IsInitialized { get; private set; } = false;
        public string CurrentChannelName { get; private set; } = "Lobby";
        
        private async void Start()
        {
            await InitializeAsync();
            IsInitialized = true;
            Debug.Log("Init Completed");

            await LoginAsync();
            Debug.Log("Login Completed");

            await JoinChannel(CurrentChannelName);
            Debug.Log("Join Completed");
        }

        public async Task InitializeAsync()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            await VivoxService.Instance.InitializeAsync();
        }

        public async Task LoginAsync()
        {
            LoginOptions options = new()
            {
                DisplayName = Guid.NewGuid().ToString(),
            };
            
            await VivoxService.Instance.LoginAsync(options);
        }
        
        public async Task JoinChannel(string channelName)
        {
            CurrentChannelName = channelName;
            await VivoxService.Instance.JoinGroupChannelAsync(CurrentChannelName, ChatCapability.TextAndAudio);
        }

        public async Task SendMessage(string message)
        {
            await VivoxService.Instance.SendChannelTextMessageAsync(CurrentChannelName, message);
        }
    }
}
