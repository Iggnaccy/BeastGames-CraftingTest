using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeListSO : ScriptableObject, IEnumerable<RecipeSO>
{
    [SerializeField] private List<RecipeSO> _recipes;
    public RecipeSO this[int index] => _recipes[index];
    public int Count => _recipes.Count;

    public IEnumerator<RecipeSO> GetEnumerator() => _recipes.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

#if UNITY_EDITOR
    [ContextMenu("Fill List")]
    private void FillList()
    {
        _recipes.Clear();
        var guids = UnityEditor.AssetDatabase.FindAssets("t:RecipeSO");
        foreach (var guid in guids)
        {
            var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            var recipe = UnityEditor.AssetDatabase.LoadAssetAtPath<RecipeSO>(path);
            _recipes.Add(recipe);
        }
        _recipes.Sort((a, b) => a.result.Item.ItemID.CompareTo(b.result.Item.ItemID));
    }
#endif
}
