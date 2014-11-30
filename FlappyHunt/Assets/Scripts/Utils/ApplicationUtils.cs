using UnityEngine;


namespace Assets.Scripts.Utils {
    public static class ApplicationUtils {

        public static bool CanQuitOnPlatform() {
            switch (Application.platform) {
                    // Desktop
                case (RuntimePlatform.OSXPlayer):
                case (RuntimePlatform.WindowsPlayer):
                case (RuntimePlatform.LinuxPlayer):

                    // Mobile
                case (RuntimePlatform.IPhonePlayer):
                case (RuntimePlatform.Android):
                case (RuntimePlatform.WP8Player):
                    return true;

                default:
                    return false;
            }
        }


        public static void Quit() {
            if (!CanQuitOnPlatform()) {
                Debug.LogError(string.Format("Shouldn't be able to quit on platform: '{0}'. Quitting anyways...", Application.platform));
            }
            else {
                Debug.Log("Quitting...");
            }
            Application.Quit();
        }

    }
}
