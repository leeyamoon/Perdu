using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnclickableObject : MonoBehaviour
{
    [SerializeField] private AudioClip sound;

    private void OnMouseEnter()
    {
        Cursor.Shared().OnEnterUnclickable();
    }

    private void OnMouseExit()
    {
        Cursor.Shared().OnExitUnclickable();
    }

    private void OnMouseUpAsButton()
    {
        AudioManager.Shared().PlaySoundOnce(sound);
    }
}
