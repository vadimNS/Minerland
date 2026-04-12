using UnityEngine;

[CreateAssetMenu(fileName = "BlockSprites", menuName = "Inventory/Block Sprites")]
public class BlockSprites : ScriptableObject
{
    public Sprite coalSprite;
    public Sprite stoneSprite;
    public Sprite dirtSprite;
    public Sprite ironSprite;
    public Sprite diamondSprite;

    public Sprite GetSprite(BlockType type)
    {
        return type switch
        {
            BlockType.Coal => coalSprite,
            BlockType.Stone => stoneSprite,
            BlockType.Dirt => dirtSprite,
            BlockType.Iron => ironSprite,
            BlockType.Diamond => diamondSprite,
            _ => null
        };
    }
}