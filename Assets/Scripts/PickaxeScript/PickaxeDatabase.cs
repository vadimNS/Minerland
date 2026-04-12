using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PickaxeDatabase", menuName = "Pickaxe/Pickaxe Database")]
public class PickaxeDatabase : ScriptableObject
{
    public List<PickaxeData> pickaxes = new List<PickaxeData>();

    public PickaxeData GetPickaxeData(PickaxeType type)
    {
        foreach (var pickaxe in pickaxes)
        {
            if (pickaxe.type == type)
                return pickaxe;
        }
        return null;
    }
}
