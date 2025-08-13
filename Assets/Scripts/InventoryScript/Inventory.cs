using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<InventorySlot> slots;
    private int coins = 0;
    public int Coins => coins;

    public Inventory(int initialSlotCount = 5)
    {
        slots = new List<InventorySlot>();
        for (int i = 0; i < initialSlotCount; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    public void AddBlock(BlockType type)
    {
        foreach (var slot in slots)
        {
            if (slot.CanStack(type))
            {
                slot.Add(type, 1);
                Debug.Log($"Block {type} added to existing slot. Total: {slot.Count}");
                return;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.Add(type, 1);
                Debug.Log($"Block {type} added to NEW slot. Total: {slot.Count}");
                return;
            }
        }

        Debug.Log($"Inventory FULL! Could not add block of type {type}.");
    }
    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            return true;
        }
        return false;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }
    public void PrintInventory()
    {
        Debug.Log("Inventory Contents:");
        for (int i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];
            string content = slot.IsEmpty ? "Empty" : $"{slot.Type} x{slot.Count}";
            Debug.Log($"Slot {i + 1}: {content}");
        }
    }

    public void SellAll()
    {
        int total = 0;
        foreach (var slot in slots)
        {
            total += slot.SellAll();
        }

        coins += total;
        Debug.Log($"Total earned: {total}. Current balance: {coins} coins.");
    }
}
