using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimationEnding : MonoBehaviour
{
    [SerializeField]  private Sprite[] spritesForStart;
    [SerializeField, Min(1)] private int spritePerSeconds = 6;

    private Image image;
    

    private void Awake() {
        image = GetComponent<Image> ();
    }

    private void Start()
    {
        StartCoroutine(Animation());
    }
    

    private IEnumerator Animation()
    {
        foreach (var sprite in spritesForStart)
        {
            image.sprite = sprite;
            yield return new WaitForSeconds(1f / spritePerSeconds);
        }
        SceneManager.LoadScene("Opening", LoadSceneMode.Single);
    }
}
