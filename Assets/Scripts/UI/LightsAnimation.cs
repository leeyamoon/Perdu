using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightsAnimation : MonoBehaviour
{
    [SerializeField]  private Sprite[] sprites;
    [SerializeField] private int spritePerFrame = 6;
    [SerializeField] private bool loop = true;

    private int index = 0;
    private Image image;
    private int frame = 0;

    private void Awake() {
        image = GetComponent<Image> ();
    }

    //Switch between sprites every spritePerFrame frames.
    private void FixedUpdate () {
        if (!loop && index == sprites.Length) return;
        frame ++;
        if (frame < spritePerFrame) return;
        image.sprite = sprites [index];
        frame = 0;
        index ++;
        if (index >= sprites.Length) {
            if (loop) index = 0;
        }
    }

}
