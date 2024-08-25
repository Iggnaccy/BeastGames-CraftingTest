using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private PlayerInventorySO _playerInventory;
    [SerializeField] private ItemDefinitionListSO _itemDefinitionList;
    [Header("Settings")]
    [SerializeField] private ItemDefinitionSO _item;
    [SerializeField] private bool _isVolatileResource = false;

    private string p_hoverMessage;

    protected virtual void Start()
    {
        if (_item != null)
        {
            Setup(_item);
        }
    }

    public void Setup(int itemID)
    {
        Setup(_itemDefinitionList.GetItemById(itemID));
    }

    public void Setup(BasicItem item)
    {
        Setup(_itemDefinitionList.GetItemById(item.ItemID));
    }

    public virtual void Setup(ItemDefinitionSO item)
    {
        _item = item;
        p_hoverMessage = $"Pick up <b>{_item.Item.ItemName}</b>";
    }

    public virtual string GetHoverMessage()
    {
        return p_hoverMessage;
    }

    public virtual void OnStartHover()
    {
        
    }

    public virtual void OnEndHover()
    {
        
    }

    public virtual void OnInteract()
    {
        Debug.Log($"Picking up {_item.Item.ItemName}");
        _playerInventory.AddItem(_item);
        if (_isVolatileResource)
        {
            Destroy(gameObject);
        }
    }
}
