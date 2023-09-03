using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeleteButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button _btn;
    private CanvasGroup _canvasGroup;
    
    [SerializeField] private AudioClip sound;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }
    
    public void Start()
    {
        _btn = gameObject.GetComponent<Button>();
        if (_btn)
        {
            _btn.onClick.AddListener(OnClick);
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _canvasGroup.alpha = 0.5f;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        _canvasGroup.alpha = 0;
    }
    
    private void OnClick()
    {
        Register.Shared().DeleteOne();
        AudioManager.Shared().PlaySoundOnce(sound);
    }
}
