using System.Collections.Generic;
using Eflatun.SceneReference;
using MEC;
using UnityEngine.SceneManagement;

namespace CoffeeDrop
{
    public static class Loader
    {
        static SceneReference TargetScene;
        public static void Load(SceneReference scene){
            TargetScene = scene;
            Timing.RunCoroutine(LoadTargetScene());
        }
        private static IEnumerator<float> LoadTargetScene()
        {
            yield return Timing.WaitForOneFrame;
            SceneManager.LoadScene(TargetScene.Name);
        }
    }
}
