using UnityEditor;
using UnityEngine;

namespace GameMain.Framework.UI.Editor
{
    [CustomEditor(typeof(ButtonEx))]
    public class ButtonExEditor : UnityEditor.UI.ButtonEditor
    {
        ButtonEx buttonEx;

        protected override void OnEnable()
        {
            base.OnEnable();
            buttonEx = (ButtonEx)target;
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
        [MenuItem("GameObject/UIEx/ButtonEx", false, 0)]
        static void CreateButtonEx(MenuCommand menuCommand)
        {
            GameObject btn = new GameObject("ButtonEx");
            GameObject text = new GameObject("Text");
            btn.AddComponent<ButtonEx>();
            btn.AddComponent<ImageEx>();
            text.AddComponent<TextEx>();
            text.transform.SetParent(btn.transform);    
            GameObjectUtility.SetParentAndAlign(btn, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(btn, "Create " + btn.name);
            Selection.activeObject = btn;
        }
    }
}