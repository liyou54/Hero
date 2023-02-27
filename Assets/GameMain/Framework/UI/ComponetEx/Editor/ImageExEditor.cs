using UnityEditor;
using UnityEngine;

namespace GameMain.Framework.UI.Editor
{
    [CustomEditor(typeof(ImageEx))]
    public class ImageExEditor : UnityEditor.UI.ImageEditor
    {
        ImageEx imageEx;

        protected override void OnEnable()
        {
            base.OnEnable();
            imageEx = (ImageEx)target;
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
        [MenuItem("GameObject/UIEx/ImageEx", false, 0)]
        static void CreateImageEx(MenuCommand menuCommand)
        {
            GameObject go = new GameObject("ImageEx");
            go.AddComponent<ImageEx>();
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}