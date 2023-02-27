namespace GameMain.Framework.UI.Editor
{
    
    using UnityEditor;
    using UnityEngine;

    namespace GameMain.Framework.UI.Editor
    {
        [CustomEditor(typeof(HorizontalLayoutGroupEx))]
        public class HorizontalLayoutGroupExEditor : UnityEditor.UI.HorizontalOrVerticalLayoutGroupEditor
        {
            HorizontalLayoutGroupEx group;

            protected override void OnEnable()
            {
                base.OnEnable();
                group = (HorizontalLayoutGroupEx)target;
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
            [MenuItem("GameObject/UIEx/HorizontalLayoutGroupEx", false, 0)]
            static void CreateImageEx(MenuCommand menuCommand)
            {
                GameObject go = new GameObject("HorizontalLayoutGroup");
                go.AddComponent<HorizontalLayoutGroupEx>();
                GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
                Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
                Selection.activeObject = go;
            }
        }
    }
}