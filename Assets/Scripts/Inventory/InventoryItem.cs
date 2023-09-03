using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryItem : MonoBehaviour
{
    private const float UpZoneMiddleYPos = 9;
    private const float DownZoneMiddleYPos = -11;
    
    private bool isInInventory;
    private GameObject initParent;
    private Vector3 posInInventory;
    private float sizeRatio;
    private bool isFlipped;
    
    private GameObject selectedObject;
    private bool isFirstDrag = true;
    private Vector3 dragOffset;
    private int sortingOrder;
    private int sortingLayerID;
    private InventoryItemTarget itemTarget;
    private SpriteRenderer spriteRenderer;
    private GameObject itemIcon;
    private CanvasGroup itemIconCanvasGroup;
    private GameManager.Direction _directionOfTarget;

    [SerializeField] private bool fromDownZone;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private float transitionDuration = 0.5f;
    [SerializeField] private AudioClip sound;


    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        sizeRatio = spriteRenderer.bounds.size.x / spriteRenderer.bounds.size.y;
    }

    void Start()
    {
        initParent = transform.parent.gameObject;
        itemTarget = target.GetComponent<InventoryItemTarget>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sortingLayerID = spriteRenderer.sortingLayerID;
        sortingOrder = spriteRenderer.sortingOrder;
        _directionOfTarget = itemTarget.GetDirection();
    }

    void Update()
    {
        if (isInInventory)
        {
            Drag();
        }
    }

    private void OnMouseUpAsButton()
    {
        if (GameManager.Shared().GetIsPopup()) return;

        if (isInInventory) return;
        
        AudioManager.Shared().PlaySoundOnce(sound);

        Vector3 iconPos = CreateInvisibleItemIcon();
        spriteRenderer.sortingLayerName = "UI";
        spriteRenderer.sortingOrder = 1;

        TransitionAnimationToInventory(iconPos);
    }

    private void TransitionAnimationToInventory(Vector3 iconPos)
    {
        Sequence mySequence = DOTween.Sequence();
        float middlePosY = GameManager.Shared().GetCurrentZone() == GameManager.Direction.UP
            ? UpZoneMiddleYPos
            : DownZoneMiddleYPos;
        mySequence.Append(transform.DOMoveY(middlePosY, transitionDuration));
        mySequence.Append(transform.DOScale(transform.localScale * 2, transitionDuration));
        mySequence.Append(transform.DOMove(iconPos, transitionDuration));
        mySequence.AppendCallback(MoveToInventory);
        mySequence.Play();
    }
    
    private Vector3 CreateInvisibleItemIcon()
    {
        itemIcon = Instantiate(iconPrefab, GameManager.Shared().InventoryContentContext.transform);
        itemIcon.GetComponent<Image>().sprite = spriteRenderer.sprite;
        itemIcon.GetComponent<ItemIcon>().SetItem(gameObject);
        itemIconCanvasGroup = itemIcon.GetComponent<CanvasGroup>();
        itemIconCanvasGroup.alpha = 0;
        
        // scale according to ratio
        Vector3 scale = itemIcon.transform.localScale;
        if (sizeRatio >= 1)
        {
            scale.y = scale.x / sizeRatio;
        }
        else
        {
            scale.x = scale.y * sizeRatio;
        }
        
        itemIcon.transform.localScale = scale;
        
        return itemIcon.transform.position;
    }

    private void MoveToInventory()
    {
        itemIconCanvasGroup.alpha = 1;
        selectedObject = gameObject;
        gameObject.SetActive(false);

        Vector3 scale = transform.localScale;
        scale /= 2;
        transform.localScale = scale;

        // important for recognizing the target item on drag
        Vector3 pos = transform.position;
        pos.z = 1;
        transform.position = pos;
        
        isInInventory = true;
    }
    
    private void MoveToTarget()
    {
        transform.SetParent(initParent.transform);
        transform.position = target.transform.position;
        spriteRenderer.sortingLayerID = sortingLayerID;
        spriteRenderer.sortingOrder = sortingOrder;
        itemIcon.SetActive(false);
        Destroy(itemIcon);
        itemTarget.ItemReach(gameObject);
    }

    private void OnDragRelease()
    {
        Vector3 mousePosition = GameManager.Shared().mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D targetObjectCol = Physics2D.OverlapPoint(mousePosition);
        Cursor.Shared().OnRelease();
        
        if (targetObjectCol)
        {
            GameObject targetObject = targetObjectCol.transform.gameObject;
            if (targetObject.GetInstanceID() == target.GetInstanceID() && 
                GameManager.Shared().GetClownMovement(_directionOfTarget).CanAccessLocation(targetObject.transform.position.x))
            {
                //todo check if works

                selectedObject = null;
                MoveToTarget();
            }
            else
            {
                SwitchItemWithIcon();
            }
        }
        else
        {
            SwitchItemWithIcon();
        }
    }

    private void Drag()
    {
        if (isFirstDrag)
        {
            isFirstDrag = false;
            Vector3 mousePosition = GameManager.Shared().mainCamera.ScreenToWorldPoint(Input.mousePosition);
            dragOffset = selectedObject.transform.position - mousePosition;
        }
        if (selectedObject)
        {
            Vector3 mousePosition = GameManager.Shared().mainCamera.ScreenToWorldPoint(Input.mousePosition);
            selectedObject.transform.position = mousePosition + dragOffset;
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            OnDragRelease();
            isFirstDrag = true;
        }
    }

    private void SwitchItemWithIcon()
    {
        gameObject.transform.position = itemIcon.transform.position;
        gameObject.SetActive(false);
        itemIcon.SetActive(true);
        itemIconCanvasGroup.alpha = 1;
    }
    
    public void SwitchIconWithItem()
    {
        UpdateSpriteDirection();
        gameObject.transform.position = itemIcon.transform.position;
        gameObject.SetActive(true);
        itemIconCanvasGroup.alpha = 0;
        Cursor.Shared().OnGrab();
    }
    
    private void UpdateSpriteDirection()
    {
        bool toFlipUp = fromDownZone && GameManager.Shared().GetCurrentZone() == GameManager.Direction.UP;
        bool toFlipDown = !fromDownZone && GameManager.Shared().GetCurrentZone() == GameManager.Direction.DOWN;

        if ((toFlipUp || toFlipDown) && !isFlipped)
        {
            Vector3 scale = transform.localScale;
            scale.y *=  -1;
            scale.x *= -1;
            transform.localScale = scale;
            isFlipped = true;
        }
        else if (!toFlipUp && !toFlipDown && isFlipped)
        {
            Vector3 scale = transform.localScale;
            scale.y *=  -1;
            scale.x *= -1;
            transform.localScale = scale;
            isFlipped = false;
        }
    }
}
