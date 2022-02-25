using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTool : MonoBehaviour
{
    
    private GameObject followingPage;

    public bool checkBox;// チェックマークの切り替え判定
    public bool flg;
    GameObject checkMarkOff;
    GameObject checkMarkOn;
    GameObject text_check;
    public TextMesh text_Check;
    

    void Awake()
    {
        followingPage = GameObject.Find("FollowingPage");
        text_Check = GameObject.Find("Text_Check").GetComponent<TextMesh>();
        checkMarkOff = GameObject.Find("CheckMark_OFF");
        checkMarkOn = GameObject.Find("CheckMark_ON");
        text_check = GameObject.Find("Text_Check");

        checkBox = false;
    }

  
    void Start()
    {
             
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;

        followingPage.SetActive(false);
        checkMarkOff.SetActive(true);
        checkMarkOn.SetActive(false);
    }

    private void Update()
    {
        TextMesh text_Check = GameObject.Find("Text_Check").GetComponent<TextMesh>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(flg == true)
        {
            checkBox = true;
        }

        else 
        {
            checkBox = false;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    private void OnTriggerExit(Collider other)
    {
        // 指先が物体から離れたらチェックマークの切り替えをする。
        // NextToolオブジェクトをアクティブ・非アクティブを切り替える
        
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        

        switchCheck(checkBox);// チェックマークのON・OFFを切り替える

    }

    public void switchCheck(bool checkBox)
    {
        if (checkBox == false)
        {
            flg = true;
            checkMarkOff.SetActive(false);
            checkMarkOn.SetActive(true);
            followingPage.SetActive(true);
            text_check.SetActive(false);
        }
        else if (checkBox == true)
        {
            flg = false;
            checkMarkOff.SetActive(true);
            checkMarkOn.SetActive(false);
            followingPage.SetActive(false);
            text_check.SetActive(true);
           
        }
    }

   
}