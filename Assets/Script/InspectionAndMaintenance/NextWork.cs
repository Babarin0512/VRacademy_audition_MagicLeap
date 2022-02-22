using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextWork : MonoBehaviour
{
    private WorkCounter workCounter;
    private CheckWork checkWork;
    private int counts;
    private int imageLength;

    private TextMesh text_NextTool;

    GameObject checkMark_ON;
    bool checkBox;
    bool flg;

    void Awake()
    {
        workCounter = this.transform.root.GetComponent<WorkCounter>();// PushCounterスクリプトを取得する
        checkWork = GameObject.Find("Previous page").GetComponent<CheckWork>();// CheckToolスクリプトを取得する
    }

    void OnEnable()//ここは実行されている
    {

        checkBox = checkWork.checkBox;
        counts = workCounter.counts;

        if (imageLength != 0)// シーンが遷移してから最初の処理でimage[]の格納が終わる前の値で実行しないため
        {
            if (counts == imageLength)// 工具が全部揃っているのを確認できたらテキストの内容を変える
            {
                text_NextTool.text = "点検整備を完了します";
            }

        }

    }

    void Start()
    {

        imageLength = workCounter.imageLength;
        text_NextTool = this.transform.Find("Text_NextTool").GetComponent<TextMesh>();

        checkBox = checkWork.checkBox;
        flg = checkWork.flg;

    }


    private void OnTriggerEnter(Collider other)
    {
        checkBox = true;
    }

    private void OnTriggerStay(Collider other)
    {

        this.gameObject.GetComponent<Renderer>().material.color = Color.green;

    }

    private void OnTriggerExit(Collider other)
    {
        // counterを+1にする
        // 自身を非アクティブにする
        // counterの最後の数になら整備シーンに変更する


        this.gameObject.GetComponent<Renderer>().material.color = Color.white;

        if (counts < imageLength)
        {
            transform.root.gameObject.GetComponent<WorkCounter>().FollowingPage();
            checkWork.switchCheck(checkBox);//switchCheck(checkBox)を実行する
        }


        else if (counts == imageLength)
        {
            // 読み込んだマニュアル画像の数とcountsが等しい場合、シーンを変更する。
            SceneManager.LoadScene("SelectScene");// 最終的にはQRで読み込んだデータをもとにシーンを変更する
        }

    }


}






