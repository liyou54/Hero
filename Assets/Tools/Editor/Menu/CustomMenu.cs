using System.Collections.Generic;
using GameMain.Framework.UI;
using UnityEditor;
using UnityEngine;

namespace Tools.Editor.Menu
{
    public class CustomMenu
    {
        //快速对选中Ui生成UIView以及UILogic代码
        [UnityEditor.MenuItem("GameObject/生成UIView代码")]
        public static void GenUICode()
        {
            var go = UnityEditor.Selection.activeGameObject;
            var uvViewName = go.name.Replace("UI_", "") + "View";
            var uiLogicName = go.name.Replace("UI_", "") + "Logic";
            // using GameMain.Framework.UI;
            // using UnityGameFramework.Runtime;
            var comps = go.GetComponentsInChildren<Transform>();
            var res = new List<IUIComponentEx>();
            foreach (var comp in comps)
            {
                var uiComponentEx = comp.GetComponent<IUIComponentEx>();
                if (uiComponentEx != null)
                {
                    res.Add(uiComponentEx);
                }
            }
            
            var viewCode  = "using GameMain.Framework.UI;\nusing UnityGameFramework.Runtime;\n";
            viewCode += string.Format("public class {0}:UIViewBase\n{{\n", uvViewName);
            foreach (var uiComponentEx in res)
            {
                viewCode += string.Format("    public {0} {1};\n", uiComponentEx.GetType().Name, uiComponentEx.CompName);
            }
            viewCode += "}";
            // 写入文件
            var dstPath = "/GameMain/Scripts/UI/View/";
            var fileName = uvViewName + ".cs";
            var fullPath = Application.dataPath + dstPath + fileName;
            PathTools.CreateDirIfNotExists(Application.dataPath + dstPath);
            System.IO.File.WriteAllText(fullPath, viewCode);
        }
    }
}