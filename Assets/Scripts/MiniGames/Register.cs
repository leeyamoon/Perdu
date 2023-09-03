using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    private static Register _register;
    [SerializeField] private Image[] placesForNums;
    private int[] _nums;
    private int _index;
    
    private void Awake()
    {
        if (_register == null)
        {
            _register = this;
            _nums = new[] { 0, 0, 0 };
            _index = 0;
        }
    }

    public static Register Shared()
    {
        return _register;
    }

    public void AddNum(Sprite sprite, int num)
    {
        if (_index == 3)
            return;
        _nums[_index] = num;
        placesForNums[_index].sprite = sprite;
        placesForNums[_index].gameObject.SetActive(true);
        _index++;
    }

    public void CheckWinning(Action winningAction)
    {
        if (_nums[0] == 2 && _nums[1] == 9 && _nums[2] == 5)
        {
            winningAction.Invoke();
        }
    }

    public void DeleteOne()
    {
        if(_index == 0)
            return;
        _index--;
        placesForNums[_index].sprite = null;
        placesForNums[_index].gameObject.SetActive(false);
        _nums[_index] = 0;
    }
}
