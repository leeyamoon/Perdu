using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endgame : MonoBehaviour
{
    [SerializeField, Min(0.5f)] private float justSpinningTime;
    [SerializeField] private Animator animatorStar;
    [SerializeField] private GameObject player;


    private void OnEnable()
    {
        GameManager.Shared().SetIsActionActive(true);
        player.GetComponent<SpriteRenderer>().color = Color.clear;
        animatorStar.StopPlayback();
        StartCoroutine(EndGameCoroutine());    
    }
                                             
    private IEnumerator EndGameCoroutine()
    {
        yield return new WaitForSeconds(justSpinningTime);
        animatorStar.Play("star apearing");
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("EndGame", LoadSceneMode.Single);
    }
}
