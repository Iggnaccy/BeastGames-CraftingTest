using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string GetHoverMessage();
    void OnStartHover();
    void OnEndHover();
    void OnInteract();
}
