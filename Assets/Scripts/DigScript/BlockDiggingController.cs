using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockDiggingController : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private MineRenderer mineRenderer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform digPoint; // Точка взаємодії (наприклад, руки)
    [SerializeField] private float damageInterval = 1f;
    [SerializeField] private float breakDistance = 1.5f; // Максимальна дистанція для руйнування

    [SerializeField] private BreakingEffectController breakingEffect;


    private Inventory playerInventory;
    private Pickaxe currentPickaxe;
    private Dictionary<PickaxeType, Pickaxe> pickaxes = new();

    private float damageTimer = 0f;
    private BlockData[,] mineData;

    public void SetMineData(BlockData[,] data)
    {
        mineData = data;
    }

    private void Start()
    {

        playerInventory = new Inventory(5);
        InitializePickaxes();
        SetPickaxe(PickaxeType.Diamond); // Стартова
    }
    private void InitializePickaxes()
    {
        foreach (PickaxeType type in System.Enum.GetValues(typeof(PickaxeType)))
        {
            pickaxes[type] = new Pickaxe(type);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= currentPickaxe.Interval)
            {
                TryDamageBlockUnderCursor();
                damageTimer = 0f;
            }
        }
        else
        {
            damageTimer = 0f;
        }

        // Продаж на P
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerInventory.SellAll();
            playerInventory.PrintInventory();
        }

        // Покупка кирок
        if (Input.GetKeyDown(KeyCode.Alpha1)) TryBuyPickaxe(PickaxeType.Wooden);
        if (Input.GetKeyDown(KeyCode.Alpha2)) TryBuyPickaxe(PickaxeType.Copper);
        if (Input.GetKeyDown(KeyCode.Alpha3)) TryBuyPickaxe(PickaxeType.Stone);
        if (Input.GetKeyDown(KeyCode.Alpha4)) TryBuyPickaxe(PickaxeType.Iron);
        if (Input.GetKeyDown(KeyCode.Alpha5)) TryBuyPickaxe(PickaxeType.Diamond);
    }


    private void TryDamageBlockUnderCursor()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);
        int x = cellPos.x;
        int y = -cellPos.y;

        if (mineData == null) return;
        if (x < 0 || y < 0 || x >= mineData.GetLength(0) || y >= mineData.GetLength(1)) return;

        // Прямокутна перевірка відстані
        Vector3 blockWorldCenter = tilemap.GetCellCenterWorld(cellPos);
        Vector2 delta = blockWorldCenter - digPoint.position;

        float maxX = breakDistance;
        float maxY = breakDistance * 2f;

        float normalizedX = delta.x / maxX;
        float normalizedY = delta.y / maxY;

        if ((normalizedX * normalizedX + normalizedY * normalizedY) > 1f)
        {
            Debug.Log("Block is too far to dig (elliptical check).");
            return;
        }

        BlockData block = mineData[x, y];
        if (block == null) return;

        block.Health -= Mathf.CeilToInt(currentPickaxe.Power);

        Debug.Log($"Block {block.Type} at {cellPos} damaged. Health: {block.Health}");

        float progress = 1f - (float)block.Health / block.MaxHealth;
        breakingEffect.Show(blockWorldCenter); // Телепортуємо ефект
        breakingEffect.SetProgress(progress);  // Ставимо кадр

        if (block.Health <= 0)
        {
            tilemap.SetTile(cellPos, null);
            mineData[x, y] = null;

            playerInventory.AddBlock(block.Type);
            playerInventory.PrintInventory();

            breakingEffect.Hide();
        }
    }
    private void TryBuyPickaxe(PickaxeType type)
    {
        Pickaxe newPick = pickaxes[type];
        if (currentPickaxe.Type == type)
        {
            Debug.Log($"You already own and use the {type} pickaxe.");
            return;
        }

        if (newPick.Price == 0 || playerInventory.SpendCoins(newPick.Price))
        {
            SetPickaxe(type);
            Debug.Log($"Bought and equipped {type} pickaxe.");
        }
        else
        {
            Debug.Log($"Not enough coins to buy {type} pickaxe. Need: {newPick.Price}, Have: {playerInventory.Coins}");
        }
    }

    private void SetPickaxe(PickaxeType type)
    {
        currentPickaxe = pickaxes[type];
    }
}
