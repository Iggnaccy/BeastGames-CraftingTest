using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDefinition", menuName = "SO/Items/ItemDefinition")]
public class ItemDefinitionSO : ItemDefinitionAbstract
{
    [SerializeField] private BasicItem _item;
    public override BasicItem Item => _item;
}

public abstract class ItemDefinitionAbstract : ScriptableObject, IItem
{
    public GameObject prefab;
    public abstract BasicItem Item { get; }

    public string ItemName => ((IItem)Item).ItemName;

    public int ItemID => ((IItem)Item).ItemID;

    public Sprite Icon => ((IItem)Item).Icon;
}

