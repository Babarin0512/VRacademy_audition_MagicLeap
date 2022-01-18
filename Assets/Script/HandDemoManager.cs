using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class HandDemoManager : MonoBehaviour
{
    [SerializeField] private MLHandTracking.HandKeyPose _activeUIKeyPose;
    [SerializeField] private MLHandTracking.HandKeyPose _inactiveUIKeyPose;
    [SerializeField] private MLHandTracking.HandKeyPose _selectUIKeyPose;

    [SerializeField] private GameObject _uiObject;
    [SerializeField] private GameObject _handCollision;

    //手のジェスチャーに応じた
    //認識率
    private float _keyPoseConfidenceValue = 0.6f;

    private MLResult result;
        
    private void Start()
    {

        //HandTracking Start
        result = MLHandTracking.Start();

        //ハンドトラッキング機能が正しく立ち上がってるかチェック
        if (!result.IsOk)
        {
            return;
        }

        //左手
        MLHandTracking.Hand mlLHand = MLHandTracking.Left;

        //右手
        MLHandTracking.Hand mlRHand = MLHandTracking.Right;

        //手を登録
        MLHandTracking.KeyposeManager ｋeyPoseManager = new MLHandTracking.KeyposeManager(mlLHand, mlRHand);

        //---------------------------------------------------------------
        //　任意のポーズのコールバック　右手
        //---------------------------------------------------------------

        //右手のポーズ変化のコールバック　UIを表示
        ｋeyPoseManager.OnKeyPoseBegin += (pose, type) =>
        {
            if (type == MLHandTracking.HandType.Left && pose == _activeUIKeyPose)
            {
                _uiObject.SetActive(true);
            }
                
            if (type == MLHandTracking.HandType.Left && pose == _inactiveUIKeyPose)
            {
                _uiObject.SetActive(false);
            }
        };
    }

    private void Update()
    {
        //ハンドトラッキング機能が正しく立ち上がってるかチェック
        if (!result.IsOk)
        {
            return;
        }

        //任意のポーズ時に指先に当たり判定を与える
        if (MLHandTracking.Right.KeyPose == _selectUIKeyPose && MLHandTracking.Right.HandKeyPoseConfidence >= _keyPoseConfidenceValue)
        {
            //任意の手の形状であればアクティブに → すなわち、当たり判定追加
            _handCollision.SetActive(true);
            _handCollision.gameObject.transform.position = MLHandTracking.Right.Index.Tip.Position;
        }
        else
        {
            //任意の手の形状でなければ非アクティブ → すなわち、当たり判定消去
            _handCollision.SetActive(false);
        }
    }
}
   
