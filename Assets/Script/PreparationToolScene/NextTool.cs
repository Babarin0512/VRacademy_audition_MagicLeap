using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//次のページに進むオブジェクトにアタッチする
public class NextTool : MonoBehaviour
{
    //private ToolCheckCounter toolCheckCounter;
    //private CheckTool checkTool;
    [SerializeField] CheckTool checkTool;
    [SerializeField] ToolCheckCounter toolCheckCounter;

    private int counts;
    private int imageLength;

    private TextMesh text_NextTool;

    GameObject checkMark_ON;
    bool checkBox;
    bool flg;
    bool flag;
    int executionMethod;

   int count_tool;



    private void Awake()
    {
        count_tool = toolCheckCounter.count_Tool;
    }

    void OnEnable()//ここは実行されている
    {
       
        checkBox = checkTool.checkBox;
        counts = toolCheckCounter.counts;
        imageLength = toolCheckCounter.imageLength;
        executionMethod = toolCheckCounter.ExecutionMethod;

        if (imageLength != 0)// シーンが遷移してから最初の処理でimage[]の格納が終わる前の値で実行しないため
        {
           
            if (toolCheckCounter.count_Tutorial == imageLength)// 工具が全部揃っているのを確認できたらテキストの内容を変える*count_Toolなどでじょうけんつける
            {
                text_NextTool.text = null;
                
            }

        
           
            else if(toolCheckCounter.count_Tool == imageLength)
            {
                text_NextTool.text = "整備作業に移行します";
                //executionMethod = 1;
            }

            else if(toolCheckCounter.count_Manual == imageLength)
            {
                text_NextTool.text = "整備を終了します";
                //executionMethod = 2;
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
            if (executionMethod == 0)
            {
                toolCheckCounter.FollowingTutorialPage();
            }

            if (executionMethod == 1)
            {
                toolCheckCounter.FollowingToolPage();
            }

            else if(executionMethod == 2)
            {
                toolCheckCounter.FollowingManualPage();
            }

            checkTool.switchCheck(checkBox);//switchCheck(checkBox)を実行する
        }


        else if (counts == imageLength)
        {

            // 読み込んだマニュアル画像の数とcountsが等しい場合、工具集めや。
            if (executionMethod == 0)
            {
                //toolCheckCounter.FollowingTutorialPage();
                return;
                

            }

            // 工具のチェックがすべて完了したらimage_Manualの画像をPanelに表示する
            if (executionMethod == 1)
            {
                toolCheckCounter.ChenghImage_Manual();
                executionMethod = 2;
                checkTool.switchCheck(checkBox);
            }

            else if(executionMethod == 2)
            {
                toolCheckCounter.chenghImage_QR();
                executionMethod = 0;
                checkTool.switchCheck(checkBox);
            }
            
        }

    }

    

    

}
