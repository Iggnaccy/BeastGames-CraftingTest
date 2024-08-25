using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDefinitionList", menuName = "SO/Items/ItemDefinitionList")]
public class ItemDefinitionListSO : ScriptableObject
{
    [SerializeField] private List<ItemDefinitionSO> items;

    public ItemDefinitionSO GetItemById(int id)
    {
        foreach (var item in items)
        {
            if (item.Item.ItemID == id)
            {
                return item;
            }
        }

        return null;
    }

#if UNITY_EDITOR
    [ContextMenu("Fill List")]
    private void FillList()
    {
        items.Clear();
        var guids = UnityEditor.AssetDatabase.FindAssets("t:ItemDefinitionSO");
        foreach (var guid in guids)
        {
            var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            var item = UnityEditor.AssetDatabase.LoadAssetAtPath<ItemDefinitionSO>(path);
            items.Add(item);
        }
        items.Sort((a, b) => a.Item.ItemID.CompareTo(b.Item.ItemID));
    }
#endif
}
