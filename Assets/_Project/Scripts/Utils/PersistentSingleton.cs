using UnityEngine;

namespace Utils
{
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        // dont doestroy on load owrks on objects that are at the root level
        public bool AutoUnparentOnAwake = true;
        protected static T Instance;

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
            if (!Application.isPlaying) return;
            if (AutoUnparentOnAwake) { transform.SetParent(null); }
            if(Instance == null){
                Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }else{
                if(Instance != this){
                    Destroy(gameObject);
                }
            }
        }
    }
}
