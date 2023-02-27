using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;

namespace MapEditor.Tools
{

    public enum MapCellType
    {
        MapEdge,
        MapGround,
        MapRiver,
        MapWall,
        MapStart,
        Default,
    }
    
    [CreateAssetMenu(fileName = "MapBurshEditorSetting", menuName = "编辑器工具/编辑器笔刷", order = 0)]
    public class MapBurshEditorSetting : ScriptableObject
    {
        public SerializedDictionary<Tile, MapCellType> Config;

        public MapCellType GetTypeByTile(TileBase tile)
        {
            foreach (var c in Config)
            {
                if (c.Key.name == tile.name)
                {
                    return c.Value;
                }
            }
            return MapCellType.Default;
        }
    }
}