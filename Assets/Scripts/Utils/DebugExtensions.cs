namespace Utils
{
    using TMPro;
    using UnityEngine;

    public  static class DebugExtensions
    {
        public static void CreateText(GameObject position, string txt ,int fontSize = 2, Color color = default, TextAlignmentOptions textAlignmentOptions = TextAlignmentOptions.Center)
        {
            CreateText(position.transform.position,txt,fontSize,color,textAlignmentOptions);
        }
        
        public static void CreateText( Vector3 position, string txt ,int fontSize = 2, Color color = default, TextAlignmentOptions textAlignmentOptions = TextAlignmentOptions.Center)
        {
            var text = new GameObject("CreatedDebugText") {transform = {position = position}}.AddComponent<TextMeshPro>();
            text.text = txt;
            text.fontSize = fontSize;
            text.color = color != default ? color : Color.black;
            text.alignment = textAlignmentOptions;
        }
    }
}