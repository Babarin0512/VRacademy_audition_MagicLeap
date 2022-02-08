using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScanButton : MonoBehaviour
{
    public bool ScanButton_flag;
    private MLQRCodeSample mlQRCodeSample;

    public string dataString;
    

    void Awake()
    {
        ScanButton_flag = false;
    }

    void Start()
    {
        mlQRCodeSample = GameObject.FindGameObjectWithTag("mlQRCodeSample").GetComponent<MLQRCodeSample>();
    }

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        ScanButton_flag = false;

        //mlQRCodeSample.MLInputOnButtonTap();
    }

    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        //SceneManager.LoadScene(dataString);
        ScanButton_flag = true;

        if(ScanButton_flag)
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        else 
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.green;

        }
        
        
    }

    
}
