using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushCounter : MonoBehaviour
{
    public Text _text;
    public int counts;
    // Start is called before the first frame update
    void Start()
    {
        counts = 0;
        _text = GameObject.Find("PushCounter").GetComponent<Text>();
        _text.text = counts.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
         _text.text = counts.ToString();

     
    
        
    }
       void OnTriggerEnter(Collider other)
    {
        counts += 1;
       
    }

    
}
