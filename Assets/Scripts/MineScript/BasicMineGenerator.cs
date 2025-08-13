using UnityEngine;
public interface IMineGenerator
{
    BlockData[,] Generate();
}
public class BasicMineGenerator : IMineGenerator
{
    private readonly MineGenerationSettings settings;

    public BasicMineGenerator(MineGenerationSettings settings)
    {
        this.settings = settings;
    }

    public BlockData[,] Generate()
    {
        var blocks = new BlockData[settings.width, settings.height];

        for (int x = 0; x < settings.width; x++)
        {
            for (int y = 0; y < settings.height; y++)
            {
                BlockType type;

                // Якщо рядок у верхніх двох — завжди Dirt
                if (y < 2)
                {
                    type = BlockType.Dirt;
                }
                else
                {
                    // Інакше — випадковий блок згідно з шансами
                    type = settings.GetRandomBlockType();
                }

                blocks[x, y] = new BlockData(type, new Vector2Int(x, y));
            }
        }

        return blocks;
    }
}
