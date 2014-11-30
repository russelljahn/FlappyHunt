using UnityEngine;


namespace Assets.Scripts.Utils {
    public static class ResourcesUtils {

        public static T LoadAsset<T>(string path) where T : UnityEngine.Object {
            var asset = Resources.Load<T>(path);
            if (!asset) {
                Debug.LogError(string.Format("Could not load '{0}' at '{1}'", typeof (T).Name, path));
                return null;
            }
            return asset;
        }


        public static T InstantiateAsset<T>(string path) where T : UnityEngine.Object {
            var asset = LoadAsset<T>(path);
            return (T) Object.Instantiate(asset);
        }


        public static T InstantiateAsset<T>(string path, Vector3 position, Quaternion rotation) where T : UnityEngine.Object {
            var asset = LoadAsset<T>(path);
            return (T) Object.Instantiate(asset, position, rotation);
        }

    }
}
