using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text : MonoBehaviour
{
    private Text count;
    PushCounter pushCounter;
    public int _counts;
    // Start is called before the first frame update
    void Start()
    {
        this.count = this.GetComponent<Text>();
        _counts = pushCounter.counts;


        
    }

    // Update is called once per frame
    void Update()
    {

        this.count.text = _counts.ToString(); 
        
    }
}
