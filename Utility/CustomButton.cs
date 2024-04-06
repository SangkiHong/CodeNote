using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CustomButton : Button
{
    public bool useHoldButton;
    public float holdDelayTime = 1f;
    public float holdActionInterval = 0.2f;

    public bool syncTransitionTextsNImages;
    public TextMeshProUGUI[] syncTexts;
    public Image[] syncImages;

    private bool onHoldButton;

    public Action OnButtonDownHandler;
    public Action OnButtonUpHandler;
    public Action OnButtonHoldHandler;

    #region Hold Button
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        OnButtonDownHandler?.Invoke();

        if (useHoldButton)
        {
            onHoldButton = false;

            StartCoroutine(HoldButton());
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        StopAllCoroutines();

        if (onHoldButton == false)
            OnButtonUpHandler?.Invoke();
    }

    private IEnumerator HoldButton()
    {
        float elapsed = 0;

        while (elapsed < holdDelayTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = holdActionInterval;
        onHoldButton = true;

        while (true)
        {
            if (elapsed < holdActionInterval)
            {
                elapsed += Time.deltaTime;
            }
            else
            {
                elapsed = 0;
                OnButtonHoldHandler?.Invoke();
            }

            yield return null;
        }
    }
    #endregion

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        if (syncTransitionTextsNImages)
        {
            Color setColor;

            switch (state)
            {
                default:
                case SelectionState.Normal:
                    setColor = colors.normalColor;
                    break;
                case SelectionState.Highlighted:
                    setColor = colors.highlightedColor;
                    break;
                case SelectionState.Pressed:
                    setColor = colors.pressedColor;
                    break;
                case SelectionState.Selected:
                    setColor = colors.selectedColor;
                    break;
                case SelectionState.Disabled:
                    setColor = colors.disabledColor;
                    break;
            }

            if (syncTexts.Length > 0)
            {
                foreach (var text in syncTexts)
                    text.color = setColor; 
            }

            if (syncImages.Length > 0)
            {
                foreach (var image in syncImages)
                    image.color = setColor; 
            }
        }
    }
}
