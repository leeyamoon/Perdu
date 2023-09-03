using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPopupWithAction : MonoBehaviour
{
    private bool _isUsed;
    private SpriteRenderer _objectToAppearSpriteRenderer;

    [SerializeField] private GameObject popup;
    [SerializeField] private Canvas canvas;
    [SerializeField] private AudioClip sound;
    [SerializeField] private bool isMultiple;
    [SerializeField] private SpriteRenderer beforeSpriteRenderer;
    [SerializeField] private SpriteRenderer afterSpriteRenderer;
    [SerializeField] private SpriteRenderer[] beforeSpriteRenderers;
    [SerializeField] private SpriteRenderer[] afterSpriteRenderers;
    [SerializeField, Min(0.01f)] private float speed;
    [SerializeField] private GameObject objectToAppear;

    private void Awake()
    {
        _objectToAppearSpriteRenderer = objectToAppear.GetComponent<SpriteRenderer>();
    }

    private void OnSuccess()
    {
        _isUsed = true;
        AudioManager.Shared().PlaySoundOnce(sound);

        if (!isMultiple)
        {
            StartCoroutine(GameManager.Shared().ProFadeWithItemAnimation(beforeSpriteRenderer,
                afterSpriteRenderer, speed, () => { }, gameObject, objectToAppear, _objectToAppearSpriteRenderer));
            
        }
        else
        {
            StartCoroutine(GameManager.Shared().MultiProFadeWithItemAnimation(beforeSpriteRenderers,
                afterSpriteRenderers, speed, () => { }, gameObject, objectToAppear, _objectToAppearSpriteRenderer));
        }
    }
    
    private void OnMouseUpAsButton()
    {
        if (GameManager.Shared().GetIsPopup()) return;
        
        if (_isUsed) return;
        
        GameObject popupInstance = Instantiate(popup, canvas.gameObject.transform);
        popupInstance.GetComponent<PopupWithAction>().SubscribeSuccessCallback(OnSuccess);
        GameManager.Shared().SetIsPopup(true);
    }
}