using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private BasicItem _item;

    protected virtual void Start()
    {
        if (_item != null)
        {
            Setup(_item);
        }
    }

    public virtual void Setup(BasicItem item)
    {
        _item = item;
    }
}
