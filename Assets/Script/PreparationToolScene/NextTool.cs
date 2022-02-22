using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//次のページに進むオブジェクトにアタッチする
public class NextTool : MonoBehaviour
{
    private ToolCheckCounter toolCheckCounter;
    private CheckTool checkTool;
    private int counts;
    private int imageLength;

    private TextMesh text_NextTool;

    GameObject checkMark_ON;
    bool checkBox;
    bool flg;
    int ExecutionMethod;



    private void Awake()
    {
        toolCheckCounter = this.transform.root.GetComponent<ToolCheckCounter>();// PushCounterスクリプトを取得する
        checkTool = GameObject.Find("Previous page").GetComponent<CheckTool>();// CheckToolスクリプトを取得する
    }

    void OnEnable()//ここは実行されている
    {
       
        checkBox = checkTool.checkBox;
        counts = toolCheckCounter.counts;

        if (imageLength != 0)// シーンが遷移してから最初の処理でimage[]の格納が終わる前の値で実行しないため
        {
            if (counts == imageLength)// 工具が全部揃っているのを確認できたらテキストの内容を変える
            {
                text_NextTool.text = "整備作業に移る";
            }

        }
       
    }

    
    void Start()
    {
       
        imageLength = toolCheckCounter.imageLength;//　IListの要素数を取得して変数に格納する
        text_NextTool = this.transform.Find("Text_NextTool").GetComponent<TextMesh>();
        
        checkBox = checkTool.checkBox;
        flg = checkTool.flg;
   
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

        if (counts < imageLength)// imageLengthを変更する
        {
            if(ExecutionMethod == 0)
            {
                toolCheckCounter.FollowingToolPage();
            }

            else if(ExecutionMethod == 1)
            {
                toolCheckCounter.FollowingManualPage();
            }

            checkTool.switchCheck(checkBox);//switchCheck(checkBox)を実行する
        }


        else if (counts == imageLength)
        {
            ExecutionMethod++;
            // 読み込んだマニュアル画像の数とcountsが等しい場合、工具集めや。


            // 工具のチェックがすべて完了したらimage_Manualの画像をPanelに表示する
            if(ExecutionMethod == 0)
            {
                toolCheckCounter.ChenghImage_Manual();
            }

            else if(ExecutionMethod == 1)
            {
                toolCheckCounter.chenghImage_QR();
            }
                
            text_NextTool.text = "次の整備作業に移ります";
            checkTool.text_Check.text = "作業が完了したらタッチしてください";
            return;
        }

    }

    

    

}
