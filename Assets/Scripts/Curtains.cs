using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : MonoBehaviour
{
    [SerializeField] private Animator animator;


    private void Start()
    {
        Open();
    }

    public void Open()
    {
        StartCoroutine(OpenCor());
    }
    
    public void Close()
    {
        gameObject.SetActive(true);
        animator.SetBool("IsClosing", true);
        animator.SetBool("IsOpening", false);
    }


    private IEnumerator OpenCor()
    {
        gameObject.SetActive(true);
        animator.SetBool("IsOpening", true);
        animator.SetBool("IsClosing", false);
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    public void SetToStartOpen()
    {
        animator.SetBool("IsOpening", true);
        animator.SetBool("IsClosing", false);
    }
    
    public void OnOpeningEnd()
    {
        
    }
    
    public void OnClosingEnd()
    {
        
    }
}
