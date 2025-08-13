using UnityEngine;

[CreateAssetMenu(fileName = "MineSettings", menuName = "Minerland/Mine Settings")]
public class MineGenerationSettings : ScriptableObject
{
    public int width = 10;
    public int height = 20;

    [Range(0, 1)] public float coalChance = 0.2f;
    [Range(0, 1)] public float ironChange = 0.1f;
    [Range(0, 1)] public float DiamondChange = 0.01f;

    public BlockType GetRandomBlockType()
    {
        float roll = Random.value;
        if (roll < coalChance)
            return BlockType.Coal;
        else if (roll < coalChance + ironChange)
            return BlockType.Iron;
        else if (roll < coalChance + ironChange + DiamondChange)
            return BlockType.Diamond;
        else
            return BlockType.Stone;
    }
}
