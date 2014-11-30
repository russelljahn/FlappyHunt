using UnityEngine;


namespace Assets.Scripts.Utils {
    public static class GuiUtils {

        public static void DrawEditorLine() {
            GUILayout.Box(GUIContent.none, GuiStyles.EditorLine, GUILayout.ExpandWidth(true), GUILayout.Height(1f));
        }
    }
}
