using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ToolCheckCounter : MonoBehaviour
{
    // 表示する画像配列の番号
    public int counts;　// 表示する用
    public int count_Manual;
    public int count_Tool;
    public int count_Tutorial;

    public int ExecutionMethod;

   
    public IList<Sprite> image_Tool; // Resouseフォルダから読み込んだ工具の画像を格納する配列  //private Sprite[] image_Tool;
    IList<Sprite> image_Manual; // 整備画像マニュアルを格納する    //Sprite[] image_Manual;
    public IList<Sprite> image_Tutorial; // チュートリアル画像をローカルAssetsから取得する
    

    Image _panel;

    public int imageLength; // 各配列の画像の最大枚数

  
    [SerializeField] GameObject followingPage;
    [SerializeField] GameObject mlQRCodeSample;
    

    [SerializeField] TextMesh text_NextTool;
    [SerializeField] TextMesh text_Check;
    [SerializeField] Text _text;

    [SerializeField] CheckTool checkTool;

    public bool _swich;




    void Awake()
    {
       
        counts = 0;
        ExecutionMethod = 0;

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

 }

 void Update()
{
    _text.text = "チェック完了:" + counts.ToString() + "/" + imageLength.ToString();

       // チュートリアルの画像が配列の最大数を超えて表示されないようにする
       if (count_Tutorial > image_Tutorial.Count - 1)
        {
            counts = imageLength; 
            _panel.sprite = image_Tutorial[count_Tutorial];
        }
 }

    
    // チュートリアル画像を変更します
    public void FollowingTutorialPage()
    {
        // count_Tutorialに+1して表示させる用のcountsに代入する
        count_Tutorial += 1;
        counts = count_Tutorial;
        _panel.sprite = image_Tutorial[counts];
    }

    // 表示する工具画像を変更します
    public void FollowingToolPage()
    {
        count_Tool += 1;
        counts = count_Tool;
        _panel.sprite = image_Tool[counts];
    }

    // 表示するマニュアル画像を変更する
    public void FollowingManualPage()
    {
        count_Manual += 1;
        counts = count_Manual;
        _panel.sprite = image_Manual[counts];
    }

    // 読み込んだ工具画像をパネルに表示する
    public IEnumerator ChenghImage_Tool()
{
        bool flag = true;
        // Assets/Resouse/Manualの整備マニュアル画像を表示する　画像を切り替える
        counts = count_Tool;

        _panel.sprite = image_Tool[0];
       

        imageLength = image_Tool.Count - 1;
        count_Tutorial = 0;
        text_Check.text = "用意完了後押す";
        text_NextTool.text = "次の工具を用意する";

        checkTool.switchCheck(flag);

        ExecutionMethod = 1;
        mlQRCodeSample.SetActive(false);


         yield return _panel.sprite = image_Tool[0];
        yield return imageLength = image_Tool.Count - 1;
    }

    // 読み込んだマニュアル画像をパネルに表示する
    public void ChenghImage_Manual()
    {
        counts = count_Manual;
        _panel.sprite = image_Manual[0];
        imageLength = image_Manual.Count - 1;
        count_Tool = 0;
        ExecutionMethod = 2;

        // テキストの内容を変更する
        text_Check.text = "作業完了したら押す";
        text_NextTool.text = "次の作業に移る";
    }

    // チュートリアルに画像をパネルに表示します
    public void chenghImage_QR()
    {
        counts = count_Tutorial;
        _panel.sprite = image_Tutorial[0];
        imageLength = image_Tutorial.Count - 1;
        count_Manual = 0;
        ExecutionMethod = 0;

        mlQRCodeSample.SetActive(true);

        // テキストの内容を変更する
        text_Check.text = "右人差し指でプッシュしてください";
        text_NextTool.text = "次のチュートリアルに移行";

    }

// 外部から工具画像とマニュアル画像を読み込む
public IEnumerator LoadSpriteManual(string ModelOfName)
{
    var handle_tool = Addressables.LoadAssetsAsync<Sprite>(ModelOfName + "tool", null); 
    var handle_manual = Addressables.LoadAssetsAsync<Sprite>(ModelOfName + "manual", null);
    yield return handle_tool;
    yield return handle_manual;

    if(handle_tool.Status == AsyncOperationStatus.Succeeded)//＊成功と失敗それぞれの処理を実装するところから再開する
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
         image_Manual = handle_manual.Result;
         yield return image_Manual;        
    }
    else if (handle_manual.Status == AsyncOperationStatus.Failed)
    {
         Debug.LogError($"{ModelOfName}がないよ");
         yield break;
    }

         yield return StartCoroutine("ChenghImage_Tool");
    }

   
}
