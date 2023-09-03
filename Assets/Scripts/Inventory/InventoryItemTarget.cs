using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemTarget : MonoBehaviour
{
    [SerializeField] private GameManager.Direction directionOfItem;
    [SerializeField] private AudioClip sound;
    public event Action OnItemReach;
    
    void Start()
    {
        // important for recognizing as the target item
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }
    
    public void ItemReach(GameObject item)
    {
        item.SetActive(false);
        //todo: add offset to right or left according to the object
        
        if (!GameManager.Shared().GetClownMovement(directionOfItem))  //todo check WTF is this
        {
            ItemReachAction();
        }
        else
        {
            GameManager.Shared().GetClownMovement(directionOfItem).WalkToItemAndUse(transform.position.x, ItemReachAction);
        }
    }

    public GameManager.Direction GetDirection()
    {
        return directionOfItem;
    }

    private void ItemReachAction()
    {
        
        // todo: activate item's animation
        OnItemReach?.Invoke();
        AudioManager.Shared().PlaySoundOnce(sound);
    }
}
