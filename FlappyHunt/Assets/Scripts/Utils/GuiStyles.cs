using UnityEngine;


namespace Assets.Scripts.Utils {
    public static class GuiStyles {
        public static GUIStyle EditorLine { get; private set; }


        static GuiStyles() {
            EditorLine = new GUIStyle("box");
            EditorLine.border.top = EditorLine.border.bottom = 1;
            EditorLine.margin.top = EditorLine.margin.bottom = 1;
            EditorLine.padding.top = EditorLine.padding.bottom = 1;
        }

    }
}
