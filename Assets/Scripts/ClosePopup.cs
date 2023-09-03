using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePopup : MonoBehaviour
{
    [SerializeField] private GameObject popup;

    private Button btn;
    
    public void Start()
    {
        btn = gameObject.GetComponent<Button>();
        if (btn)
        {
            btn.onClick.AddListener(OnClick);
        }
    }
    
    private void OnClick()
    {
        Destroy(popup);
        // GameManager.Shared().SetIsActionActive(true);
        GameManager.Shared().SetIsPopup(false);
    }
}
