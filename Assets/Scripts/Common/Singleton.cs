using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    protected static T instance; // hide
    public static T Instance => GetInstance();

    protected static T GetInstance()
    {
        if (instance == null)
        {
            //똑같은 타입의 인스턴스를 찾음
            instance = FindObjectOfType<T>();

            if (instance == null)
            {
                //없다면 생성
                GameObject obj = new GameObject(typeof(T).FullName);
                instance = obj.AddComponent<T>();
            }
        }

        return instance;
    }

    protected virtual void Awake()
    {
        if (!Application.isPlaying)
            return;
        if (instance != this)
            return;

        instance = this as T;
    }
}