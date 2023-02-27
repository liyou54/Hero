using System.Collections;
using System.Collections.Generic;
using MapEditor.Tools;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapExport : MonoBehaviour
{
    private Tilemap tileMap;

    [SerializeField] private MapBurshEditorSetting _setting;

    [Button]
    public void ExprortData()
    {
        tileMap = GetComponentInChildren<Tilemap>();
        if (tileMap == null)
        {
            Debug.LogError("没有找到TileMap");
            return;
        }
        var res = new List<Vector3Int>();
        var cellBall = tileMap.cellBounds;
        var size = cellBall.size;
        var position = cellBall.position;
        for (int i = 0; i < size.x; i++)
        {
            var x = i + position.x;
            for (int j = 0; j < size.y; j++)
            {
                var y = j + position.y;
                var tile = tileMap.GetTile(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    var t = _setting.GetTypeByTile(tile);
                    if (t != MapCellType.Default)
                    {
                        res.Add(new Vector3Int(x, y, (int)t));
                    }
                }
            }
        }

        //写入二进制
        var bytes = new byte[res.Count * 12];
        for (int i = 0; i < res.Count; i++)
        {
            var v = res[i];
            var x = System.BitConverter.GetBytes(v.x);
            var y = System.BitConverter.GetBytes(v.y);
            var z = System.BitConverter.GetBytes(v.z);
            x.CopyTo(bytes, i * 12);
            y.CopyTo(bytes, i * 12 + 4);
            z.CopyTo(bytes, i * 12 + 8);
        }

        var mapDataPath = Application.dataPath + "/MapData/";
        if (!System.IO.Directory.Exists(mapDataPath))
        {
            System.IO.Directory.CreateDirectory(mapDataPath);
        }

        var path = mapDataPath + gameObject.name + ".bytes";
        System.IO.File.WriteAllBytes(path, bytes);
        Debug.Log("导出成功");
    }
}