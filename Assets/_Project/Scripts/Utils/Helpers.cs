using UnityEngine;

namespace Utils
{
    public class Helpers : MonoBehaviour
    {
        public static void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif            
        }

    }
}
