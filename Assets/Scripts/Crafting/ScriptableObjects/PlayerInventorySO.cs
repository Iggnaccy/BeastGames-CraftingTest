using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "SO/PlayerInventorySO", order = 1)]
public class PlayerInventorySO : ScriptableObject
{
    public UnityEvent<IItem, int> OnItemAdded;
    public UnityEvent<IItem, int> OnItemRemoved;

    private List<IItem> items = new List<IItem>();
    private Dictionary<int, int> itemCount = new Dictionary<int, int>();

    public int GetItemCount(int itemID)
    {
        if (itemCount.ContainsKey(itemID))
        {
            return itemCount[itemID];
        }
        return 0;
    }

    public int GetItemCount(IItem item)
    {
        return GetItemCount(item.ItemID);
    }

    public void AddItem(IItem item, int count = 1)
    {
        if (itemCount.ContainsKey(item.ItemID))
        {
            itemCount[item.ItemID] += count;
        }
        else
        {
            items.Add(item);
            itemCount.Add(item.ItemID, count);
        }

        OnItemAdded?.Invoke(item, count);
    }

    public void AddItem(ItemDefinitionSO itemDefinition, int count = 1)
    {
        AddItem(itemDefinition.Item, count);
    }

    public bool RemoveItem(IItem item, int count = 1)
    {
        if(itemCount.TryGetValue(item.ItemID, out int currentCount) && currentCount >= count)
        {
            itemCount[item.ItemID] -= count;
            if (itemCount[item.ItemID] == 0)
            {
                items.Remove(item);
                itemCount.Remove(item.ItemID);
            }
            OnItemRemoved?.Invoke(item, count);
            return true;
        }
        return false;
    }
}
