using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeElementUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private Color _enoughColor = Color.white;
    [SerializeField] private Color _notEnoughColor = Color.gray;

    public ItemDefinitionSO Item { get; private set; }

    public void Setup(ItemDefinitionSO item, int count, int playerOwnedAmount = -1)
    {
        _icon.sprite = item.Item.Icon;
        _countText.text = count.ToString();
        if(playerOwnedAmount != -1)
        {
            _countText.color = playerOwnedAmount >= count ? _enoughColor : _notEnoughColor;
        }
        Item = item;
    }

    public void UpdateColor(int playerOwnedAmount, int count)
    {
        _countText.color = playerOwnedAmount >= count ? _enoughColor : _notEnoughColor;
    }
}
