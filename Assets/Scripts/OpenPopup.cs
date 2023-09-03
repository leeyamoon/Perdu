using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPopup : MonoBehaviour
{
    private Button _btn;

    [SerializeField] private bool isButtonUI;
    [SerializeField] private GameObject popup;
    [SerializeField] private Canvas canvas;


    public void Start()
    {
        if (!isButtonUI) return;
        
        _btn = gameObject.GetComponent<Button>();
        if (_btn)
        {
            _btn.onClick.AddListener(OnClick);
        }
    }
    
    private void OnMouseUpAsButton()
    {
        OnClick();
    }
    
    private void OnClick()
    {
        if (GameManager.Shared().GetIsPopup()) return;

        Instantiate(popup, canvas.gameObject.transform);
        GameManager.Shared().SetIsPopup(true);
    }
}
