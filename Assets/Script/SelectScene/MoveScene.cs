using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{

    private MLQRCodeSample _mlQRCodeSample;
    
    void Start()
    {
        // MLQRCodeSample.csコンポーネントを取得する
        _mlQRCodeSample = GameObject.FindGameObjectWithTag("mlQRCodeSample").GetComponent<MLQRCodeSample>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        
    }
}
