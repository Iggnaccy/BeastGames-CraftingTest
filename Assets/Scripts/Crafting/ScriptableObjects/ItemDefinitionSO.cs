using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDefinition", menuName = "SO/Items/ItemDefinition")]
public class ItemDefinitionSO : ScriptableObject
{
    [SerializeField] private BasicItem _item;
    public GameObject prefab;

    public BasicItem Item => _item;
}
