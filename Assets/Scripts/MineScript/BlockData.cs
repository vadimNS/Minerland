using UnityEngine;

public enum BlockType { Coal, Stone, Dirt, Iron, Diamond }
public class BlockData
{
    public BlockType Type;
    public Vector2Int Position;
    public int Health;
    public int MaxHealth;

    public BlockData(BlockType type, Vector2Int pos)
    {
        Type = type;
        Position = pos;
        MaxHealth = GetInitialHealth(type);
        Health = MaxHealth;
    }

    private int GetInitialHealth(BlockType type)
    {
        return type switch
        {
            BlockType.Dirt => 2,
            BlockType.Stone => 4,
            BlockType.Coal => 5,
            BlockType.Iron => 8,
            BlockType.Diamond => 1000,
            _ => 1
        };
    }
    public static int GetBlockPrice(BlockType type)
    {
        return type switch
        {
            BlockType.Dirt => 1,
            BlockType.Stone => 2,
            BlockType.Coal => 4,
            BlockType.Iron => 8,
            BlockType.Diamond => 100,
            _ => 0
        };
    }

}
