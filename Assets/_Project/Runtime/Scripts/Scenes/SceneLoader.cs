using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


//we are using the path property of the scene refernce type when we are loading the scene async
//we are also going to load all these scenes additively
//the only scene to be loaded in single mode is the BOOTSTRAPER SCENE beacause its job is to UNLOAD all other scenes

namespace App.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] Image LoadingBar;
        [SerializeField] float FillSpeed = 0.5f;
        [SerializeField] Canvas LoadingCanvas;
        [SerializeField] Camera LoadingCamera;
        [SerializeField] SceneGroup[] SceneGroups;

        float TargetProgress;
        bool IsLoading;
        public readonly SceneGroupManager Manager = new SceneGroupManager();

        void Awake(){
            Manager.OnSceneLoaded +=    sceneName => Debug.Log("Loaded Scene : " + sceneName);
            Manager.OnSceneUnloaded +=    sceneName => Debug.Log("Unloaded Scene: " + sceneName);
            Manager.OnSceneGroupLoaded += sceneGroupName => Debug.Log("Scene Group loaded" + sceneGroupName);
        }
        async void Start()
        {
            await LoadSceneGroup(0);
        }
        void Update()
        {
            if (!IsLoading) return;
            float currentFillAmount = LoadingBar.fillAmount;
            float ProgressDifference = Mathf.Abs(currentFillAmount - TargetProgress);
            float dynamicFillSpeed = ProgressDifference * FillSpeed;
            LoadingBar.fillAmount = Mathf.Lerp(currentFillAmount, TargetProgress, Time.deltaTime * dynamicFillSpeed);
        }
        public async Task LoadSceneGroup(int index)
        {
            LoadingBar.fillAmount = 0f;
            TargetProgress = 1f;

            if (index < 0 || index >= SceneGroups.Length)
            {
                Debug.LogError($"Invalid Scene Group Index : {index}");
                return;
            }
            LoadingProgress progress = new LoadingProgress();
            progress.Progressed += target => TargetProgress = Mathf.Max(target, TargetProgress);

            EnableLoadingCanvas();
            await Manager.LoadScenes(SceneGroups[index], progress);
            EnableLoadingCanvas(false);
        }
        void EnableLoadingCanvas(bool enable = true)
        {
            IsLoading = enable;
            LoadingCanvas.gameObject.SetActive(enable);
            LoadingCamera.gameObject.SetActive(enable);
        }

    }
    public class LoadingProgress : IProgress<float>
    {
        public event Action<float> Progressed;
        const float ratio = 1f;
        public void Report(float value)
        {
            Progressed?.Invoke(value / ratio);
        }
    }
}
