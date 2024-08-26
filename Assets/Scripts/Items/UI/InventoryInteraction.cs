using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInteraction : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerInventorySO _playerInventory;
    [SerializeField] private Button _throwOutButton, _backgroundButton, _cancelButton;
    [SerializeField] private GameObject _anchor;
    [Header("Settings")]
    [SerializeField, Min(0)] private float _minimumDistanceInEachDirection;
    [SerializeField, Min(0)] private float _maximumDistanceInEachDirection;

    private ItemDefinitionSO p_selectedItem;

    private void Start()
    {
        _backgroundButton.onClick.AddListener(Hide);
        _cancelButton.onClick.AddListener(Hide);
        _throwOutButton.onClick.AddListener(ThrowOut);
        Hide();
    }

    public void Show(ItemDefinitionSO item, RectTransform targetTransform)
    {
        p_selectedItem = item;
        var rectTransform = transform as RectTransform;
        transform.position = new Vector3(targetTransform.position.x + rectTransform.sizeDelta.x / 2, targetTransform.position.y - rectTransform.sizeDelta.y / 2);
        _anchor.SetActive(true);
    }

    private void ThrowOut()
    {
        var itemPrefab = Instantiate(p_selectedItem.prefab);
        float signX = Random.value > 0.5f ? 1 : -1, signZ = Random.value > 0.5f ? 1 : -1;
        var offsetFromPlayer = new Vector3(signX * Mathf.Lerp(_minimumDistanceInEachDirection, _maximumDistanceInEachDirection, Random.value), 
                                           Mathf.Lerp(_minimumDistanceInEachDirection, _maximumDistanceInEachDirection, Random.value), 
                                           signZ * Mathf.Lerp(_minimumDistanceInEachDirection, _maximumDistanceInEachDirection, Random.value));
        itemPrefab.transform.position = _player.transform.position + offsetFromPlayer;
        var itemPickup = itemPrefab.GetComponent<ItemPickup>();
        itemPickup.SetVolatile(true);
        itemPickup.SetIsKinematic(false);
        itemPickup.SetIsKinematic(true, 0.5f);
        itemPickup.Setup(p_selectedItem);

        _playerInventory.RemoveItem(p_selectedItem);
        Hide();
    }

    private void Hide()
    {
        _anchor.SetActive(false);
    }
}
