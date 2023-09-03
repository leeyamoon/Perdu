using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningAnimation : MonoBehaviour
{
    [SerializeField]  private Sprite[] spritesLoop;
    [SerializeField]  private Sprite[] spritesForStart;
    [SerializeField, Min(1)] private int spritePerSeconds = 6;
    [SerializeField] private Curtains curtains;
    
    private Image image;

    private bool _passToPlay;

    private void Awake() {
        image = GetComponent<Image> ();
    }

    private void Start()
    {
        StartCoroutine(Animation());
    }

    public void ChangeToStart()
    {
        _passToPlay = true;
    }

    private IEnumerator Animation()
    {
        while (!_passToPlay)
        {
            foreach (var sprite in spritesLoop)
            {
                image.sprite = sprite;
                yield return new WaitForSeconds(1f / spritePerSeconds);
            }
        }
        foreach (var sprite in spritesForStart)
        {
            image.sprite = sprite;
            yield return new WaitForSeconds(1f / spritePerSeconds);
        }
        curtains.Close();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    
    
}
