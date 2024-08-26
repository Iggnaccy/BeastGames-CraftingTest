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
    [SerializeField] private Button _button;

    private List<RecipeElementUI> _cache = new List<RecipeElementUI>();
    private bool p_isDirty = false, p_isCraftable = false;
    private RecipeSO p_recipe;

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
        _playerInventory.OnItemAdded.AddListener(UpdateIngredients);
        _button.onClick.AddListener(Craft);
    }

    public void UpdateIngredients(IItem _, int __) // we don't care about the item or the amount, we just want to mark the UI as dirty
    { 
        p_isDirty = true;
    }

    private void Update()
    {
        if(p_isDirty)
        {
            UpdateIngredientsUI();
            UpdateCraftable();
            p_isDirty = false;
        }
    }

    private void UpdateIngredientsUI()
    {
        for (int i = 0; i < _cache.Count; i++)
        {
            var ingredient = _cache[i].Item;
            var recipeRequirerment = p_recipe.ingredients.Find(x => x.item == ingredient).amount;
            var ownedCount = _playerInventory.GetItemCount(_cache[i].Item.ItemID);
            _cache[i].UpdateColor(recipeRequirerment, ownedCount);
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

    private void UpdateCraftable()
    {
        p_isCraftable = IsCraftable();
    }

    private void Craft()
    {
        Debug.Log("Boop!");
        if(p_isDirty)
        {
            UpdateCraftable();
        }
        if(p_isCraftable)
        {
            for(int i = 0; i < p_recipe.ingredients.Count; i++)
            {
                _playerInventory.RemoveItem(p_recipe.ingredients[i].item, p_recipe.ingredients[i].amount);
            }

            bool success = p_recipe.Craft();
            if (success)
            {
                _playerInventory.AddItem(p_recipe.result, p_recipe.resultAmount);
            }
            else
            {
                // probably nothing, as other interactions with recipes should be done on events
                Debug.Log($"Attempt to craft {p_recipe.result.Item.ItemName} failed");
            }
            p_isDirty = true;
        }
    }
}
