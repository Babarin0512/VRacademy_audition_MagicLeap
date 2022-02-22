using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorkCounter : MonoBehaviour
{
    Text _text;
    public int counts;

    private GameObject _mainCamera;//MainCameraを取得する
    private Quaternion quaternion;


    private Sprite[] image;//Resouseフォルダから読み込んだ画像を格納する配列
    private Image _panel;
    private GameObject imageObject;//imageをGameObjectとして代入する変数

    public int imageLength;

    private GameObject followingPage;
    private GameObject checkMarkOn;
    private GameObject checkMarkOff;
    //private bool checkBox;// チェックボックスにチェックが入ってるかどうか判定する
    private GameObject text_NextTool;
    private GameObject text_Check;


    void Awake()
    {
        counts = 0;
        //image[]にResouseフォルダ内の画像を格納する
        image = Resources.LoadAll<Sprite>("Manual");
        _text = GameObject.Find("PushCounter").GetComponent<Text>();

    }

    void Start()
    {



        //image[]にResouseフォルダ内の画像を格納する
        //image = Resources.LoadAll<Sprite>("Sprites");
        //PanelのImageコンポーネントを取得する
        _panel = GameObject.Find("Panel").GetComponent<Image>();

        //imageを生成する
        _panel.sprite = image[0];

        imageLength = image.Length - 1;


    }

    // Update is called once per frame
    void Update()
    {

        _text.text = "完了した作業:" + counts.ToString() + "/" + imageLength.ToString();



        if (counts > image.Length - 1)
        {
            counts = 0;
            _panel.sprite = image[0];
        }
        else if (counts <= 0)
        {
            counts = 0;
            _panel.sprite = image[0];
        }

    }

    public void FollowingPage()
    {
        counts += 1;
        _panel.sprite = image[counts];


    }

}
