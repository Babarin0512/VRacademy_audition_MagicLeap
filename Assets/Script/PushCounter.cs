using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushCounter : MonoBehaviour
{
    public Text _text;
    public int counts;

    private GameObject _mainCamera;//MainCameraを取得する
    private Quaternion quaternion;
    

    private Sprite[] image;//Resouseフォルダから読み込んだ画像を格納する配列
    private Image _panel;
    private GameObject imageObject;//imageをGameObjectとして代入する変数


    void Awake()
    {
        counts = 0;
        _text = GameObject.Find("PushCounter").GetComponent<Text>();
        _text.text = counts.ToString();
    }

    void Start()
    {
        
    

        //image[]にResouseフォルダ内の画像を格納する
         image = Resources.LoadAll<Sprite>("Sprites");
        //PanelのImageコンポーネントを取得する
        _panel = GameObject.Find("Panel").GetComponent<Image>();
        
        //imageを生成する
        _panel.sprite = image[0];

        //MainCameraのRotationの値を取得する
        _mainCamera = GameObject.Find("MainCamera");
        //Debug.Log(_mainCamera.name);
        //Quaternion quaternion = _mainCamera.transform.rotation;
        
        
        
      
    }

    // Update is called once per frame
    void Update()
    {
         _text.text = counts.ToString() + "/" + image.Length.ToString() + "ページ";
         

         if(counts > 2)
         {
             counts = 0;
         }
         else if(counts < 0)
         {
             counts = 0;
         }

         float x = quaternion.eulerAngles.x;
         float y = quaternion.eulerAngles.y;
         this.gameObject.transform.rotation = Quaternion.Euler(x, y, 0);
       
  
    }

    public void FollowingPage()
    {
        counts += 1;

        if(counts < image.Length)
        {
            //image[]から画像を取得してPanel上に生成する
            
            _panel.sprite = image[counts];
        }

        else if(counts == image.Length)
        {
            return;
        }
        
    }

    public void PreviousPage()
    {
        counts -= 1;
       
        if(counts > 0)
        {
            _panel.sprite = image[counts];
    
        }

        else if(counts < 0)
        {
            return;
        }
    }



    

    
}
