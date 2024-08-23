using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Recipe", menuName = "SO/Items/Recipe")]
public class RecipeSO : ScriptableObject
{
    public List<RecipeElement> ingredients;
    public ItemDefinitionSO result;
    public int resultAmount = 1;
    [Range(0f, 1f)]
    public float chanceToCraft = 1f;

    public UnityEvent OnCraftingSuccess;
    public UnityEvent OnCraftingFailed;

    private void OnValidate()
    {
        foreach (var ingredient in ingredients)
        {
            ingredient.OnValidate();
        }
    
    }
}

[System.Serializable]
public class RecipeElement
{
    [ReadOnly] public int itemId;
    [ReadOnly] public string itemName;
    public int amount = 1;

    [SerializeField] private ItemDefinitionSO _setToThisItem; // This is a tool for the editor to set the item ID based on the item definition, otherwise unused

    public void OnValidate()
    {
        if (_setToThisItem != null)
        {
            itemId = _setToThisItem.Item.ItemID;
            itemName = _setToThisItem.Item.ItemName;
            _setToThisItem = null;
        }
    }
}