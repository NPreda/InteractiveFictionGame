using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MonoBehaviour))]
public class HideInDerivedDrawer : PropertyDrawer {

    bool? _chachedIsDerived;
    // Cache this so we don't have to muck with strings 
    // or walk the type hierarchy on every repaint.
    bool IsDerived(SerializedProperty property) {        
        if (_chachedIsDerived.HasValue == false) {

            string path = property.propertyPath;
            var type = property.serializedObject.targetObject.GetType();

            if (path.IndexOf('.') > 0) {
                // Field is in a nested type. Dig down to get that type.
                var fieldNames = path.Split('.');
                for(int i = 0; i < fieldNames.Length - 1; i++) {
                    var info = type.GetField(fieldNames[i]);
                    if (info == null)
                        break;
                    type = info.FieldType;
                }
            }

            _chachedIsDerived = fieldInfo.DeclaringType != type;
        }
        return _chachedIsDerived.Value; 
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        // If we're in a more derived type than where this field was declared,
        // abort and draw nothing instead.
        if (IsDerived(property))           
            return;

        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {

        if (IsDerived(property)) {
            // Collapse the unseen derived property.
            return -EditorGUIUtility.standardVerticalSpacing;            
        } else {
            // Provision the normal vertical spacing for this control.
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}