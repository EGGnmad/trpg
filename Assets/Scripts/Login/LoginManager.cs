using System;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Events;

namespace TRPG.Login
{
    public class LoginManager : MonoBehaviour
    {
        public UnityEvent SignInCompleted;
        
        void Start()
        {
            PlayerAccountService.Instance.SignedIn += SignInWithUnity;
        }

        private void OnDestroy()
        {
            PlayerAccountService.Instance.SignedIn -= SignInWithUnity;
        }

        #region Methods:SignIn

        public async void StartSignIn()
        {
            await PlayerAccountService.Instance.StartSignInAsync();
        }
        
        private async void SignInWithUnity()
        {
            try
            {
                string accessToken = PlayerAccountService.Instance.AccessToken;
                await SignInWithUnityAsync(accessToken);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        
        private async UniTask SignInWithUnityAsync(string accessToken)
        {
            try
            {
                await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
                Debug.Log("SignIn is successful.");
                
                SignInCompleted?.Invoke();
            }
            catch (AuthenticationException ex)
            {
                // Compare error code to AuthenticationErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
            catch (RequestFailedException ex)
            {
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
        }
        
        #endregion
    }
}
