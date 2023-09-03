using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Parallax : MonoBehaviour
{
    private float _width;
    private float _initPosX;

    public GameObject mainCamera;
    public float effect;
    
    // Start is called before the first frame update
    void Start()
    {
        _initPosX = transform.position.x;
        _width = GetComponent<SpriteRenderer>().bounds.size.x; // todo: change to sprite renderer
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        float temp = mainCamera.transform.position.x * (1 - effect);
        float distance = mainCamera.transform.position.x * effect;
        
        transform.position = new Vector3(_initPosX + distance, transform.position.y, transform.position.z);

        // // todo: fix
        if (temp > _initPosX + _width) _initPosX += _width;
        else if (temp < _initPosX - _width) _initPosX -= _width;
    }
}
