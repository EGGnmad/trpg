using Unity.Services.Core;

namespace TRPG.Common
{
    public class GameManager : Singleton<GameManager>
    {
        protected override void Awake()
        {
            base.Awake();
            
            DontDestroyOnLoad(gameObject);

            Init();
        }

        public async void Init()
        {
            await UnityServices.InitializeAsync();
        }
    }
}