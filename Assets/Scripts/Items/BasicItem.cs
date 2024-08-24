using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BasicItem : IItem
{
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _id;

    public string ItemName => _itemName;
    public Sprite Icon => _icon;
    public int ItemID => _id;
}
