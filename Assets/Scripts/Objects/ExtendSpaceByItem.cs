using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendSpaceByItem : MonoBehaviour
{
    [SerializeField] private InventoryItemTarget target;
    [SerializeField] private SpriteRenderer beforeSpriteRenderer;
    [SerializeField] private SpriteRenderer afterSpriteRenderer;
    [SerializeField, Min(0.01f)] private float speed;

    [SerializeField] private GameManager.Locations locationToExpend;
    [SerializeField] private GameManager.Locations[] anotherLocationsToExtend;
    [SerializeField] private GameManager.Locations expendToThisLocation;



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
        
        GameManager.Shared().AddRangeToLocation(locationToExpend, 
            GameManager.Shared().GetRangeOfLocation(expendToThisLocation));
        foreach (var loc in anotherLocationsToExtend)
        {
            GameManager.Shared().AddRangeToLocation(loc,
                        GameManager.Shared().GetRangeOfLocation(expendToThisLocation));
        }
        
    }
}
