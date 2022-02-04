using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class MLQRCodeVisual : MonoBehaviour
{
   [SerializeField]
   private TextMesh dataText;
#if UNITY_LUMIN
   private Timer disableTimer;
#endif

private string _string;

   void Awake()
   {
#if UNITY_LUMIN
       disableTimer = new Timer(3f);
#endif
   }

   void Start()
   {
      _string = GameObject.Find("ScanButton").GetComponent<ScanButton>().dataString;
   }

   void Update()
   {
#if UNITY_LUMIN
       if (gameObject.activeSelf && disableTimer.LimitPassed)
       {
           gameObject.SetActive(false);
       }
#endif
   }

   public void Set(MLBarcodeScanner.BarcodeData data)
   {
#if UNITY_LUMIN
       disableTimer?.Reset();
#endif

       transform.position = data.Pose.position;
       transform.rotation = data.Pose.rotation;
       dataText.text = data.ToString();
       gameObject.SetActive(true);
       _string = data.StringData;
   }
}
