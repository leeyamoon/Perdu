using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PopupAction : MonoBehaviour
{
    private Action _onSuccessCallback;

    [SerializeField] protected GameObject popup;

    public void SetOnSuccessCallBack(Action cb)
    {
        _onSuccessCallback = cb;
    }

    public void OnSuccess()
    {
        Destroy(popup);
        GameManager.Shared().SetIsPopup(false);
        _onSuccessCallback();
    }
}
