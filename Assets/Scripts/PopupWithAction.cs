using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PopupWithAction : MonoBehaviour
{
    [SerializeField] private PopupAction action;

    public void SubscribeSuccessCallback(Action func)
    {
        action.SetOnSuccessCallBack(func);
    }
}
