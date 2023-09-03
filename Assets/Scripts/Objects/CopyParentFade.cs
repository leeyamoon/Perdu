using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyParentFade : MonoBehaviour
{
    [SerializeField] private SpriteRenderer parentSpriteRenderer;
    private SpriteRenderer _mySpriteRenderer;

    private void Awake()
    {
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _mySpriteRenderer.color = parentSpriteRenderer.color;
    }
}
