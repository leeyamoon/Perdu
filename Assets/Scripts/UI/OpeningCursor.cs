using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursorIdleTexture2D;
    [SerializeField] private Texture2D cursorClickTexture2D;

    private bool _onClick;
    // Start is called before the first frame update
    private void Start()
    {
        _onClick = false;
        UnityEngine.Cursor.SetCursor(cursorIdleTexture2D, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !_onClick)
        {
            _onClick = true;
            UnityEngine.Cursor.SetCursor(cursorClickTexture2D, Vector2.zero, CursorMode.ForceSoftware);
        }
        else if (_onClick && !Input.GetMouseButton(0))
        {
            _onClick = false;
            UnityEngine.Cursor.SetCursor(cursorIdleTexture2D, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
