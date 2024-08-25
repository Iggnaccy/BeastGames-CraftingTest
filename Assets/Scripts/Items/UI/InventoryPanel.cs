using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class InventoryPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInventorySO _playerInventory;
    [SerializeField] private ItemUIDisplay _itemPrefab;
    [SerializeField] private Transform _itemParent;

    private Dictionary<int, ItemUIDisplay> p_itemDisplays = new Dictionary<int, ItemUIDisplay>();

    private void Start()
    {
        _playerInventory.OnItemAdded.AddListener(OnItemAdded);
        _playerInventory.OnItemRemoved.AddListener(OnItemRemoved);
    }

    private void OnDestroy()
    {
        _playerInventory.OnItemAdded.RemoveListener(OnItemAdded);
        _playerInventory.OnItemRemoved.RemoveListener(OnItemRemoved);
    }

    private void OnItemAdded(IItem item, int count)
    {
        if (p_itemDisplays.ContainsKey(item.ItemID))
        {
            p_itemDisplays[item.ItemID].SetCount(count);
            p_itemDisplays[item.ItemID].gameObject.SetActive(true);
            return;
        }
        ItemUIDisplay itemDisplay = Instantiate(_itemPrefab, _itemParent);
        itemDisplay.Setup(item, count);
        p_itemDisplays.Add(item.ItemID, itemDisplay);
    }

    private void OnItemRemoved(IItem item, int count)
    {
        if(p_itemDisplays.ContainsKey(item.ItemID))
        {
            if(count <= 0)
            {
                p_itemDisplays[item.ItemID].gameObject.SetActive(false);
                return;
            }
            p_itemDisplays[item.ItemID].SetCount(count);
        }
    }
}
