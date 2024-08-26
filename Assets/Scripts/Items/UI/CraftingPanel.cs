using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RecipeListSO _recipes;
    [SerializeField] private PlayerInventorySO _playerInventory;
    [SerializeField] private RecipeUI _recipeUIPrefab;
    [Header("UI References")]
    [SerializeField] private Transform _recipesParent;
    [SerializeField] private Toggle _craftableOnlyToggle;

    private List<RecipeUI> _cache = new List<RecipeUI>();

    private void Start()
    {
        _craftableOnlyToggle.onValueChanged.AddListener(ToggleCraftable);
        SetupRecipes();
    }

    private void SetupRecipes()
    {
        for(int i = 0; i < _recipes.Count; i++)
        {
            var recipeUI = GetElement(i);
            recipeUI.Setup(_recipes[i]);
        }
    }

    private void ToggleCraftable(bool onlyCraftable)
    {
        for(int i = 0; i < _recipes.Count; i++)
        {
            RecipeUI element = GetElement(i);
            element.gameObject.SetActive(!onlyCraftable || element.IsCraftable());
        }
    }

    private RecipeUI GetElement(int index)
    {
        if(index >= _cache.Count)
        {
            RecipeUI element = Instantiate(_recipeUIPrefab, _recipesParent);
            _cache.Add(element);
        }
        return _cache[index];
    }
}
