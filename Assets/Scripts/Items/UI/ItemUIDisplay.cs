using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemUIDisplay : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _itemCount;
    public Button button;

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
