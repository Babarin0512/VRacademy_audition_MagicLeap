using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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


    private string Data;

    public bool swich;
    

    private ToolCheckCounter toolCheckCounter;
    private CheckTool checkTool;

    void Start()
    {
        MLBarcodeScanner.BarcodeType type = MLBarcodeScanner.BarcodeType.All;

        _barcodeSettings = MLBarcodeScanner.Settings.Create(false, type, QRCodeSize);
        SetSettingsAsync(_barcodeSettings);

        toolCheckCounter = GameObject.Find("Canvas").GetComponent<ToolCheckCounter>();
        checkTool = GameObject.Find("Previous page").GetComponent<CheckTool>();

        
    }

    private void MLInputOnOnTriggerDown(byte controllerid, float triggervalue)
    {
#if PLATFORM_LUMIN
        // Controlのトリガーが押した状態で且つ、QRコードのスキャンを実施していない場合
        if (triggervalue > .5f && !_isScanning)
        {
            // QRコードのスキャンを開始
            StartScanningAsync();

            // QRコードのスキャン中を判定するフラグを立てる
            _isScanning = true;

            // Controlを振動させる 
            MLInput.Controller targetController = MLInput.GetController(controllerid);

           

            targetController.StartFeedbackPatternVibe(MLInput.Controller.FeedbackPatternVibe.ForceDown, MLInput.Controller.FeedbackIntensity.Medium);
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
        // ControlのTriggerのイベントハンドラーを登録する。
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            _didInit = true;
            MLBarcodeScanner.OnMLBarcodeScannerResultsFound += OnMLBarcodeScannerResultsFound;
            MLInput.OnTriggerDown += MLInputOnOnTriggerDown;
            MLInput.OnTriggerUp += MLInputOnOnTriggerUp;
        }
#endif
    }

    private void OnDisable()
    {
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
        StartCoroutine(toolCheckCounter.LoadSpriteManual("car"));

       


        if (data.Type != MLBarcodeScanner.BarcodeType.None)
        {
            ExtractBarcodeScannerData(data);
            StartCoroutine(toolCheckCounter.LoadSpriteManual("car"));
        }
    }


    /// <summary>
    /// QRコードのPrefabを作成または更新し、車種ごとの画像を読み込む。
    /// </summary>
    /// <param name="data"></param>
    private void ExtractBarcodeScannerData(BarcodeData data)
    {
        StartCoroutine(toolCheckCounter.LoadSpriteManual("car")); // 画像を読み込む関数を実行する


      

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
            //StartCoroutine(toolCheckCounter.LoadSpriteManual(data.StringData));
        }

        
    }
    #endregion
}


