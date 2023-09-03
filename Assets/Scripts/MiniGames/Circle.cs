using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour
{
    private Button _btn;
    private int _sliceIndex;

    [SerializeField] private int id;
    [SerializeField] private int initSliceIndex;
    [SerializeField] private TurningCircles turner;
    [SerializeField] private Image image;
    [SerializeField] private AudioClip sound;

    public void Start()
    {
        _sliceIndex = initSliceIndex % turner.GetSlices();
        float xRotation = 360f / turner.GetSlices() * _sliceIndex;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, xRotation);
        turner.UpdateCircleIndex(id, _sliceIndex);

        _btn = gameObject.GetComponent<Button>();

        if (_btn)
        {
            _btn.onClick.AddListener(OnClick);
        }
    }
    
    private void OnClick()
    {
        _sliceIndex = (_sliceIndex + 1) % turner.GetSlices();
        float xRotation = 360f / turner.GetSlices() * _sliceIndex;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, xRotation);
        AudioManager.Shared().PlaySoundOnce(sound);

        turner.UpdateCircleIndex(id, _sliceIndex);
        turner.OnCircleChange();
    }
}