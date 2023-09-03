using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampString : MonoBehaviour
{

    [SerializeField] private InventoryItemTarget target;
    [SerializeField] private SpriteRenderer beforeSpriteRenderer;
    [SerializeField] private SpriteRenderer afterSpriteRenderer;
    [SerializeField, Min(0.01f)] private float speed;
    

    private void Start()
    {
        target.OnItemReach += OnItemReach;
    }
    
    
    private void OnItemReach()
    {
        StartCoroutine(GameManager.Shared().ProFadeAnimation(beforeSpriteRenderer,
            afterSpriteRenderer, speed, OpenZone, gameObject));
    }

    private void OpenZone()
    {
        GameManager.Shared().AddRangeToLocation(GameManager.Locations.TutorialZoneUp, 
            GameManager.Shared().GetRangeOfLocation(GameManager.Locations.OuterZoneUp));
    }
}