using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public partial class TablePostprocessor : AssetPostprocessor
{
    public const string TablePath = "Assets/Table/";
    public const string CsPath = "Assets/GameMain/Scripts/Tables/";
    static void OnPostprocessAllAssets(           // 这个函数必须为静态的，其他可以不是！
        string[] importedAssets,
        string[] deletedAssets,
        string[] movedAssets,
        string[] movedFromAssetPaths)
    {
        if (Application.isPlaying)
        {
            return;
        }
        foreach (var path in deletedAssets)
        {
            if (path.StartsWith(TablePath) && path.EndsWith(".csv"))
            {
                var name = Path.GetFileName(path);
                var csPath = CsPath + name.Replace(".csv", ".cs");
                if (File.Exists(csPath))
                {
                    File.Delete(csPath);
                }
            }
            Debug.Log("Deleted Asset: " + path);
        }
        foreach (var path in movedFromAssetPaths)
        {
            if (path.StartsWith(TablePath) && path.EndsWith(".csv"))
            {
                var name = Path.GetFileName(path);
                var csPath = CsPath + name.Replace(".csv", ".cs");
                if (File.Exists(csPath))
                {
                    File.Delete(csPath);
                }
            }
            Debug.Log("Moved Asset: " + path);
        }
        foreach (var path in movedAssets)
        {
            if (path.StartsWith(TablePath) && path.EndsWith(".csv"))
            {
                System.Text.Encoding encode;
                using (System.IO.FileStream fs = new System.IO.FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    encode = GetType(fs);
                }

                if (!Equals(encode, Encoding.UTF8))
                {
                    Debug.LogError("表格必须为UTF8编码");
                }
                
                string[] lines = File.ReadAllLines(path, encode);
                var  name  =Path.GetFileName(path);
                GenTable(name, lines);

            }
        }
        foreach (var path in importedAssets)
        {
            if (path.StartsWith(TablePath) && path.EndsWith(".csv"))
            {
     
                System.Text.Encoding encode;
                using (System.IO.FileStream fs = new System.IO.FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    encode = GetType(fs);
                }

                if (!Equals(encode, Encoding.UTF8))
                {
                    Debug.LogError("表格必须为UTF8编码");
                }
                
                string[] lines = File.ReadAllLines(path, encode);
               var  name  =Path.GetFileName(path);
               GenTable(name, lines);

            }
        }
    }
    
    
    public static System.Text.Encoding GetType(FileStream fs) 
    { 
        byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 }; 
        byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 }; 
        byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM 
        Encoding reVal = Encoding.Default; 

        BinaryReader r = new BinaryReader(fs, System.Text.Encoding.Default); 
        int i; 
        int.TryParse(fs.Length.ToString(), out i); 
        byte[] ss = r.ReadBytes(i); 
        if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF)) 
        { 
            reVal = Encoding.UTF8; 
        } 
        else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00) 
        { 
            reVal = Encoding.BigEndianUnicode; 
        } 
        else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41) 
        { 
            reVal = Encoding.Unicode; 
        } 
        r.Close(); 
        return reVal; 

    } 

    /// <summary> 
    /// 判断是否是不带 BOM 的 UTF8 格式 
    /// </summary> 
    /// <param name="data"></param> 
    /// <returns></returns> 
    private static bool IsUTF8Bytes(byte[] data) 
    { 
        int charByteCounter = 1; 
        //计算当前正分析的字符应还有的字节数 
        byte curByte; //当前分析的字节. 
        for (int i = 0; i < data.Length; i++) 
        { 
            curByte = data[i]; 
            if (charByteCounter == 1) 
            { 
                if (curByte >= 0x80) 
                { 
                    //判断当前 
                    while (((curByte <<= 1) & 0x80) != 0) 
                    { 
                        charByteCounter++; 
                    } 
                    //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X　 
                    if (charByteCounter == 1 || charByteCounter > 6) 
                    { 
                        return false; 
                    } 
                } 
            } 
            else 
            { 
                //若是UTF-8 此时第一位必须为1 
                if ((curByte & 0xC0) != 0x80) 
                { 
                    return false; 
                } 
                charByteCounter--; 
            } 
        } 
        if (charByteCounter > 1) 
        { 
            throw new Exception("非预期的byte格式"); 
        } 
        return true; 
    } 

}
