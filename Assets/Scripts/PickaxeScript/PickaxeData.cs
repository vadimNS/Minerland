using UnityEngine;

[CreateAssetMenu(fileName = "PickaxeData", menuName = "Pickaxe/Pickaxe Data")]
public class PickaxeData : ScriptableObject
{
    public PickaxeType type;
    public float power;
    public float interval;
    public int price;
    public Sprite sprite;
}
