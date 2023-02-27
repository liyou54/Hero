using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.DataTable;

public enum CardCardTypeEnum
{
	Atttack,
	Defence,

}
public class CardTable : TableParse , IDataRow
{
    public int Id{ get; protected set; } //id ;
    public string Name{ get; protected set; } //CardName;
    public CardCardTypeEnum Group{ get; protected set; } //CardTypeEnum;
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
		Name = ParseString(text[index++]);
		Group = ParseEnum<CardCardTypeEnum>(text[index++]);
		return true;
	}
	public bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
	{
		throw new System.NotImplementedException();
	}
}