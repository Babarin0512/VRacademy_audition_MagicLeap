using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousPage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        transform.root.gameObject.GetComponent<PushCounter>().PreviousPage();
        this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
}
