using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.DataTable;

public enum EntityNameAlais
{
	TileCell = 1,	
}
public enum EntityGroupEnum
{
	TileMap,	
}
public class EntityTable : TableParse , IDataRow
{
    public int Id{ get; protected set; } //实体id ;
    public EntityNameAlais Name{ get; protected set; } //资源名称;
    public string Path{ get; protected set; } //实体资源路径;
    public EntityGroupEnum Group{ get; protected set; } //资源组;
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
		Name = ParseEnum<EntityNameAlais>(text[index++]);
		Path = ParseString(text[index++]);
		Group = ParseEnum<EntityGroupEnum>(text[index++]);
		return true;
	}
	public bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
	{
		throw new System.NotImplementedException();
	}
}