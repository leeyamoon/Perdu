using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemIcon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject item;
    private Button btn;
    // private bool isMouseDown;
    
    [SerializeField] private AudioClip sound;

    public void Start()
    {
        btn = gameObject.GetComponent<Button>();
        if (btn)
        {
            btn.onClick.AddListener(OnClick);
        }
    }

    public void SetItem(GameObject obj)
    {
        item = obj;
    }

    private void OnClick()
    {
        // item.GetComponent<InventoryItem>().SwitchIconWithItem();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // item.GetComponent<InventoryItem>().SwitchIconWithItem();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Shared().GetIsPopup()) return;

        item.GetComponent<InventoryItem>().SwitchIconWithItem();
        AudioManager.Shared().PlaySoundOnce(sound);
    }
}
