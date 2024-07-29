using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class Bootrapper : PersistentSingleton<Bootrapper>
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static async void Init()
    {
        Debug.Log("Bootstrapper...");
        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync("Bootstrapper", LoadSceneMode.Single);
        await AsyncOperationExtensions.DoAsTask(loadSceneAsync);
    }
}

