using System;
using System.Collections.Generic;
using System.Linq;
using Eflatun.SceneReference;
using UnityEditor.SearchService;
using UnityEngine;

namespace CoffeeDrop
{
    [Serializable]
    public class SceneGroup : MonoBehaviour
    {
        public string GroupName =   "New Scene Group";
        public List<SceneData> Scenes;
        public string FindSceneNameByType(SceneType sceneType){
            return Scenes.FirstOrDefault(scene => scene.SceneType == sceneType)?.Reference.Name;
        }
    }

    public class SceneData{
        public SceneReference Reference;
        public string Name => Reference.Name;
        public SceneType SceneType;
    }
    public enum SceneType{ActiveScene, MainMenu, UserInerface, HUD ,Cinematic, Environment, Tooling}
}