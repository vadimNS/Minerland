using UnityEngine;
using UnityEngine.Tilemaps;

public class MineRenderer : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase coalTile;
    [SerializeField] private TileBase stoneTile;
    [SerializeField] private TileBase dirtTile;
    [SerializeField] private TileBase ironTile;
    [SerializeField] private TileBase DiamondTile;

    public void Render(BlockData[,] mineData)
    {
        tilemap.ClearAllTiles();

        int width = mineData.GetLength(0);
        int height = mineData.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                BlockData block = mineData[x, y];
                TileBase tile = GetTileForBlock(block.Type);

                tilemap.SetTile(new Vector3Int(x, -y, 0), tile);
            }
        }

    }

    private TileBase GetTileForBlock(BlockType type)
    {
        return type switch
        {
            BlockType.Coal => coalTile,
            BlockType.Dirt => dirtTile,
            BlockType.Stone => stoneTile,
            BlockType.Iron => ironTile,
            BlockType.Diamond => DiamondTile,
            _ => null,
        };
    }
}
