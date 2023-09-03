using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puppet : MonoBehaviour
{
    private Button _btn;
    private int _spriteIndex;

    [SerializeField] private int id;
    [SerializeField] private int initSpriteIndex;
    [SerializeField] private PuppetShow show;
    [SerializeField] private Image image;
    [SerializeField] private AudioClip sound;

    public void Start()
    {
        _spriteIndex = initSpriteIndex;
        _btn = gameObject.GetComponent<Button>();
        image.sprite = show.GetSpriteByIndex(id, _spriteIndex);

        if (_btn)
        {
            _btn.onClick.AddListener(OnClick);
        }
    }
    
    private void OnClick()
    {
        _spriteIndex = (_spriteIndex + 1) % 4;
        image.sprite = show.GetSpriteByIndex(id, _spriteIndex);
        AudioManager.Shared().PlaySoundOnce(sound);
        
        show.OnPuppetChange();
    }
}
