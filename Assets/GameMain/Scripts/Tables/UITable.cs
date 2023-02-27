using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.DataTable;

public enum UINameAlais
{
	MainMenu = 1,
	MainGame = 2,
	CardTemp = 3,

}
public enum UIGroupEnum
{
	Top,
	Temp,

}
public class UITable : TableParse , IDataRow
{
    public int Id{ get; protected set; } //id ;
    public UINameAlais Name{ get; protected set; } //UI名称;
    public string Path{ get; protected set; } //UI资源路径;
    public UIGroupEnum Group{ get; protected set; } //UIGroup;
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
		Name = ParseEnum<UINameAlais>(text[index++]);
		Path = ParseString(text[index++]);
		Group = ParseEnum<UIGroupEnum>(text[index++]);
		return true;
	}
	public bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
	{
		throw new System.NotImplementedException();
	}
}