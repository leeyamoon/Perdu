using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeMachineGears : MonoBehaviour
{
    [SerializeField] private InventoryItemTarget target;
    [SerializeField] private SpriteRenderer beforeSpriteRenderer;
    [SerializeField] private Sprite oneGearOn;
    [SerializeField] private SpriteRenderer afterSpriteRenderer;
    [SerializeField, Min(0.01f)] private float speed;

    [SerializeField] private GameObject objectToAppear;

    private SpriteRenderer _toAppearSpriteRenderer;
    private bool _haveOneGear;
    
    private void Awake()
    {
        _toAppearSpriteRenderer = objectToAppear.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        target.OnItemReach += OnItemReach;
        _haveOneGear = false;
    }
    
    
    private void OnItemReach()
    {
        if (!_haveOneGear)
        {
            _haveOneGear = true;
            beforeSpriteRenderer.sprite = oneGearOn;
            return;
        }
        StartCoroutine(GameManager.Shared().ProFadeWithItemAnimation(beforeSpriteRenderer,
                afterSpriteRenderer, speed, ()=>{}, gameObject, objectToAppear, _toAppearSpriteRenderer));
    }


}
