using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace App.SceneManagement
{

    public class Bootrapper : PersistentSingleton<Bootrapper>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static async void Init()
        {
            Debug.Log("Running Bootstrapper Scene");
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync("Bootstrapper", LoadSceneMode.Single);
            await AsyncOperationExtensions.DoAsTask(loadSceneAsync);
        }
    }

}
