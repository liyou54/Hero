using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

namespace GameMain.Framework.UI.Editor
{
    [CustomEditor(typeof(TextEx))]
    public class TextExEditor : TMP_EditorPanelUI
    {
        TextEx imageEx;

        protected override void OnEnable()
        {
            base.OnEnable();
            imageEx = (TextEx)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space();
            serializedObject.Update();
            var genCodeData = this.serializedObject.FindProperty("GenCodeData");
            EditorGUILayout.PropertyField(genCodeData, new GUIContent("代码生成"));
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
        [MenuItem("GameObject/UIEx/TextEx", false, 0)]
        static void CreateTextEx(MenuCommand menuCommand)
        {
            GameObject go = new GameObject("TextEx");
            go.AddComponent<TextEx>();
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
        
    }
    
    
}