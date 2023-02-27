using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.DataTable;

public enum SceneNameAlais
{
	MainMenuScene = 1,		GameScene = 2,	
}
public class SceneTable : TableParse , IDataRow
{
    public int Id{ get; protected set; } //id ;
    public SceneNameAlais Name{ get; protected set; } //UI名称;
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
		Name = ParseEnum<SceneNameAlais>(text[index++]);
		Path = ParseString(text[index++]);
		return true;
	}
	public bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
	{
		throw new System.NotImplementedException();
	}
}