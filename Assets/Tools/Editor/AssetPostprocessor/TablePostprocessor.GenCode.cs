using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public partial class TablePostprocessor
{

    
    public static bool TryDecodeStr(string str, out List<string> res)
    {
        int dotNum = 0;
        res = new List<string>();
        List<char> temp = new List<char>(1024);
        int flag = 0;
        while (flag < str.Length)
        {
            if (str.Length == flag + 1)
            {
                temp.Add(str[flag]);
                res.Add(new string(temp.ToArray()));
                break;
            }

            if (str[flag] == ',' && (dotNum % 2 == 0))
            {
                res.Add(new string(temp.ToArray()));
                flag++;
                dotNum = 0;
                temp.Clear();
                continue;
            }

            if (str[flag] == '"' && (dotNum % 2 == 0))
            {
                dotNum++;
                flag++;
                continue;
            }

            if (str[flag] == '"' && (dotNum % 2 == 1))
            {
                if (flag + 1 == str.Length)
                {
                    res.Add(new string(temp.ToArray()));
                    break;
                }

                if (str[flag + 1] == '"')
                {
                    temp.Add('"');
                    flag += 2;
                    continue;
                }
                else
                {
                    dotNum++;
                    flag++;
                    continue;
                }
            }

            temp.Add(str[flag]);
            flag++;
        }

        return true;
    }

    public static bool TryGetAttrName(string attrStr, string tableName, out List<string> attrNames)
    {
        attrNames = new List<string>();
        if (!attrStr.StartsWith("#"))
        {
            Debug.LogError("表头必须以#开头");
            return false;
        }

        attrStr = attrStr.Replace("#,", "");
        TryDecodeStr(attrStr, out var attrSplit);
        foreach (var attr in attrSplit)
        {
            //判断只包含字母和数字下划线 且不以数字开头 且不为空 且不重复
            if (attr.Length == 0 ||
                (attr[0] >= '0' && attr[0] <= '9') ||
                attr.Contains(" ") ||
                attr.Contains("\t"))
            {
                Debug.LogError("表头属性名不合法");
                return false;
            }

            foreach (var c in attr)
            {
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '_'))
                {
                    Debug.LogError("表头属性名只能包含字母数字下划线");
                    return false;
                }
            }

            if (attrNames.Contains(attr))
            {
                Debug.LogError("表头属性名不能重复");
                return false;
            }

            attrNames.Add(attr);
        }

        return true;
    }

    public static bool TryGetAttrType(string typeStr, string tableName, out List<string> typeNames)
    {
        typeNames = new List<string>();
        if (!typeStr.StartsWith("#"))
        {
            Debug.LogError("表头必须以#开头");
            return false;
        }

        typeStr = typeStr.Replace("#,", "");
        TryDecodeStr(typeStr, out var typeSplit);

        List<string> types = new List<string>(TypesPrase.Keys);
        foreach (var attr in typeSplit)
        {
            if (attr.EndsWith("Enum") || attr.EndsWith("Alais"))
            {
                typeNames.Add(attr);
            }
            else if (types.Contains(attr))
            {
                typeNames.Add(attr);
            }
            else
            {
                Debug.LogError("表头属性类型错误");
                return false;
            }
        }

        return true;
    }
    
    
    public static bool TryGetEnum(string tableName, List<string> attrNames, List<string> typeNames,
        List<string> descNames, List<string> dataLine, out Dictionary<string, HashSet<(string,int)>> res)
    {
        res = new Dictionary<string, HashSet<(string,int)>>();
        for (int i = 0; i < typeNames.Count; i++)
        {
            string attr = typeNames[i];
            if (attr.EndsWith("Enum")||attr.EndsWith("Alais")) 
            {
                if (!res.ContainsKey(attr))
                {
                    res.Add(attr, new HashSet<(string,int)>());
                }

                var enumData = res[attr];
                for (int j = 3; j < dataLine.Count; j++)
                {
                    var line = dataLine[j];
                    TryDecodeStr(line, out var lineSplit);
                    if (lineSplit.Count != attrNames.Count + 1)
                    {
                        Debug.LogError("表格数据行数不匹配");
                        return false;
                    }
                    enumData.Add((lineSplit[i + 1],int.Parse(lineSplit[1])));
                }
            }
        }

        return true;
    }

    public static bool TryGetAttrDesc(string descStr, string tableName, out List<string> descNames)
    {
        descNames = new List<string>();
        if (!descStr.StartsWith("#"))
        {
            Debug.LogError("表头必须以#开头");
            return false;
        }

        descStr = descStr.Replace("#,", "");
        TryDecodeStr(descStr, out var descSplit);

        foreach (var attr in descSplit)
        {
            descNames.Add(attr);
        }

        return true;
    }

    
    public static void GenCode(string tableName, List<string> attrNames, List<string> typeNames, List<string> descNames,
        Dictionary<string, HashSet<(string,int)>> enumDatas)
    {
        string code = "";
        code += "using System.Collections;\n";
        code += "using System.Collections.Generic;\n";
        code += "using UnityEngine;\n";
        code += "using UnityGameFramework.Runtime;\n";
        code += "using GameFramework.DataTable;\n";
        code += "\n";
        foreach (var enumData in enumDatas)
        {
            var en = new List<string>();
            code += "public enum " + tableName + enumData.Key + "\n{\n";
            foreach (var data in enumData.Value)
            {
                if (enumData.Key.EndsWith("Enum"))
                {
                    if (en.Contains(data.Item1))
                    {
                        continue;
                    }
                    code += "\t"+ data.Item1 + ",\n";
                    en.Add(data.Item1);
                }
                else if(enumData.Key.EndsWith("Alais"))
                {
                    code +="\t"+ data.Item1 + " = " + data.Item2.ToString() + ",\n";
                }
            }

            code += "\n}\n";
        }

        code += "public class " + tableName + "Table : TableParse , IDataRow\n";
        code += "{\n";
        for (int i = 0; i < attrNames.Count; i++)
        {
            if (typeNames[i] == "Desc")
            {
                continue;
            }

            if (typeNames[i].EndsWith("Enum") || typeNames[i].EndsWith("Alais"))
            {
                code += "    public " + tableName + typeNames[i] + " " + attrNames[i] +
                        "{ get; protected set; } //" + descNames[i] + ";\n";
                continue;
            }

            code += "    public " + typeNames[i] + " " + attrNames[i] + "{ get; protected set; } //" + descNames[i] +
                    ";\n";
        }

        code += "\tpublic bool ParseDataRow(string dataRowString, object userData)\n\t{\n";
        code += "\t\tvar ok = TryDecodeStr(dataRowString, out var text);\n";
        code += "\t\tif (!ok)\n\t\t{\n";
        code += "\t\t\tLog.Error(\"解析表格失败\");\n";
        code += "\t\t\treturn false;\n\t\t}\n";

        code += "\t\tint index = 0;\n";
        code += "\t\tindex++;\n";
        for (int i = 0; i < attrNames.Count; i++)
        {
            if (typeNames[i] == "Desc")
            {
                continue;
            }

            if (typeNames[i].EndsWith("Enum") || typeNames[i].EndsWith("Alais"))
            {
                code += "\t\t" + attrNames[i] + " = "
                        + "ParseEnum<" + tableName + typeNames[i]+ ">(text[index++]);\n";
                continue;
            }

            code += "\t\t" + attrNames[i] + " = " + TypesPrase[typeNames[i]] + "(text[index++]);\n";
        }

        code += "\t\treturn true;\n\t}\n";
        code += "\tpublic bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)\n\t{\n";
        code += "\t\tthrow new System.NotImplementedException();\n\t}\n";
        code += "}";

        PathTools.CreateDirIfNotExists(CsPath);
        System.IO.File.WriteAllText(CsPath + tableName + "Table.cs", code);
    }

    public static bool GenTable(string name, string[] lines)
    {
        if (lines.Length < 3)
        {
            Debug.LogError("表格内容为空");
            return false;
        }

        string tableName = Path.GetFileNameWithoutExtension(name);
        List<string> tableAttr;
        List<string> tableType;
        List<string> tableDesc;
        if (!TryGetAttrName(lines[0], tableName, out tableAttr))
        {
            Debug.LogError("表头属性名错误");
            return false;
        }

        if (!TryGetAttrType(lines[1], tableName, out tableType))
        {
            Debug.LogError("表头属性类型错误");
            return false;
        }

        if (!TryGetAttrDesc(lines[2], tableName, out tableDesc))
        {
            Debug.LogError("表头属性描述错误");
            return false;
        }

        if (!TryGetEnum(tableName, tableAttr, tableType, tableDesc, lines.ToList(), out var enumData))
        {
            Debug.LogError("表头属性枚举错误");
            return false;
        }

        GenCode(tableName, tableAttr, tableType, tableDesc, enumData);
        Debug.Log("生成表格" + tableName + "成功");
        return true;
    }
}