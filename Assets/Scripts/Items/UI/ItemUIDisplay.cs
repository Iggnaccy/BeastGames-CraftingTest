using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIDisplay : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _itemCount;

    private IItem p_item;

    public void Setup(IItem item, int count)
    {
        p_item = item;
        _icon.sprite = item.Icon;
        SetCount(count);
    }

    public void SetCount(int count)
    {
        _itemCount.text = count.ToString();
    }
}
