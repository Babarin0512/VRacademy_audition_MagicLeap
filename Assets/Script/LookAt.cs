using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    private GameObject _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = GameObject.Find("MainCamera");
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_mainCamera.transform);


        
    }
}
