using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private ClownMovement clownMovement;
    private void OnMouseDown()
    {
        if (GameManager.Shared().GetIsPopup()) return;
        
        clownMovement.WalkToPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
    }
}
