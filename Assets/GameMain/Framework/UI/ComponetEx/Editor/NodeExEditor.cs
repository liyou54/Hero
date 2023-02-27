using GameMain.Framework.UI;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;

[CustomEditor(typeof(NodeEx))]
    public class NodeExEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space();
            serializedObject.Update();
            var genCodeData = this.serializedObject.FindProperty("GenCodeData");
            EditorGUILayout.PropertyField(genCodeData, new GUIContent("代码生成"));
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
        [MenuItem("GameObject/UIEx/NodeEx", false, 0)]
        static void CreateImageEx(MenuCommand menuCommand)
        {
            GameObject go = new GameObject("NodeEx");
            go.AddComponent<NodeEx>();
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
