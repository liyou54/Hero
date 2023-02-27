using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace GameMain.Framework.UI
{
    public class UIHelper : MonoBehaviour
    {
        public List<Component> ComponentList;
        public string Name;

        [Button("标准化")]
        public void Standardize()
        {
            ComponentList = new List<Component>();
            var queue = new Queue<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                queue.Enqueue(child);
            }

            while (queue.Count > 0)
            {
                var temp = queue.Dequeue();
                if (!temp.GetComponent<UILogicBase>())
                {
                    for (int i = 0; i < temp.childCount; i++)
                    {
                        var child = temp.GetChild(i);
                        queue.Enqueue(child);
                    }
                }

                var comps = temp.GetComponents<IUIComponentEx>();
                foreach (var comp in comps)
                {
                    if (comp.IsGenCode)
                    {
                        ComponentList.Add(comp as Component);
                    }
                }
            }
        }

        [Button("生成ViewCode")]
        public void GenViewCode()
        {
            var viewName = Name + "View";
            var viewCode =
                $"using System;\nusing UnityEngine;\nusing GameMain.Framework.UI;\n\nnamespace GameMain.Framework\n{{\n    [Serializable]\n    public class {viewName}\n    {{\n";
            foreach (var component in ComponentList)
            {
                var compName = component.GetType().Name;
                var compCode = $"        public {compName} {(component as IUIComponentEx).CompName};\n";
                viewCode += compCode;
            }

            viewCode += "    }\n}";
            Debug.Log(viewCode);
            var path = "Assets/GameMain/Scripts/UI/View/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileName = viewName + ".cs";
            var fullPath = path + fileName;
            File.WriteAllText(fullPath, viewCode);
            AssetDatabase.ImportAsset(fullPath);
        }

        [Button("生成LogicCode")]
        public void GenLogicCode()
        {
            var logicName = Name + "Logic";

            var logicCode =
                $"using System;\nusing UnityEngine;\nusing GameMain.Framework.UI;\n\nnamespace GameMain.Framework\n{{\n    public class {logicName} : UILogicBase\n    {{\n";
            logicCode += "        [SerializeField] private  " + Name + "View view;\n";
            logicCode += "    }\n}";
            Debug.Log(logicCode);
            var path = "Assets/GameMain/Scripts/UI/Logic/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileName = logicName + ".cs";
            var fullPath = path + fileName;
            File.WriteAllText(fullPath, logicCode);
            AssetDatabase.ImportAsset(fullPath);
        }

        [Button("挂载logic")]
        public void AttachLogic()
        {
            var logicName = "GameMain.Framework." + Name + "Logic";
            var logicType = Type.GetType(logicName);
            //通过反射挂载脚本
            var logic = gameObject.GetComponent(logicType);
            if (logic != null)
            {
                DestroyImmediate(logic);
            }

            logic = gameObject.AddComponent(logicType);

            // 通过反射设置 view
            var viewField = logicType.GetField("view", BindingFlags.Instance | BindingFlags.NonPublic);

            //反射创建 view
            var viewType = Type.GetType("GameMain.Framework." + Name + "View");
            var view = Activator.CreateInstance(viewType);

            // 为脚本赋值
            viewField.SetValue(logic, view);
            for (int i = 0; i < ComponentList.Count; i++)
            {
                var component = ComponentList[i];
                var compName = (component as IUIComponentEx).CompName;
                // 通过反射设置 view 的 comp
                var compField = viewType.GetField(compName, BindingFlags.Instance | BindingFlags.Public);
                compField.SetValue(view, component);
            }
        }
    }
}