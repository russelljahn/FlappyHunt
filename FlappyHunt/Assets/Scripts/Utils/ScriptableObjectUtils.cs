#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;


namespace Assets.Scripts.Utils {
    public static class ScriptableObjectUtils {
        /// <summary>
        //	This makes it easy to create, name and place unique new ScriptableObject asset files.
        /// </summary>
        public static T CreateAsset<T>() where T : ScriptableObject {
            var asset = ScriptableObject.CreateInstance<T>();

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "") {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "") {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            var assetPathAndName =
                AssetDatabase.GenerateUniqueAssetPath(string.Format("{0}/New {1}.asset", path, typeof (T).ToString()));

            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
            return asset;
        }
    }
}


#endif
