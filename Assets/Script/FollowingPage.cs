using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//次のページに進むオブジェクトにアタッチする
public class FollowingPage : MonoBehaviour
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
        transform.root.gameObject.GetComponent<PushCounter>().FollowingPage();
    }
}
