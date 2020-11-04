
using UnityEditor;


[CustomPropertyDrawer(typeof(DisplayOnly))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, UnityEngine.GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
    public override void OnGUI(UnityEngine.Rect position, SerializedProperty property, UnityEngine.GUIContent label)
    {
        UnityEngine.GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        UnityEngine.GUI.enabled = true;
    }
}
