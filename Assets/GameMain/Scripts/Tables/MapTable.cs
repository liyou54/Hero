using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.DataTable;

public enum MapNameAlais
{
	Default = 1,	
}
public class MapTable : TableParse , IDataRow
{
    public int Id{ get; protected set; } //id ;
    public MapNameAlais Name{ get; protected set; } //UI名称;
    public string Path{ get; protected set; } //UI资源路径;
	public bool ParseDataRow(string dataRowString, object userData)
	{
		var ok = TryDecodeStr(dataRowString, out var text);
		if (!ok)
		{
			Log.Error("解析表格失败");
			return false;
		}
		int index = 0;
		index++;
		Id = ParseInt(text[index++]);
		Name = ParseEnum<MapNameAlais>(text[index++]);
		Path = ParseString(text[index++]);
		return true;
	}
	public bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
	{
		throw new System.NotImplementedException();
	}
}