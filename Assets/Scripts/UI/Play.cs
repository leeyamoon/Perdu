using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private OpeningAnimation openingAnimation;

    public void OnPointerClick(PointerEventData eventData)
    {
        openingAnimation.ChangeToStart();
    }
}
