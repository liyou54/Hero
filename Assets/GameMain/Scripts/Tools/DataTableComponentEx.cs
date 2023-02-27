using System;
using UnityGameFramework.Runtime;

namespace GameMain.Scripts.Tools
{
    public static class DataTableComponentEx
    {
        public static void CreateDataTableAndRead(this DataTableComponent dataTableComponent, Type type,object thisObj = null)
        {
            var table = dataTableComponent.CreateDataTable(type);
            table.ReadData(string.Format("Assets/Table/{0}.csv", type.Name.Replace("Table", ""), thisObj));
        }
    }
}

