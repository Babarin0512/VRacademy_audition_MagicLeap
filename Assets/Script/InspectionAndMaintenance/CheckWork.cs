using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWork : MonoBehaviour
{
    private GameObject followingPage;

    public bool checkBox;// チェックマークの切り替え判定
    public bool flg;
    GameObject checkMarkOff;
    GameObject checkMarkOn;
    TextMesh text_Check;

    void Awake()
    {
        followingPage = GameObject.Find("FollowingPage");


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

        text_Check = GameObject.Find("Text_Check").GetComponent<TextMesh>();

    }

    private void Update()
    {
        TextMesh text_Check = GameObject.Find("Text_Check").GetComponent<TextMesh>();
        text_Check.text = checkBox.ToString();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (flg == true)
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
            text_Check.text = null;
        }
        else if (checkBox == true)
        {
            flg = false;
            checkMarkOff.SetActive(true);
            checkMarkOn.SetActive(false);
            followingPage.SetActive(false);
            text_Check.text = "作業が完了したら\n押してください";
        }
    }

}
