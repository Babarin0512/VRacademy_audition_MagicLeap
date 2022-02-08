using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//次のページに進むオブジェクトにアタッチする
public class NextButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        transform.root.gameObject.GetComponent<PushCounter>().FollowingPage();
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
