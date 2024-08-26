using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "SO/PlayerInventorySO", order = 1)]
public class PlayerInventorySO : ScriptableObject
{
    public UnityEvent<IItem, int> OnItemAdded;
    public UnityEvent<IItem, int> OnItemRemoved;

    private Dictionary<int, IItem> items = new Dictionary<int, IItem>();
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

    public IItem GetItem(int itemID)
    {
        if (items.ContainsKey(itemID))
        {
            return items[itemID];
        }
        return null;
    }

    public void AddItem(IItem item, int count = 1)
    {
        if (items.ContainsKey(item.ItemID))
        {
            itemCount[item.ItemID] += count;
        }
        else
        {
            items.Add(item.ItemID, item);
            itemCount.Add(item.ItemID, count);
        }

        OnItemAdded?.Invoke(item, itemCount[item.ItemID]);
    }

    public bool RemoveItem(IItem item, int count = 1)
    {
        if(itemCount.TryGetValue(item.ItemID, out int currentCount) && currentCount >= count)
        {
            itemCount[item.ItemID] -= count;
            OnItemRemoved?.Invoke(item, itemCount[item.ItemID]);
            if (itemCount[item.ItemID] <= 0)
            {
                items.Remove(item.ItemID);
                itemCount.Remove(item.ItemID);
            }
            return true;
        }
        return false;
    }
}
