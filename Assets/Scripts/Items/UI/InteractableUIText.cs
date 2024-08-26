using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableUIText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void UpdateText(IInteractable interactable)
    {
        if(interactable == null)
        {
            text.SetText("");
            return;
        }
        text.SetText(interactable.GetHoverMessage());
    }
}
