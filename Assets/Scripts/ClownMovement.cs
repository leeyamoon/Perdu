using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ClownMovement : MonoBehaviour
{
    private const float UpZoneInventoryYPos = 14;
    private const float DownZoneInventoryYPos = -16;
    private const float ImpossibleCursorYPos = 10000;

    [SerializeField, Min(0.1f)] private float speedOfWalking;
    [SerializeField] private bool isUp;
    [SerializeField] private GameObject reflection;
    [SerializeField] private Animator reflectionAnimator;

    private static readonly int IS_MOVING = Animator.StringToHash("IsMoving");
    private IEnumerator _curWalking;
    private GameManager.Locations _currentLocation;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentLocation = isUp ? GameManager.Locations.TutorialZoneUp : GameManager.Locations.TutorialZoneDown;
    }

    public int UpdateSpriteDirection(float targetXPos, int forceDir=0)
    {
        if (forceDir == 1)
        {
            FlipToRight();
            return 1;
        }

        if (forceDir == -1)
        {
            FlipToLeft();
            return -1;
        }
        
        if (targetXPos >= transform.position.x)
        {
            FlipToRight();
            return 1;
        }
        else
        {
            FlipToLeft();
            return -1;
        }
    }

    private void FlipToRight()
    {
        Vector3 scale = transform.localScale;
        scale.x = Math.Abs(scale.x);
        transform.localScale = scale;
            
        Vector3 reflactionScale = reflection.transform.localScale;
        reflactionScale.x = scale.x;
        reflection.transform.localScale = reflactionScale;
       
    }

    private void FlipToLeft()
    {
        Vector3 scale = transform.localScale;
        scale.x = Math.Abs(scale.x) * -1;
        transform.localScale = scale;
            
        Vector3 reflactionScale = reflection.transform.localScale;
        reflactionScale.x = scale.x;
        reflection.transform.localScale = reflactionScale;
    }

    public void SetCurrentLocation(GameManager.Locations location)
    {
        _currentLocation = location;
    }

    public bool CanAccessLocation(float xPos, float yPos = ImpossibleCursorYPos)
    {
        var curRange = GameManager.Shared().GetRangeOfLocation(_currentLocation);
        bool canAccessX = curRange[0] <= xPos && xPos <= curRange[1];
        
        if (yPos == ImpossibleCursorYPos)
        {
            return canAccessX;
        }

        return canAccessX || yPos > UpZoneInventoryYPos || yPos < DownZoneInventoryYPos;
    }
    
    public void WalkToPosition(float xPosition)
    {
        if (CanAccessLocation(xPosition) && !GameManager.Shared().GetActionActive())
        {
            if (_curWalking != null)
            {
                StopCoroutine(_curWalking);
            }
            _curWalking = WalkingToPositionCoroutine(xPosition);
            UpdateSpriteDirection(xPosition);
            StartCoroutine(_curWalking);
        }
    }

    public void WalkToItemAndUse(float xPosition, Action funcToRunAfter)
    {
        if (!GameManager.Shared().GetActionActive() && CanAccessLocation(xPosition))
        {
            UpdateSpriteDirection(xPosition);
            StartCoroutine(WalkToItemAndUseCoroutine(xPosition, funcToRunAfter));
        }
    }

    private IEnumerator WalkingToPositionCoroutine(float xPosition) //can't interrupt walking
    {
        Vector3 currentPos = transform.position;
        Vector3 newPos = new Vector3(xPosition, currentPos.y, currentPos.z);
        float t = 0f;
        float factorToMovement = Math.Abs(newPos.x - currentPos.x);
        if (factorToMovement == 0)
        {
            yield break;
        }
        
        _animator.SetBool(IS_MOVING, true);
        reflectionAnimator.SetBool(IS_MOVING, true);
        
        while (t < 1) 
        {
            t += Time.fixedDeltaTime * speedOfWalking/factorToMovement;
            transform.position = Vector3.Lerp(currentPos, newPos, t); 
            yield return null;
        }
        
        _animator.SetBool(IS_MOVING, false);
        reflectionAnimator.SetBool(IS_MOVING, false);
    }
    
    private IEnumerator WalkToItemAndUseCoroutine(float xPosition, Action funcToRunAfter)
    {
        if (GameManager.Shared().GetActionActive()) yield break;
        if(_curWalking != null)
            StopCoroutine(_curWalking);
        GameManager.Shared().SetIsActionActive(true);
        yield return WalkingToPositionCoroutine(xPosition);
        GameManager.Shared().SetIsActionActive(false);
        funcToRunAfter.Invoke();
    }
}
