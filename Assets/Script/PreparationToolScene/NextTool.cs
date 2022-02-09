using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//次のページに進むオブジェクトにアタッチする
public class NextTool : MonoBehaviour
{
    private PushCounter pushCounter;
    private CheckTool checkTool;
    private int counts;
    private int imageLength;

    private TextMesh text_NextTool;

    GameObject checkMark_ON;
    bool checkBox;



    private void Awake()
    {
       
    }

    private void OnEnable()//ここは実行されている
    {
        pushCounter = this.transform.root.GetComponent<PushCounter>();// PushCounterスクリプトを取得する
        counts = pushCounter.counts;
        imageLength = pushCounter.imageLength;
        text_NextTool = this.transform.Find("Text_NextTool").GetComponent<TextMesh>();
        text_NextTool.text = "test";
        checkBox = GameObject.Find("Previous page").GetComponent<CheckTool>().checkBox;

        if (imageLength == 0)
        {
            while(imageLength != 0)
            {
                imageLength = pushCounter.imageLength;

                if (counts < imageLength)
                {
                    text_NextTool.text = counts.ToString() + "/" + imageLength.ToString();//"次に用意する工具を表示します";
                }
                else if (counts == imageLength)
                {
                    text_NextTool.text = "整備作業に移る";
                }
            }
            

      

        }

        


        /*if (counts < imageLength)
        {
            text_NextTool.text = counts.ToString() + "/" + imageLength.ToString();//"次に用意する工具を表示します";
        }
        else if(counts == imageLength)
        {
            text_NextTool.text = "整備作業に移る";
        }*/
        
    }

    private void OnDisable()
    {
        text_NextTool = GameObject.Find("Text_NextTool").GetComponent<TextMesh>();
        text_NextTool.text = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        /*pushCounter = GameObject.Find("PushuCounter").GetComponent<PushCounter>();
        int imageLength;
        imageLength = pushCounter.imageLength;

        text_NextTool = GameObject.Find("Text_NextTool").GetComponent<TextMesh>();*/
        checkMark_ON = GameObject.Find("CheckMark_ON");
        
        



    }

    // Update is called once per frame
    void Update()
    {
        pushCounter = this.transform.root.GetComponent<PushCounter>();// PushCounterスクリプトを取得する
        counts = pushCounter.counts;
        imageLength = pushCounter.imageLength;

        text_NextTool.text = counts.ToString() + "/" + imageLength.ToString();// pushCounter.imageLengthを取得できているか確認する
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
        // テキストに整備にはいることを表示する
        
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;

        if (counts < imageLength)
        {
            //text_NextTool.text = null;//"次に用意する工具を表示します";
            transform.root.gameObject.GetComponent<PushCounter>().FollowingPage();
            gameObject.SetActive(false);
            checkMark_ON.SetActive(false);
            checkBox = false;
        }

       
        else if (counts == imageLength)
        {
            // 読み込んだマニュアル画像の数と最後までcountsが等しい場合、シーンを変更する。
            //SceneManager.LoadScene("CB400SF00");// 最終的にはQRで読み込んだデータをもとにシーンを変更する
            return;
        }
    }

    

}
