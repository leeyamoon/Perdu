using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuppetShow : MonoBehaviour
{
    private int[] _nums = new int[4];

    [SerializeField] private Sprite[] puppetSprites;
    [SerializeField] private PopupAction action;

    private void Start()
    {
        GameManager.Shared().SetIsPopup(true);
    }

    public Sprite GetSpriteByIndex(int id, int index)
    {
        _nums[id] = index;
        return puppetSprites[index];
    }

    public void OnPuppetChange()
    {
        if (_nums[0] == 0 && _nums[1] == 1 && _nums[2] == 2 && _nums[3] == 3)
        {
            action.OnSuccess();
        }
    }
}
