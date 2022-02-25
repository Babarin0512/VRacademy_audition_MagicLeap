using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Korutin : MonoBehaviour
{

    public IList<Sprite> image_Tool; // Resouseフォルダから読み込んだ工具の画像を格納する配列  //private Sprite[] image_Tool;
    IList<Sprite> image_Manual; // 整備画像マニュアルを格納する    //Sprite[] image_Manual;
    public IList<Sprite> image_Tutorial; // チュートリアル画像をローカルAssetsから取得する
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoadSpriteManual", "car");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ChenghImage_Tool()
    {
        //image_Manual = image_Tutorial;
        Debug.Log(image_Manual[0].name);
        yield return null;
    }

    /*public IEnumerator LoadSpriteManual(string ModelOfName)//
    {


        var handle_manual = Addressables.LoadAssetsAsync<Sprite>(ModelOfName + "manual", null);

        yield return handle_manual;
        Debug.Log(ModelOfName + "manual");



        if (handle_manual.Status == AsyncOperationStatus.Succeeded)//＊成功と失敗それぞれの処理を実装するところから再開する
        {
            image_Tool = handle_manual.Result;
            foreach (var value in image_Tool)
            {


                Debug.Log(value.name);
            }

            yield return image_Tool;


        }
        else if (handle_manual.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError($"{ModelOfName}がないよ");// 3Dテキストで"ネットワークの接続状況を確認してください"と表示する
            yield break;
        }
        //yield return StartCoroutine("ChenghImage_Tool");
    }*/

        public IEnumerator LoadSpriteManual(string ModelOfName)
    {

        var handle_tool = Addressables.LoadAssetsAsync<Sprite>(ModelOfName + "tool", null); // 
        var handle_manual = Addressables.LoadAssetsAsync<Sprite>(ModelOfName + "manual", null);
        yield return handle_tool;
        yield return handle_manual;
        Debug.Log(ModelOfName + "manual");
        Debug.Log(ModelOfName + "tool");


        if (handle_tool.Status == AsyncOperationStatus.Succeeded)//＊成功と失敗それぞれの処理を実装するところから再開する
        {
            image_Tool = handle_tool.Result;
            foreach (var value in image_Tool)
            {


                Debug.Log(value.name);
            }

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
            foreach (var value in image_Manual)
            {
                Debug.Log(value.name);
            }
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
