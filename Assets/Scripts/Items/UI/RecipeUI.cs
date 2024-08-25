using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private PlayerInventorySO _playerInventory; // to update UI to show if player has enough ingredients
    [SerializeField] private RecipeElementUI _ingredientPrefab;
    [SerializeField] private Transform _ingredientParent;
    [SerializeField] private RecipeElementUI _result;

    private List<RecipeElementUI> _cache = new List<RecipeElementUI>();
    private bool p_isDirty = false;
    private RecipeSO p_recipe;

    private void Start()
    {
        _playerInventory.OnItemAdded.AddListener(UpdateIngredients);
    }

    private void OnDestroy()
    {
        _playerInventory.OnItemAdded.RemoveListener(UpdateIngredients);
    }

    public void Setup(RecipeSO recipe)
    {
        for(int i = 0; i < recipe.ingredients.Count; i++)
        {
            RecipeElementUI element = GetElement(i);
            var ingredient = recipe.ingredients[i];
            element.Setup(ingredient.item, ingredient.amount, _playerInventory.GetItemCount(ingredient.item.Item.ItemID));
        }
        _result.Setup(recipe.result, recipe.resultAmount);

        for(int i = recipe.ingredients.Count; i < _cache.Count; i++)
        {
            _cache[i].gameObject.SetActive(false);
        }
        p_recipe = recipe;
    }

    public void UpdateIngredients(IItem _, int __) // we don't care about the item or the amount, we just want to mark the UI as dirty
    { 
        p_isDirty = true;
    }

    private void Update()
    {
        if(p_isDirty)
        {
            for(int i = 0; i < _cache.Count; i++)
            {
                if(_cache[i].gameObject.activeSelf)
                {
                    var ingredient = _cache[i].Item;
                    var recipeRequirerment = p_recipe.ingredients.Find(x => x.item == ingredient).amount;
                }
            }
            p_isDirty = false;
        }
    }

    private RecipeElementUI GetElement(int siblingOrder)
    {
        RecipeElementUI element = _cache.Find(e => !e.gameObject.activeSelf);
        if(element == null)
        {
            element = Instantiate(_ingredientPrefab, _ingredientParent);
            _cache.Add(element);
        }
        element.transform.SetSiblingIndex(siblingOrder);
        return element;
    }

    public bool IsCraftable()
    {
        for(int i = 0; i < p_recipe.ingredients.Count; i++)
        {
            var ingredient = p_recipe.ingredients[i];
            if(_playerInventory.GetItemCount(ingredient.item.Item.ItemID) < ingredient.amount)
            {
                return false;
            }
        }
        return true;
    }
}
