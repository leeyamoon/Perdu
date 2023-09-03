using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    enum CursorState {
            Idle,
            OutOfRange,
            Grab,
            // Clickable
    }
    
    [SerializeField] private Texture2D cursorIdle;
    [SerializeField] private Texture2D cursorClick;
    [SerializeField] private Texture2D cursorClickError;
    [SerializeField] private Texture2D cursorGrab;
    [SerializeField] private Texture2D cursorOutOfLimit;
    // [SerializeField] private Texture2D cursorClickable; //todo Rony
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip errorSound;

    [NonSerialized] public GameManager.Direction curDirection;
    private CursorState _state;
    private float _curXPosition;
    private float _curYPosition;
    private static Cursor _self;
    private Camera _mainCamera;
    private bool _isDuringGrab;
    private bool _isDuringUnclickableHover;
    private bool _isInPopup;

    private void Awake()
    {
        if (_self == null)
        {
            _self = this;
        }
    }

    public static Cursor Shared()
    {
        return _self;
    }

    // Start is called before the first frame update
    private void Start()
    {
        curDirection = GameManager.Direction.UP;
        UnityEngine.Cursor.SetCursor(cursorIdle, Vector2.zero, CursorMode.ForceSoftware);
        _state = GameManager.Shared().GetClownMovement(curDirection)
            .CanAccessLocation(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y)
            ? CursorState.Idle
            : CursorState.OutOfRange;

        _mainCamera = Camera.main; // prevents expensive access in update
    }

    // Update is called once per frame
    void Update()
    {
        _curXPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
        _curYPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition).y;

        if (_isDuringGrab || _isDuringUnclickableHover)
        {
            return;
        }

        if (_isInPopup)
        {
            HandleIdleCursorClick();
            return;
        }

        HandleRangeCursor();

        HandleIdleCursorClick();
        
        HandleOutOfRangeCursorClick();
    }

    private void HandleRangeCursor()
    {
        if (_state == CursorState.Idle &&
            !GameManager.Shared().GetClownMovement(curDirection).CanAccessLocation(_curXPosition, _curYPosition))
        {
            _state = CursorState.OutOfRange;
            UnityEngine.Cursor.SetCursor(cursorOutOfLimit, Vector2.one/2, CursorMode.ForceSoftware);
        }

        if (_state == CursorState.OutOfRange &&
            GameManager.Shared().GetClownMovement(curDirection).CanAccessLocation(_curXPosition, _curYPosition))
        {
            _state = CursorState.Idle;
            UnityEngine.Cursor.SetCursor(cursorIdle, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    private void HandleIdleCursorClick()
    {
        if (_state == CursorState.Idle && Input.GetMouseButtonDown(0))
        {
            AudioManager.Shared().PlaySoundOnce(clickSound);
            UnityEngine.Cursor.SetCursor(cursorClick, Vector2.zero, CursorMode.ForceSoftware);
        }
        
        if (_state == CursorState.Idle && Input.GetMouseButtonUp(0))
        {
            UnityEngine.Cursor.SetCursor(cursorIdle, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    private void HandleOutOfRangeCursorClick()
    {
        if (_state == CursorState.OutOfRange && Input.GetMouseButtonDown(0))
        {
            AudioManager.Shared().PlaySoundOnce(errorSound);
        }
    }

    public void OnGrab()
    {
        _isDuringGrab = true;
        _state = CursorState.Grab;
        UnityEngine.Cursor.SetCursor(cursorGrab, Vector2.zero, CursorMode.ForceSoftware);
    }
    
    public void OnRelease()
    {
        if (!_isDuringGrab) return;
        _isDuringGrab = false;
        _state = CursorState.Idle;
        UnityEngine.Cursor.SetCursor(cursorIdle, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void OnEnterUnclickable()
    {
        if (_isDuringGrab || _isInPopup) return;
        _isDuringUnclickableHover = true;
        _state = CursorState.OutOfRange;
        UnityEngine.Cursor.SetCursor(cursorOutOfLimit, Vector2.one/2, CursorMode.ForceSoftware);
    }
    
    public void OnExitUnclickable()
    {
        if (!_isDuringUnclickableHover) return;
        _isDuringUnclickableHover = false;
        _state = CursorState.Idle;
        UnityEngine.Cursor.SetCursor(cursorIdle, Vector2.zero, CursorMode.ForceSoftware);
    }
    
    public void OnOpenPopup()
    {
        _isInPopup = true;
        _state = CursorState.Idle;
        UnityEngine.Cursor.SetCursor(cursorIdle, Vector2.zero, CursorMode.ForceSoftware);
    }
    
    public void OnClosePopup()
    {
        if (!_isInPopup) return;
        _isInPopup = false;
    }
}
