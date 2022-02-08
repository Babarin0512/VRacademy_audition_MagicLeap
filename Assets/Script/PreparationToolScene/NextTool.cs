using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//次のページに進むオブジェクトにアタッチする
public class NextTool : MonoBehaviour
{
    private PushCounter pushCounter;
    private int counts;
    private int imageLength;

    private TextMesh text_NextTool;

    private void OnEnable()//ここは実行されている
    {
        int counts;
        counts = pushCounter.counts;

        if(counts < imageLength)
        {
            text_NextTool.text = counts.ToString() + "/" + imageLength.ToString();//"次に用意する工具を表示します";
        }
        else if(counts == imageLength)
        {
            text_NextTool.text = "整備作業に移る";
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        pushCounter = GameObject.Find("PushuCounter").GetComponent<PushCounter>();
        int imageLength;
        imageLength = pushCounter.imageLength;

        text_NextTool = GameObject.Find("Text_NextTool").GetComponent<TextMesh>();

}

    // Update is called once per frame
    void Update()
    {

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
            //text_NextTool.text = "次に用意する工具を表示します";
            transform.root.gameObject.GetComponent<PushCounter>().FollowingPage();
            gameObject.SetActive(false);
        }
        else if (counts == imageLength)
        {
            // 読み込んだマニュアル画像の数と最後までcountsが等しい場合、シーンを変更する。
            SceneManager.LoadScene("CB400SF00");// 最終的にはQRで読み込んだデータをもとにシーンを変更する
        }
    }
}
