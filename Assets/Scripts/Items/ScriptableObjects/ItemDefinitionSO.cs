using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDefinition", menuName = "SO/Items/ItemDefinition")]
public class ItemDefinitionSO : ItemDefinitionAbstract
{
    [SerializeField] private BasicItem _item;
    public override BasicItem Item => _item;
}

public abstract class ItemDefinitionAbstract : ScriptableObject
{
    public GameObject prefab;
    public abstract BasicItem Item { get; }
}

