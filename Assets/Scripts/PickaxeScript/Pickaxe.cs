using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickaxeType
{
    Wooden,
    Copper,
    Stone,
    Iron,
    Diamond
}
public class Pickaxe
{
    public PickaxeType Type { get; }
    public float Power { get; }
    public float Interval { get; }
    public int Price { get; }

    public Pickaxe(PickaxeType type)
    {
        Type = type;
        switch (type)
        {
            case PickaxeType.Wooden:
                Power = 1f;
                Interval = 1f;
                Price = 0;
                break;
            case PickaxeType.Copper:
                Power = 1.5f;
                Interval = 0.8f;
                Price = 10;
                break;
            case PickaxeType.Stone:
                Power = 2f;
                Interval = 0.6f;
                Price = 25;
                break;
            case PickaxeType.Iron:
                Power = 4f;
                Interval = 0.4f;
                Price = 50;
                break;
            case PickaxeType.Diamond:
                Power = 10f;
                Interval = 0.01f;
                Price = 100;
                break;
        }
    }
}

