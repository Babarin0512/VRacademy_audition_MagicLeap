using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ToolCheckCounter : MonoBehaviour
{
    Text _text;
    public int counts;

    private GameObject _mainCamera;//MainCameraを取得する
    private Quaternion quaternion;


    public IList<Sprite> image_Tool; // Resouseフォルダから読み込んだ工具の画像を格納する配列  //private Sprite[] image_Tool;
    IList<Sprite> image_Manual; // 整備画像マニュアルを格納する    //Sprite[] image_Manual;
    public IList<Sprite> image_Tutorial; // チュートリアル画像をローカルAssetsから取得する
    

    public Image _panel;
    private GameObject imageObject;//imageをGameObjectとして代入する変数

    public int imageLength; // 各配列の画像の最大枚数

    private GameObject followingPage;
    private GameObject checkMarkOn;
    private GameObject checkMarkOff;
    //private bool checkBox;// チェックボックスにチェックが入ってるかどうか判定する
    private GameObject text_NextTool;
    private GameObject text_Check;
    public GameObject[] Button;



    void Awake()
    {
        Button[0] = GameObject.Find("FollowingPage");
        Button[1] = GameObject.Find("Previous page");
        Button[2] = GameObject.Find("PushCounter");
        Button[3] = GameObject.Find("MLQRCodeSample");
        counts = 0;
        _text = GameObject.Find("PushCounter").GetComponent<Text>();

        //チュートリアルの画像をResouseフォルダ内から取得する
        image_Tutorial = Resources.LoadAll<Sprite>("Tutorial");
    }

void Start()
{

    //PanelのImageコンポーネントを取得する
    _panel = GameObject.Find("Panel").GetComponent<Image>();

        // imageをパネルに生成する
   
    _panel.sprite = image_Tutorial[0];

    imageLength = image_Tutorial.Count - 1;

        for(int i = 0; i < 3; i++)
        {
            Button[i].transform.localScale = Vector3.one * 0; 
        }
}

// Update is called once per frame
void Update()
{

    _text.text = "完了した作業:" + counts.ToString() + "/" + imageLength.ToString();



    if (counts > imageLength)
    {
        counts = imageLength; // 配列の最大数より大きくならないようにする
        //_panel.sprite = image_Tool[0];
    }
    
}

    public void FollowingTutorialPage()
    {
        counts += 1;
        _panel.sprite = image_Tutorial[counts];
    }

    public void FollowingToolPage()
    {
        counts += 1;
        _panel.sprite = image_Tool[counts];
    }

    public void FollowingManualPage()
    {
        counts += 1;
        _panel.sprite = image_Manual[counts];
    }


    public void ChenghImage_Tool()
{
        // Assets/Resouse/Manualの整備マニュアル画像を表示する　画像を切り替える
        counts = 0;
    _panel.sprite = image_Tool[0];
    imageLength = image_Tool.Count - 1;
}

    public void ChenghImage_Manual()
    {
        counts = 0;
        _panel.sprite = image_Manual[0];
        imageLength = image_Manual.Count - 1;
    }

    public void chenghImage_QR()
    {
        counts = 0;
        _panel.sprite = image_Tutorial[0];
        imageLength = image_Tutorial.Count - 1;
        for (int i = 0; i < 3; i++)
        {
            Button[i].transform.localScale = new Vector3(0, 0, 0); 
        }
        Button[3].SetActive(true);
    }

public IEnumerator LoadSpriteManual(string ModelOfName)
{

    var handle_tool = Addressables.LoadAssetsAsync<Sprite>(ModelOfName + "Tool", null); // 
    var handle_manual = Addressables.LoadAssetsAsync<Sprite>(ModelOfName + "Manual", null);
       

    if (handle_tool.Status == AsyncOperationStatus.Succeeded)//＊成功と失敗それぞれの処理を実装するところから再開する
    {
        image_Tool = handle_tool.Result;
        yield return image_Tool;
    }
    else if (handle_tool.Status == AsyncOperationStatus.Failed)
    {
        Debug.LogError($"{ModelOfName}がないよ");// 3Dテキストで"ネットワークの接続状況を確認してください"と表示する
            yield break;
    }

      

        if (handle_manual.Status == AsyncOperationStatus.Succeeded)
        {
            image_Manual = handle_tool.Result;
            yield return image_Manual;
        }
        else if (handle_manual.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError($"{ModelOfName}がないよ");
            yield break;
        }
    }




}
