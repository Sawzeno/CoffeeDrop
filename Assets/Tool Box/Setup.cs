using UnityEngine;
using UnityEditor;
using static System.IO.Path;
using static System.IO.Directory;

public static class Setup{
    [MenuItem("Tools/Setup/CreateDefualtFolders")]
    public static void CreateDefualtFolders(){
        Folders.CreateDefault("_Project",
                              "Animation",
                              "Art",
                              "Materials",
                              "Prefabs",
                              "SO",
                              "Scripts",
                              "Settings");
        AssetDatabase.Refresh();
    }

    static class Folders{
        public static void CreateDefault(string root, params string[] folders){
            var fullpath  = Combine(Application.dataPath, root);
            foreach (var folder in folders){
                var path = Combine(fullpath, folder);
                if(!Exists(path)){
                    CreateDirectory(path);
                }
            }
        }
    }
}