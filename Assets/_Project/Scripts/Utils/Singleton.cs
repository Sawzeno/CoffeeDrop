using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static T Instance;
        public static bool HasInstance => Instance != null;
        public static T TryGetInstance() => HasInstance ? Instance : null;

        public static T GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = FindAnyObjectByType<T>();
                    if (Instance == null)
                    {
                        var go = new GameObject(typeof(T).Name + "Auto-Generated");
                        Instance = go.AddComponent<T>();
                    }
                }
                return Instance;
            }
        }
        protected virtual void Awake()
        {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton()
        {
            if(!Application.isPlaying) return;
            Instance = this as T;
        }
    }
}
