using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.Extensions
{
    public static class SelectableExtensions
    {
        // Returns original color
        public static Color SetDisabledColor(this Selectable selectable, Color color)
        {
            var colorBlock = selectable.colors;
            var originalColor = colorBlock.disabledColor;

            colorBlock.disabledColor = color;
            selectable.colors = colorBlock;

            return originalColor;
        }


        // Returns original color
        public static Color SetNormalColor(this Selectable selectable, Color color)
        {
            var colorBlock = selectable.colors;
            var originalColor = colorBlock.normalColor;

            colorBlock.normalColor = color;
            selectable.colors = colorBlock;

            return originalColor;
        }


        // Returns original color
        public static Color SetPressedColor(this Selectable selectable, Color color)
        {
            var colorBlock = selectable.colors;
            var originalColor = colorBlock.pressedColor;

            colorBlock.pressedColor = color;
            selectable.colors = colorBlock;

            return originalColor;
        }


        // Returns original color
        public static Color SetHighlightedColor(this Selectable selectable, Color color)
        {
            var colorBlock = selectable.colors;
            var originalColor = colorBlock.highlightedColor;

            colorBlock.highlightedColor = color;
            selectable.colors = colorBlock;

            return originalColor;
        }
    }
}
