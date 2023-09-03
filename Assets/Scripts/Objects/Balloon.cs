using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private InventoryItemTarget target;
    [SerializeField] private GameObject pop;

    [SerializeField] private GameManager.Locations locationToExpend;
    [SerializeField] private GameManager.Locations expendToThisLocation;

    
    private void Start()
    {
        target.OnItemReach += OnItemReach;
    }
    
    
    private void OnItemReach()
    {
        OpenZone();
    }

    private void OpenZone()
    {
        Instantiate(pop, transform.position, Quaternion.identity);
        GameManager.Shared().AddRangeToLocation(locationToExpend, 
            GameManager.Shared().GetRangeOfLocation(expendToThisLocation));
        gameObject.SetActive(false);
    }
}
