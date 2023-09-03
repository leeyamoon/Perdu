using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingBubble : MonoBehaviour
{
    [SerializeField] private GameObject bubble;
    [SerializeField, Min(0.1f)] private float timeToShow;
    [SerializeField, Min(0.01f)] private float cooldownTime;

    private bool _inCooldown;

    private void Start()
    {
        _inCooldown = false;
    }

    private void OnMouseDown()
    {
        if(_inCooldown)
            return;
        StartCoroutine(ShowBubble());
    }

    private IEnumerator ShowBubble()
    {
        _inCooldown = true;
        bubble.SetActive(true);
        yield return new WaitForSeconds(timeToShow);
        bubble.SetActive(false);
        yield return new WaitForSeconds(cooldownTime);
        _inCooldown = false;
    }
}
