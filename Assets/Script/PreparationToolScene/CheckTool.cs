using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTool : MonoBehaviour
{
    private bool nextButton;
    private GameObject followingPage;

    bool checkBox;// チェックマークの切り替え判定
    GameObject checkMarkOff;
    GameObject checkMarkOn;

    void Awake()
    {
        followingPage = GameObject.Find("FollowingPage");
        nextButton = false;

        checkBox = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        checkMarkOff = GameObject.Find("CheckMark_OFF");
        checkMarkOn = GameObject.Find("CheckMark_ON");
        followingPage.SetActive(false);
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        
        checkMarkOff.SetActive(true);
        checkMarkOn.SetActive(false);

        TextMesh text_Check = GameObject.Find("Text_Check").GetComponent<TextMesh>();

    }

    private void Update()
    {
        TextMesh text_Check = GameObject.Find("Text_Check").GetComponent<TextMesh>();
        //text_Check.text = "テスト";
    }



    private void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    private void OnTriggerExit(Collider other)
    {
        // 指先が物体から離れたらチェックマークの切り替えをする。
        // NextToolオブジェクトをアクティブ・非アクティブを切り替える
        TextMesh text_NextTool = GameObject.Find("Text_NextTool").GetComponent<TextMesh>();
        TextMesh text_Check = GameObject.Find("Text_Check").GetComponent<TextMesh>();

        this.gameObject.GetComponent<Renderer>().material.color = Color.white;

        if(!checkBox)
        {
            checkBox = true;
            checkMarkOff.SetActive(false);
            checkMarkOn.SetActive(true);
            followingPage.SetActive(true);
            text_Check.text = null;
            //text_NextTool.text = "次に用意する工具を表示します"; 
        }
        else if(checkBox)
        {
            checkBox = false;
            checkMarkOff.SetActive(true);
            checkMarkOn.SetActive(false);
            followingPage.SetActive(false);
            text_Check.text = "用意ができたら押してください";
        }
        
        

    }

   
}