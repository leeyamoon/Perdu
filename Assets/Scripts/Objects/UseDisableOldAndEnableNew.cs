using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseDisableOldAndEnableNew : MonoBehaviour
{
    [SerializeField] private InventoryItemTarget target;
    [SerializeField] private SpriteRenderer beforeSpriteRenderer;
    [SerializeField] private SpriteRenderer afterSpriteRenderer;
    [SerializeField, Min(0.01f)] private float speed;
    [SerializeField] private bool changeSprite;

    [SerializeField] private GameObject objectToAppear;
    [SerializeField] private GameObject objectToDisappear;


    private SpriteRenderer _toAppearSpriteRenderer;

    private void Awake()
    {
        _toAppearSpriteRenderer = objectToAppear.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        target.OnItemReach += OnItemReach;
    }
    
    
    private void OnItemReach()
    {
        objectToDisappear.SetActive(false);
        
        if (changeSprite)
        {
            StartCoroutine(GameManager.Shared().ProFadeWithItemAnimation(beforeSpriteRenderer,
                afterSpriteRenderer, speed, ()=>{}, gameObject, objectToAppear, _toAppearSpriteRenderer));
        }
        else
        {
            StartCoroutine(ShowItemFade());
        }
        
    }

    private IEnumerator ShowItemFade()
    {
        Color _transparent = Color.white;
        _transparent.a = 0;
        Color _itemColor = beforeSpriteRenderer.color;
        float progress = 0f;
        _toAppearSpriteRenderer.color = _transparent;

        objectToAppear.SetActive(true);

        while (progress < 1)
        {
            _toAppearSpriteRenderer.color =Color.Lerp(_transparent, _itemColor, progress);
            progress += Time.deltaTime * speed;
            yield return null;
        }
    }
}

