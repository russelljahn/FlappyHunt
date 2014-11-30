using UnityEditor;
using UnityEngine;


namespace Assets.Scripts.Editor {
    [InitializeOnLoad]
    internal static class AutosaveOnPlay {

        static AutosaveOnPlay() { EditorApplication.playmodeStateChanged = HandleSave; }


        private static void HandleSave() {
            if (!EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isPlaying) {
                return;
            }
            Debug.Log(string.Format("Autosaving scene before entering Play mode: '{0}'", EditorApplication.currentScene));
            EditorApplication.SaveScene();
            EditorApplication.SaveAssets();
        }
    }
}
