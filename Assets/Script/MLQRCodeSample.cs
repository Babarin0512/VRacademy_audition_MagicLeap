using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.MagicLeap;
using UnityEngine.XR.Management;
using static UnityEngine.XR.MagicLeap.MLBarcodeScanner;
using BarcodeSettings = UnityEngine.XR.MagicLeap.MLBarcodeScanner.Settings;

public class MLQRCodeSample : MonoBehaviour
{
    [SerializeField, Tooltip("QRコードの大きさをメートル単位で表したもの")]
    private float QRCodeSize = 0.1f;
    [SerializeField, Tooltip("QRコードのデータを可視化するPrefab")]
    private MLQRCodeVisual qrCodeVisualPrefab;

    // トラッキング済みのQRコード
    private Dictionary<string, MLQRCodeVisual> qrCodeVisualByBarcodeData = new Dictionary<string, MLQRCodeVisual>();

    // QRコードトラッキングの設定
    private BarcodeSettings _barcodeSettings;

    // QRコードスキャンの重複起動を防ぐためのフラグ
    private bool _isScanning;

    // Control入力がサブスクライブされているかどうかを確認するためのフラグ
    private bool _didInit;

    // Panel(CarSelect)に関連するObjectを取得する
    private GameObject panelCarSelect;
    private Text panelCarSelect_text;
    private Image panelCarSelect_image;

    // ScanButton.csのScanButton_flag変数を取得する
    private ScanButton scaButton;
    private bool scanButton_flag;
    

    void Start()
    {
        MLBarcodeScanner.BarcodeType type = MLBarcodeScanner.BarcodeType.All;

        _barcodeSettings = MLBarcodeScanner.Settings.Create(false, type, QRCodeSize);
        SetSettingsAsync(_barcodeSettings);

        panelCarSelect = GameObject.FindGameObjectWithTag("panel(CarSelect)");
        panelCarSelect_text = panelCarSelect.transform.GetChild(0).gameObject.GetComponent<Text>();
        panelCarSelect_image = panelCarSelect.transform.GetChild(1).gameObject.GetComponent<Image>();

        scanButton_flag = GameObject.FindGameObjectWithTag("scanButton").GetComponent<ScanButton>().ScanButton_flag;
    }

    private void MLInputOnOnTriggerDown(byte controllerid, float triggervalue)//public void MLInputOnButtonTap()
    {
        panelCarSelect.SetActive(true);
#if PLATFORM_LUMIN
       // Controlのトリガーが押した状態で且つ、QRコードのスキャンを実施していない場合
       //if(scanButton_flag && !_isScanning)
       if (triggervalue > .5f && !_isScanning)//ScanButtonが押されて且つ、QRコードのスキャンを実施していない場合
       {
           // QRコードのスキャンを開始
           StartScanningAsync();

           // QRコードのスキャン中を判定するフラグを立てる
           _isScanning = true;

           //スキャンボタンを押したか判定するbool変数をfalseに戻す
           scanButton_flag = false;

           
           
           //Controlを振動させる 
           MLInput.Controller targetController = MLInput.GetController(controllerid);
           targetController.StartFeedbackPatternVibe(MLInput.Controller.FeedbackPatternVibe.ForceDown, MLInput.Controller.FeedbackIntensity.Medium);
       }

       else if(scanButton_flag && _isScanning)//QRコードのスキャン中に間違えてボタンを押した場合
       {
           return;
       }
#endif
    }

    private void MLInputOnOnTriggerUp(byte controllerid, float triggervalue)
    {
#if PLATFORM_LUMIN
       // Controlのトリガーが離した状態で且つ、QRコードのスキャンを実施している場合
       if (triggervalue < .5f && _isScanning)
       {
           // QRコードのスキャンを停止
           StopScanningAsync();

           // QRコードのスキャン中を判定するフラグをOFFにする
           _isScanning = false;

           // Controlを振動させる 
           MLInput.Controller targetController = MLInput.GetController(controllerid);

           targetController.StartFeedbackPatternVibe(MLInput.Controller.FeedbackPatternVibe.ForceDown, MLInput.Controller.FeedbackIntensity.Medium);
       }
#endif
    }

    private void OnDestroy()
    {

    }

    private void OnEnable()
    {
#if PLATFORM_LUMIN
       // XR Managerが動作中かを確認します。1つでも実行されていれば、Magic Leap 1であるとみなし、
       // ControlのTriggerのイベントハンドラーを登録する。←必要ないので削除する
       if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
       {
           _didInit = true;
           MLBarcodeScanner.OnMLBarcodeScannerResultsFound += OnMLBarcodeScannerResultsFound;//
           MLInput.OnTriggerDown += MLInputOnOnTriggerDown;
           MLInput.OnTriggerUp += MLInputOnOnTriggerUp;
       }
#endif
    }

    private void OnDisable()// 非アクティブの場合はスキャンしたQRコードの情報を消去する
    {
        MLBarcodeScanner.OnMLBarcodeScannerResultsFound -= OnMLBarcodeScannerResultsFound;
       if (_didInit)
       {
           MLBarcodeScanner.OnMLBarcodeScannerResultsFound -= OnMLBarcodeScannerResultsFound;
           MLInput.OnTriggerDown -= MLInputOnOnTriggerDown;
           MLInput.OnTriggerUp -= MLInputOnOnTriggerUp;
       }
    }

    #region Magic Leap Barcode Scanner API
    private void SetSettingsAsync(BarcodeSettings value)
    {
       _ = MLBarcodeScanner.SetSettingsAsync(value);
    }

    private void StartScanningAsync(BarcodeSettings? settings = null)
    {
       _ = MLBarcodeScanner.StartScanningAsync(settings);
    }

    private void StopScanningAsync()
    {
       _ = MLBarcodeScanner.StopScanningAsync();
    }

    private void OnMLBarcodeScannerResultsFound(BarcodeData data)
    {
           Debug.Log(data.StringData);
         
       if (data.Type != MLBarcodeScanner.BarcodeType.None)
       {
               ExtractBarcodeScannerData(data);
              
       }
    }


   /// <summary>
   /// QRコードのPrefabを作成または更新します。
   /// </summary>
   /// <param name="data"></param>
   private void ExtractBarcodeScannerData(BarcodeData data)// 車種パネルをアクティブにする
    {
       Debug.Log(data.StringData);
       if (qrCodeVisualByBarcodeData.TryGetValue(data.StringData, out MLQRCodeVisual visual))
       {
               visual.Set(data);
       }
       else
       {
           MLQRCodeVisual qrCode = Instantiate(qrCodeVisualPrefab);
           qrCodeVisualByBarcodeData.Add(data.StringData, qrCode);
           qrCode.Set(data);

           
       }
    }
    #endregion

   
}