namespace Tools.Editor.CustomKey
{
    public class CustomKey:UnityEditor.Editor
    {
        //快速复制资源路径
        [UnityEditor.MenuItem("Tools/CustomKey/CopyAssetPath  #z")]
        public static void CopyAssetPath()
        {
            var path = UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.Selection.activeObject);
            UnityEditor.EditorGUIUtility.systemCopyBuffer = path;
        }
        
        //Reimport
        [UnityEditor.MenuItem("Tools/CustomKey/Reimport  #r")]
        public static void ReImport()
        {
            var paths = UnityEditor.Selection.assetGUIDs;
            foreach (var path in paths)
            {
                var assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(path);
                UnityEditor.AssetDatabase.ImportAsset(assetPath);
            }
        }
        
 
    }
}