using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartBtn : MonoBehaviour
{
    private Button _btn;
    
    public void Start()
    {
        _btn = gameObject.GetComponent<Button>();
        if (_btn)
        {
            _btn.onClick.AddListener(OnClick);
        }
    }
    
    private void OnClick()
    {
        GameManager.Shared().RestartGame();
    }
}
