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

    public bool Craft()
    {
        var roll = Random.Range(0f, 1f);
        if(roll <= chanceToCraft)
        {
            OnCraftingSuccess.Invoke();
            return true;
        }
        else
        {
            OnCraftingFailed.Invoke();
            return false;
        }
    }

    public UnityEvent OnCraftingSuccess;
    public UnityEvent OnCraftingFailed;
}

[System.Serializable]
public class RecipeElement
{
    public ItemDefinitionSO item;
    public int amount = 1;
}