using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousPage : MonoBehaviour
{
    private bool nextButton;
    private GameObject followingPage;

    void Awake()
    {
         followingPage = GameObject.Find("FollowingPage");
         nextButton = false;
    }
   
    // Start is called before the first frame update
    void Start()
    {
        followingPage.SetActive(false);
        this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    private void OnTriggerExit(Collider other)
    {
        followingPage.SetActive(true);
        transform.root.gameObject.GetComponent<PushCounter>().PreviousPage();
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;

    }
}
