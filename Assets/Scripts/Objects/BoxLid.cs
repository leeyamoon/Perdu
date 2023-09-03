using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLid : MonoBehaviour
{

    [SerializeField] private InventoryItemTarget target;
    [SerializeField] private GameObject scissors;
    [SerializeField] private SpriteRenderer firstSpriteRenderer;
    [SerializeField] private SpriteRenderer secondSpriteRenderer;
    [SerializeField] private SpriteRenderer thirdSpriteRenderer;
    [SerializeField] private SpriteRenderer scissorsSpriteRenderer;
    [SerializeField] private float speed;

    private void Start()
    {
        target.OnItemReach += OnItemReach;
    }
    

    private IEnumerator ProAnimation()
    {
        float progress = 0;
        Color transparentColor = Color.white;
        transparentColor.a = 0;
        while (progress < 1)
        {
            firstSpriteRenderer.color = Color.Lerp(Color.white, transparentColor, progress);
            secondSpriteRenderer.color = Color.Lerp(transparentColor, Color.white, progress);
            progress += Time.deltaTime * speed;
            yield return null;
        }
        progress = 0;
        yield return new WaitForSeconds(0.2f);
        scissorsSpriteRenderer.color = transparentColor;
        scissors.SetActive(true);
        while (progress < 1)
        {
            secondSpriteRenderer.color = Color.Lerp(Color.white, transparentColor, progress);
            thirdSpriteRenderer.color = Color.Lerp(transparentColor, Color.white, progress);
            scissorsSpriteRenderer.color = thirdSpriteRenderer.color;
            progress += Time.deltaTime * speed;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void OnItemReach()
    {
        StartCoroutine(ProAnimation());
    }
}
