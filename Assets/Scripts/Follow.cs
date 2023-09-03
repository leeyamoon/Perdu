using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private float _initPosX;
    private float _initTargetPosX;
    
    [SerializeField] private GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        _initPosX = transform.position.x;
        _initTargetPosX = target.gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = target.transform.position.x + _initPosX - _initTargetPosX;
        transform.position = pos;
    }
}
