using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

//we are using the path property of the scene refernce type when we are loading the scene async
//we are also going to load all these scenes additively
//the only scene to be loaded in single mode is the BOOTSTRAPER SCENE beacause its job is to UNLOAD all other scenes

namespace CoffeeDrop
{
    public class SceneGroupManager{
        SceneGroup ActiveSceneGroup;
        public event Action<string> OnSceneLoaded = delegate{ };
        public event Action<string> OnSceneUnloaded = delegate{ };
        public event Action OnSceneGroupLoaded = delegate{ };

        public async Task LoadScenes(SceneGroup group, IProgress<float> progress, bool reloadDupScenes = false){
            //get any loaded scenes that are still here after running the unload method;
            ActiveSceneGroup =  group;
            var loadedScenes    =   new List<string>();
            await UnloadScenes();
            // get all still loaded scenes and add them to the list
            int sceneCount  =   SceneManager.sceneCount;
            for(var i = 0; i < sceneCount; i++){
                loadedScenes.Add(SceneManager.GetSceneAt(i).name);
            }
            // get the number of scenes that are in the active scene group that we just passed in
            var totalScenesToLoad = ActiveSceneGroup.Scenes.Count;
            // create new async oeration group for them
            var operationGroup = new AsyncOperationGroup(totalScenesToLoad);
            // now load them 
            for(var i = 0; i < totalScenesToLoad; ++i){
                var SceneData =  group.Scenes[i];
                // if this  scene is an duplicate of a scene and its already loaded and we said no dupes, continue
                if(reloadDupScenes == false && loadedScenes.Contains(SceneData.Name)) continue;
                //otherwise
                //start laoding the scene asynchronously and get an handle on that operation
                var operation = SceneManager.LoadSceneAsync(SceneData.Reference.Path, LoadSceneMode.Additive);
                // and the put this oeprtaion into our opertions group
                operationGroup.Operations.Add(operation);
                // once thats done publish an event that says we have started to load this particular scene
                OnSceneLoaded.Invoke(SceneData.Name);

                // wait unitll all AsyncOperations in the Group are done
                while(!operationGroup.IsDone){
                    // publish the PROGRESS REPORT 
                    progress?.Report(operationGroup.Progress);
                    await Task.Delay(100); // dealy to not go too frequently
                }

                Scene activeScene   =   SceneManager.GetSceneByName(ActiveSceneGroup.FindSceneNameByType(SceneType.Active));

                if(activeScene.IsValid()){
                    SceneManager.SetActiveScene(activeScene);
                }
                OnSceneGroupLoaded.Invoke();
            }
        }
        public async Task UnloadScenes() { 
            var scenes = new List<string>();
            var activeSceneName =   SceneManager.GetActiveScene().name;
            int sceneCount= SceneManager.sceneCount;

            for(var i = 0; i < sceneCount; ++i){
                var sceneAt = SceneManager.GetSceneAt(i);
                // make sure that the scene is loaded
                // if its just sitting there unloaded the continue
                if(!sceneAt.isLoaded) continue;
                // test is its active scene or bootstrapper as they should not be unloaded
                var sceneName   =   sceneAt.name;
                if(sceneName  ==  activeSceneName || sceneName == "Bootstrapper") continue;
                scenes.Add(sceneName);
            }
            // create an AsyncOperationGroup fr unloading
            var operationGroup  =   new AsyncOperationGroup(scenes.Count);
            foreach(var scene in scenes){
                var operation = SceneManager.UnloadSceneAsync(scene);
                if(operation == null)continue;
                operationGroup.Operations.Add(operation);
                OnSceneUnloaded.Invoke(scene);
            }
            // wait untill all AsyncOperations in the group are done
            while(!operationGroup.IsDone){
                await Task.Delay(100);// dealy to avoid tight loop
            }
            
        }


    }
//most methods from unity scene Manager return an asyncOperation
// collect all these return types into a group that we can peroform actions and logic on
    public readonly struct AsyncOperationGroup { 
        public readonly List<AsyncOperation> Operations;

        public float Progress => Operations.Count == 0 ? 0 : Operations.Average(o => o.progress);
        public bool IsDone  => Operations.All(o => o.isDone);
        public AsyncOperationGroup(int initialCapacity){
            Operations  =   new List<AsyncOperation>(initialCapacity);
        }
    }
}
