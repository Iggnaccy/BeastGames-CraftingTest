using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabSelector : MonoBehaviour
{
    [SerializeField] private List<Transform> _tabs = new List<Transform>();
    [SerializeField] private List<Button> _buttons = new List<Button>();

    private int p_currentTab = 0;

    private void Start()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            int index = i;
            _buttons[i].onClick.AddListener(() => SelectTab(index));
            _tabs[i].gameObject.SetActive(i == p_currentTab);
        }
        _buttons[p_currentTab].interactable = false;
    }

    private void SelectTab(int index)
    {
        if (index == p_currentTab)
            return;

        _tabs[p_currentTab].gameObject.SetActive(false);
        _tabs[index].gameObject.SetActive(true);
        _buttons[p_currentTab].interactable = true;
        _buttons[index].interactable = false;
        p_currentTab = index;
    }

    private void OnValidate()
    {
        if (_tabs.Count != _buttons.Count)
        {
            Debug.LogError("Tab count does not match button count.");
        }
    }
}
