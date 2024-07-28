using System;
using System.Collections.Generic;
using Eflatun.SceneReference;
using MEC;
using UnityEngine.SceneManagement;

namespace CoffeeDrop
{
    public static class Loader
    {
        static SceneReference LoadingScene = new (SceneGuidToPathMapProvider.ScenePathToGuidMap["Assets/_Project/Scenes/Loading.unity"]);
        static SceneReference TargetScene;
        public static void Load(SceneReference scene){
            TargetScene = scene;
            SceneManager.LoadScene(LoadingScene.Name);
            Timing.RunCoroutine(LoadTargetScene());
        }
        private static IEnumerator<float> LoadTargetScene()
        {
            yield return Timing.WaitForOneFrame;
            SceneManager.LoadScene(TargetScene.Name);
        }
    }

    public class LoadingProgess : IProgress<float>
    {
        public event Action<float> Progressed;
        const float ratio = 1f;
        public void Report(float value)
        {
            Progressed?.Invoke(value/ratio);
        }
    }
}
