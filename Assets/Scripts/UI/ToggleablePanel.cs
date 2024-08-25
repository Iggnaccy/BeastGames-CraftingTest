using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class ToggleablePanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputActionReference _toggleAction;
    [Header("Settings")]
    [SerializeField] private float _movementTime = 0.25f;
    [SerializeField] private float _hiddenXOffset = -900f;
    [SerializeField] private bool _startHidden = true;

    private bool p_isOpen = true;
    private float p_startingX;
    private bool p_isMoving = false;

    private void Start()
    {
        var rectTransform = transform as RectTransform;
        p_startingX = rectTransform.anchoredPosition.x;
        if (_startHidden)
        {
            rectTransform.anchoredPosition = new Vector2(_hiddenXOffset, rectTransform.anchoredPosition.y);
            p_isOpen = false;
        }
        _toggleAction.action.performed += _ => TogglePanel();
    }

    private void OnDestroy()
    {
        _toggleAction.action.performed -= _ => TogglePanel();
    }

    private void Hide(float time)
    {
        Move(_hiddenXOffset, time);
        p_isOpen = false;
    }

    private void Show(float time)
    {
        Move(p_startingX, time);
        p_isOpen = true;
    }

    private void Move(float xPos, float time)
    {
        var rectTransform = transform as RectTransform;
        p_isMoving = true;
        var tween = rectTransform.DOAnchorPosX(xPos, time);
        tween.onComplete += () => p_isMoving = false;
        tween.Play();
    }

    public void TogglePanel()
    {
        if (p_isMoving)
        {
            return;
        }
        if (p_isOpen)
        {
            Hide(_movementTime);
        }
        else
        {
            Show(_movementTime);
        }
    }
}
