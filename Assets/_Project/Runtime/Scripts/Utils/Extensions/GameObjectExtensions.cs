using UnityEngine;

// extension methods in C# must be static methods within a static classes
// the First parameter starts with "this" jeyword , whhich results as if that method was part of that class

namespace Utils
{
    public static class GameObjectExtensions{
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component{
            T component = gameObject.GetComponent<T>();
            if(!component) component =gameObject.AddComponent<T>();
            return component;
        }
        public static T OrNull<T>(this T obj) where T : Object => obj ? obj : null;
    }
}
