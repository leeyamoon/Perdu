using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] private bool isUp;
    [SerializeField] private ClownMovement currentClownMovement;
    [SerializeField] private GameObject otherClown;
    [SerializeField] private CinemachineStateDrivenCamera cameraController;
    [SerializeField] private GameManager.Locations otherLocation;
    [SerializeField] private AudioClip mirrorUsingSound;
    private ClownMovement _otherClownMovement;
    private static readonly int IS_UP = Animator.StringToHash("isUp");
    private static readonly float EXTRA_TIME_TO_WAIT = 0.5f;

    private void Awake()
    {
        _otherClownMovement = otherClown.GetComponent<ClownMovement>();
    }

    private void UseMirror()
    {
        GameManager.Direction zone = isUp ? GameManager.Direction.DOWN : GameManager.Direction.UP;
        GameManager.Shared().SetCurrentZone(zone);
        
        cameraController.m_AnimatedTarget.SetBool(IS_UP, !isUp);
        GameManager.Shared().InActionForSeconds(cameraController.m_CustomBlends.m_CustomBlends[0].m_Blend.m_Time + EXTRA_TIME_TO_WAIT);
        AudioManager.Shared().PlaySoundOnce(mirrorUsingSound);
        Cursor.Shared().curDirection = isUp ? GameManager.Direction.DOWN : GameManager.Direction.UP;
    }

    private void OnMouseDown()
    {
         if (GameManager.Shared().GetIsPopup()) return;
        
        _otherClownMovement.SetCurrentLocation(otherLocation);
        Vector3 tmpOtherLoc = otherClown.transform.position;
        Vector3 newLoc = new Vector3(transform.position.x, tmpOtherLoc.y, tmpOtherLoc.z);
        otherClown.transform.position = newLoc;
        int dir = currentClownMovement.UpdateSpriteDirection(newLoc.x);
        _otherClownMovement.UpdateSpriteDirection(newLoc.x, dir);
        currentClownMovement.WalkToItemAndUse(newLoc.x, UseMirror);
    }
}