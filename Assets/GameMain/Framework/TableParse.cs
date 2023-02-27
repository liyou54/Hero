using System.Collections.Generic;
using GameFramework.DataTable;
using UnityEngine;
using UnityGameFramework.Runtime;


public abstract class  TableParse
{
    protected  bool TryDecodeStr(string str, out List<string> res)
    {
        int dotNum = 0;
        res = new List<string>();
        List<char> temp = new List<char>(1024);
        int flag = 0;
        while (flag < str.Length)
        {
            if  (str.Length == flag + 1)
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
    protected  int ParseInt(string data)
    {
        return int.Parse(data);
    }

    protected float ParseFloat(string data)
    {
        return float.Parse(data);
    }

    protected string ParseString(string data)
    {
        return data;
    }

    protected bool ParseBool(string data)
    {
        return bool.Parse(data);
    }

    protected T ParseEnum<T>(string data)
    {
        return (T)System.Enum.Parse(typeof(T), data);
    }

    protected T[] ParseArray<T>(string data, string split = ",")
    {
        string[] strArray = data.Split(split);
        T[] array = new T[strArray.Length];
        if (typeof(T) == typeof(int))
        {
            for (int i = 0; i < strArray.Length; i++)
            {
                array[i] = (T)(object)int.Parse(strArray[i]);
            }
        }
        else if (typeof(T) == typeof(float))
        {
            for (int i = 0; i < strArray.Length; i++)
            {
                array[i] = (T)(object)float.Parse(strArray[i]);
            }
        }
        else if (typeof(T) == typeof(string))
        {
            for (int i = 0; i < strArray.Length; i++)
            {
                array[i] = (T)(object)strArray[i];
            }
        }
        else if (typeof(T) == typeof(bool))
        {
            for (int i = 0; i < strArray.Length; i++)
            {
                array[i] = (T)(object)bool.Parse(strArray[i]);
            }
        }
        return array;
    }

    protected Vector2 ParseVector2(string data)
    {
        string[] strArray = data.Split(",");
        return new Vector2(float.Parse(strArray[0]), float.Parse(strArray[1]));
    }
    
    protected Vector3 ParseVector3(string data)
    {
        string[] strArray = data.Split(",");
        return new Vector3(float.Parse(strArray[0]), float.Parse(strArray[1]), float.Parse(strArray[2]));
    }
    
    protected Vector2Int ParseVector2Int(string data)
    {
        string[] strArray = data.Split(",");
        return new Vector2Int(int.Parse(strArray[0]), int.Parse(strArray[1]));
    }

}