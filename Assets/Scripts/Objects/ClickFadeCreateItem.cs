using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFadeCreateItem : MonoBehaviour
{
    [SerializeField] private GameManager.Direction directionOfItem;
    
    [SerializeField] private SpriteRenderer beforeSpriteRenderer;
    [SerializeField] private SpriteRenderer afterSpriteRenderer;
    [SerializeField, Min(0.01f)] private float speed;
    [SerializeField] private GameObject objectToAppear;
    
    private bool _isUsed;
    private SpriteRenderer _objectToAppearSpriteRenderer;

    private void Awake()
    {
        _objectToAppearSpriteRenderer = objectToAppear.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _isUsed = false;
    }

    private void OnMouseDown()
    {
        if (GameManager.Shared().GetIsPopup()) return;
        
        if (_isUsed) return;
        _isUsed = true;
        
        GameManager.Shared().GetClownMovement(directionOfItem).WalkToItemAndUse(transform.position.x, ItemReachAction);
    }

    private void ItemReachAction()
    {
        StartCoroutine(GameManager.Shared().ProFadeWithItemAnimation(beforeSpriteRenderer,
            afterSpriteRenderer, speed, () => { }, gameObject, objectToAppear, _objectToAppearSpriteRenderer));
    }
}
