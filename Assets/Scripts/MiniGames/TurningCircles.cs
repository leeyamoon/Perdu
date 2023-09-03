using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurningCircles : MonoBehaviour
{
    private int[] _nums = new int[4];

    [SerializeField] private int slices;
    [SerializeField] private PopupAction action;

    private void Start()
    {
        GameManager.Shared().SetIsPopup(true);
    }

    public int GetSlices()
    {
        return slices;
    }

    public void UpdateCircleIndex(int id, int index)
    {
        _nums[id] = index;
    }

    public void OnCircleChange()
    {
        if (_nums[0] == 0 && _nums[1] == 0 && _nums[2] == 0 && _nums[3] == 0)
        {
            action.OnSuccess();
        }
    }
}